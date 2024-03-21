using System.Diagnostics.CodeAnalysis;

/// <summary>
/// API Models
/// </summary>
namespace WebApp.Server.Models
{
    /// <summary>
    /// Source class
    /// </summary>
    public class Source
    {

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the upload date.
        /// </summary>
        /// <value>
        /// The upload date.
        /// </value>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public byte[] Content { get; set; }

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        public string SourceType { get; set; }


        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        /// <value>
        /// The first name of the author.
        /// </value>
        public string AuthorFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        /// <value>
        /// The last name of the author.
        /// </value>
        public string AuthorLastName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public ICollection<Notes> Notes { get; set; }

    }
}
