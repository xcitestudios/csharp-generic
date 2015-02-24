namespace com.xcitestudios.Generic.Interfaces
{
    /// <summary>
    /// Really simple interface to add a validation check.
    /// </summary>
    public interface IValid
    {
        /// <summary>
        /// Is this object valid?
        /// </summary>
        /// <returns>True if it is, false if it isn't.</returns>
        bool IsValid();
    }
}
