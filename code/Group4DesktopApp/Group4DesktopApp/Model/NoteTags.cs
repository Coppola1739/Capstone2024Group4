using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The NoteTag class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
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
        /// Gets or sets the note ID.
        /// </summary>
        /// <value>
        /// The note ID.
        /// </value>
        public int NotesId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteTags"/> class.
        /// </summary>
        public NoteTags() { 
            this.TagName = string.Empty;
            this.NotesId = -1;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteTags"/> class,
        /// with the specified tag name and note id
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="notesId">The notes identifier.</param>
        public NoteTags(string tagName, int notesId)
        {
            TagName = tagName;
            NotesId = notesId;
        }
    }
}
