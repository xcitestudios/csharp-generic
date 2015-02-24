namespace tests.com.xcitestudios.Generic.Text
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using global::com.xcitestudios.Generic.Text;

    [TestClass]
    public class TemplatedTest
    {
        [TestMethod]
        public void TestContext()
        {
            var contextObject = new
            {
                alpha = "ALPHA",
                beta = "BETA",
                trueCheck = true,
                falseCheck = false
            };

            var template = new Template();
            template.Content = "{{alpha}} {{beta}} {{#trueCheck}}trueCheck{{/trueCheck}} {{#falseCheck}}falseCheck{{/falseCheck}}";
            template.Context = contextObject;

            Assert.AreEqual("ALPHA BETA trueCheck ", template.Render());
        }

        [TestMethod]
        public void TestSerializationConsistency()
        {
            var contextObject = new { 
                alpha = "ALPHA",
                beta = "BETA"
            };

            var template = new Template();
            template.Content = "{{alpha}} {{beta}}";
            template.Context = contextObject;

            var json = template.SerializeJSON();

            var newTemplate = new Template();
            newTemplate.DeserializeJSON(json);

            Assert.AreEqual(template.Content, newTemplate.Content);
            Assert.AreEqual("ALPHA BETA", template.Render());
            Assert.AreEqual("ALPHA BETA", newTemplate.Render());
        }
    }
}
