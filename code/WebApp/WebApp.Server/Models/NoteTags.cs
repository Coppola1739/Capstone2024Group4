using System.Diagnostics.CodeAnalysis;

/// <summary>
/// 
/// </summary>
namespace WebApp.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NoteTags
    {

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public int NotesId { get; set; }

    }
}
