using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Group4DesktopApp.Utilities
{
    /// <summary>
    /// Utilities class that parses a link to extract values.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public static class LinkParser
    {
        private static Regex youtubeRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

        /// <summary>
        /// Determines whether the specified link is a youtube link.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>
        ///   <c>true</c> if the specified link is a youtube link; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsYoutubeLink(string link)
        {
            Match youtubeMatch = youtubeRegex.Match(link);

            if (youtubeMatch.Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Extracts the youtube link ID. Returns null, if the link is not a valid youtube link or could not extract it.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>The extracted youtube link ID, if valid, null otherwise</returns>
        public static string? ExtractYoutubeLinkID(string link)
        {
            Match youtubeMatch = youtubeRegex.Match(link);

            string id = string.Empty;

            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
                return id;
            }
            return null;
        }
    }
}
