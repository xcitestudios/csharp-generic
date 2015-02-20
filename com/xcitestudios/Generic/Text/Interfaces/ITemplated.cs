namespace com.xcitestudios.Generic.com.xcitestudios.Generic.Text.Interfaces
{
    /// <summary>
    /// General interface for something that can render a template style string.
    /// </summary>
    public interface ITemplated
    {
        /// <summary>
        /// Template version of the content to be used during rendering.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Object passed in to replace items in the content.
        /// </summary>
        object Context { get; set; }

        /// <summary>
        /// Renders content given the context object.
        /// </summary>
        /// <returns>String content rendered using the values in <see cref="P:context"/></returns>
        string Render();
    }
}
