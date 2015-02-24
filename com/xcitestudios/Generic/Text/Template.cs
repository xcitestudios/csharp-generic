namespace com.xcitestudios.Generic.Text
{
    using com.xcitestudios.Generic.Data.Manipulation.Interfaces;
    using com.xcitestudios.Generic.Text.Interfaces;
    using Nustache.Core;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;

    /// <summary>
    /// A simple implementation of <see cref="I:com.xcitestudios.Generic.Text.Interfaces.ITemplated"/>
    /// </summary>
    [DataContract]
    public class Template : ITemplatedSerializable, ISerialization
    {
        /// <summary>
        /// Template version of the content to be used during rendering.
        /// </summary>
        [DataMember(Name="content")]
        public string Content { get; set; }

        /// <summary>
        /// Object passed in to replace items in the content.
        /// </summary>
        [DataMember(Name = "context")]
        public object Context { get; set; }

        /// <summary>
        /// Renders content given the context object.
        /// </summary>
        /// <returns>String content rendered using the values in <see cref="P:context"/></returns>
        public string Render()
        {
            return Nustache.Core.Render.StringToString(Content, Context);
        }

        /// <summary>
        /// Updates the element implementing this interface using a JSON representation. 
        /// This means updating the state of this object with that defined in the JSON 
        /// as opposed to returning a new instance of this object.
        /// </summary>
        /// <param name="jsonString">Representation of the object</param>
        public void DeserializeJSON(string jsonString)
        {
            var serializer = new JavaScriptSerializer();
            var newObj = serializer.Deserialize<Template>(jsonString);

            Content = newObj.Content;
            Context = newObj.Context;
        }

        /// <summary>
        /// Convert this object into JSON so it can be handled by anything that supports JSON.
        /// </summary>
        /// <returns>A JSON representation of this object</returns>
        public string SerializeJSON()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}
