﻿using System.Diagnostics.CodeAnalysis;

/// <summary>
/// API Models
/// </summary>
namespace WebApp.Server.Models
{
    /// <summary>
    /// NoteTags Model
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
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public int NotesId { get; set; }

    }
}
