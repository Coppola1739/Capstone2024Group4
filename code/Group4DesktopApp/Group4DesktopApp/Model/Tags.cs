using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The Tags class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class Tags
    {
        private string tagName;
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName
        {
            get { return tagName; }
            set
            {
                tagName = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tags"/> class.
        /// </summary>
        public Tags()
        {
            this.tagName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tags"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public Tags(string tagName)
        {
            this.tagName = tagName;
        }
    }
}
