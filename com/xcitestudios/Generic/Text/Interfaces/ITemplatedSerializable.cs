namespace com.xcitestudios.Generic.Text.Interfaces
{
    using global::com.xcitestudios.Generic.Data.Manipulation.Interfaces;

    /// <summary>
    /// General interface for something that can render a template style string and supports serialization.
    /// </summary>
    public interface ITemplatedSerializable : ITemplated, ISerialization
    {
    }
}
