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
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tags"/> class.
        /// </summary>
        public Tags()
        {
            TagName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tags"/> class,
        /// with the specified tag name.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public Tags(string tagName)
        {
            TagName = tagName;
        }
    }
}
