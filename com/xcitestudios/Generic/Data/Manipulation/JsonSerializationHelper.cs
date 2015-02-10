namespace com.xcitestudios.Generic.Data.Manipulation
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Add this to a class and you can quickly convert it to JSON if it uses [DataMember] and [DataContract]
    /// </summary>
    [DataContractAttribute]
    public abstract class JsonSerializationHelper
    {
        /// <summary>
        /// Return JSON representating "this" which is a type of T
        /// </summary>
        /// <typeparam name="T">type of this object</typeparam>
        /// <returns>JSON representation of this class</returns>
        public string Serialize<T>()
        {
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, this);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Given JSON, return an object of type T.
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <param name="jsonString">Representation of object of type T as JSON.</param>
        /// <returns>An object of type T</returns>
        public T Deserialize<T>(string jsonString)
        {
            using (var ms = new MemoryStream())
            {
                var jsonBytes = Encoding.UTF8.GetBytes(jsonString);

                ms.Write(jsonBytes, 0, jsonBytes.Length);

                ms.Position = 0;
                var ser = new DataContractJsonSerializer(typeof(T));
                var obj = ser.ReadObject(ms);

                return (T)obj;
            }
        }
    }
}
