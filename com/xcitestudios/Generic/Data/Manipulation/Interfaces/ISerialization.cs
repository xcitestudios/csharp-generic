namespace com.xcitestudios.Generic.Data.Manipulation.Interfaces
{
    /// <summary>
    /// General interface for an object that can be converted to/from JSON.
    /// </summary>
    public interface ISerialization
    {
        /// <summary>
        /// Updates the element implementing this interface using a JSON representation. 
        /// This means updating the state of this object with that defined in the JSON 
        /// as opposed to returning a new instance of this object.
        /// </summary>
        /// <param name="jsonString">Representation of the object</param>
        void Deserialize(string jsonString);

        /// <summary>
        /// Convert this object into JSON so it can be handled by anything that supports JSON.
        /// </summary>
        /// <returns>A JSON representation of this object</returns>
        string Serialize();
    }
}
