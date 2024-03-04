using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Group4DesktopApp.Utilities
{
    public static class LinkParser
    {
        private static Regex youtubeRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

        public static bool IsYoutubeLink(string link)
        {
            Match youtubeMatch = youtubeRegex.Match(link);

            if (youtubeMatch.Success)
            {
                return true;
            }
            return false;
        }

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
