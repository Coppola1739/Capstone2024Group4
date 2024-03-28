using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The Notes class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Notes : INotifyPropertyChanged
    {
        private string content;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Gets or sets the notes ID.
        /// </summary>
        /// <value>
        /// The notes ID.
        /// </value>
        public int NotesId { get; set; }
        /// <summary>
        /// Gets or sets the source ID.
        /// </summary>
        /// <value>
        /// The source ID.
        /// </value>
        public int SourceId { get; set; }
        /// <summary>
        /// Gets or sets the Note content.
        /// </summary>
        /// <value>
        /// The Note content.
        /// </value>
        public string Content {
            get { return content; }
            set
            {
                content = value;
                NotifyPropertyChanged(nameof(Content));
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class.
        /// </summary>
        public Notes() {
            this.NotesId = -1;
            this.SourceId = -1;
            this.content = string.Empty;
            this.Content = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class, 
        /// with the specified note ID, source ID, and note content
        /// </summary>
        /// <param name="notesId">The note identifier.</param>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="content">The note content.</param>
        public Notes(int notesId, int sourceId, string content)
        {
            this.NotesId = notesId;
            this.SourceId = sourceId;
            this.content = content;
            this.Content = content;
        }

        /// <summary>
        /// Converts Note to a string using its note content.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Content;
        }
        [ExcludeFromCodeCoverage]
        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
