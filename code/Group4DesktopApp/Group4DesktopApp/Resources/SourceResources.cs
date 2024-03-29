using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Resources
{
    /// <summary>
    /// Hold and manages the location where all temporary documents that are used for the sources.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public static class SourceResources
    {
        private static string dir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"EBWebView\");
        private static string dir2 = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"CapstoneSources\");
        /// <summary>
        /// Returns a dictionary of the source resource directories
        /// </summary>
        public static readonly Dictionary<string, string> ResourceDirectories = new Dictionary<string, string>()
        {
            { "YoutubeBrowser", dir},
            { "PDFDocuments", dir2}

        };
        /// <summary>
        /// Determines whether any of the source resource directories are existing.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if any directory exists; otherwise, <c>false</c>.
        /// </returns>
        public static bool isAnyDirectoryExisting()
        {
            return ResourceDirectories.Values.Any(Directory.Exists);
        }
    }
}
