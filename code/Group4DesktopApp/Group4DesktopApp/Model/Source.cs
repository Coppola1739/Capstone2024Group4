using System;
using System.IO;
using Group4DesktopApp.Resources;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The Source Class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Gets or sets the source ID.
        /// </summary>
        /// <value>
        /// The source ID.
        /// </value>
        public int SourceId { get; set; }
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>
        /// The user ID.
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
        /// Gets or sets the content byte array.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public Byte[] Content { get; set; }
        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        public string SourceType { get; set; }
        /// <summary>
        /// Gets the source image.
        /// </summary>
        /// <value>
        /// The source image.
        /// </value>
        public string SourceImage {
            get { return SourceIconPaths.GetIconPath(SourceType); }
        }
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
        /// Gets or sets the title of the source.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class.
        /// </summary>
        public Source()
        {
            this.SourceId = -1;
            this.UserId = -1;
            this.SourceName = string.Empty;
            this.UploadDate = DateTime.Now;
            this.Content = new byte[1];
            this.SourceType = string.Empty;
            this.AuthorFirstName = string.Empty;
            this.AuthorLastName = string.Empty;
            this.Title = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class, with the specified data.
        /// </summary>
        /// <param name="sourceid">The sourceid.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="sourcename">The sourcename.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="content">The content.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="authorFirstname">The author firstname.</param>
        /// <param name="authorLastname">The author lastname.</param>
        /// <param name="title">The title.</param>
        public Source(int sourceid, int id, string sourcename, DateTime uploadDate, byte[] content, string sourceType, string authorFirstname, string authorLastname, string title)
        {
            this.SourceId = sourceid;
            this.UserId = id;
            this.SourceName = sourcename;
            this.UploadDate = uploadDate;
            this.Content = content;
            this.SourceType = sourceType;
            this.AuthorFirstName = authorFirstname;
            this.AuthorLastName = authorLastname;
            this.Title = title;

        }
        /// <summary>
        /// Converts Source to string, using its source Type and name.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Type: {this.SourceType} Name: {this.SourceName}";
        }
    }
}
