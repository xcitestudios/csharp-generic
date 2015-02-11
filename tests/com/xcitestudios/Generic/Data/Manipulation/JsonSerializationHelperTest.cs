namespace tests.com.xcitestudios.Generic.Data.Manipulation
{
    using global::com.xcitestudios.Generic.Data.Manipulation;
    using global::com.xcitestudios.Generic.Data.Manipulation.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract]
    class JsonSerializationHelperTestObject : JsonSerializationHelper, ISerialization
    {
        [DataMember(Name = "stringA")]
        public string StringA { get; set; }

        [DataMember]
        public string StringB { get; set; }

        [DataMember]
        public int IntItem { get; set; }

        [DataMember(Name="floatItem")]
        public float FloatItem { get; set; }

        [DataMember(Name="datetime")]
        public string DateTimeSerialized { get; private set; }

        public DateTime? DateTimeIgnored
        {
            get
            {
                DateTime parsed;

                if (DateTime.TryParse(DateTimeSerialized, null, DateTimeStyles.RoundtripKind, out parsed))
                {
                    return parsed;
                }

                return null;
            }
            set
            {
                DateTimeSerialized = value.HasValue ? value.Value.ToString(@"yyyy-MM-ddTHH\:mm\:sszzz") : null;
            }
        }

        public void Deserialize(string jsonString)
        {
            var newObj = Deserialize<JsonSerializationHelperTestObject>(jsonString);
            StringA = newObj.StringA;
            StringB = newObj.StringB;
            IntItem = newObj.IntItem;
            FloatItem = newObj.FloatItem;
            DateTimeIgnored = newObj.DateTimeIgnored;
        }

        public string Serialize()
        {
            return Serialize<JsonSerializationHelperTestObject>();
        }
    }

    [TestClass]
    public class JsonSerializationHelperTest
    {
        [TestMethod]
        public void TestSerialization()
        {
            var item = new JsonSerializationHelperTestObject();
            item.StringA = "123";
            item.StringB = "abc";
            item.IntItem = 875;
            item.FloatItem = 8.54F;

            var jsonString = item.Serialize();

            StringAssert.StartsWith(jsonString, "{");
            StringAssert.EndsWith(jsonString, "}");
            StringAssert.Contains(jsonString, @"""stringA"":""123""");
            StringAssert.Contains(jsonString, @"""StringB"":""abc""");
            StringAssert.Contains(jsonString, @"""IntItem"":875");
            StringAssert.Contains(jsonString, @"""floatItem"":8.54");
            StringAssert.Contains(jsonString, @"""datetime"":null");

            item.DateTimeIgnored = new DateTime(2015, 1, 5, 23, 11, 45, DateTimeKind.Utc);

            jsonString = item.Serialize();

            StringAssert.Contains(jsonString, @"""datetime"":""2015-01-05T23:11:45+00:00""");
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var item = new JsonSerializationHelperTestObject();
            item.StringA = "123";
            item.StringB = "abc";
            item.IntItem = 875;
            item.FloatItem = 8.54F;
            item.DateTimeIgnored = DateTime.Now;

            var jsonString = item.Serialize();

            var newItem = new JsonSerializationHelperTestObject();
            newItem.Deserialize(jsonString);

            Assert.AreEqual(item.StringA, newItem.StringA);
            Assert.AreEqual(item.StringB, newItem.StringB);
            Assert.AreEqual(item.IntItem, newItem.IntItem);
            Assert.AreEqual(item.FloatItem, newItem.FloatItem);
            Assert.AreEqual(item.DateTimeIgnored, newItem.DateTimeIgnored);
        }
    }
}
