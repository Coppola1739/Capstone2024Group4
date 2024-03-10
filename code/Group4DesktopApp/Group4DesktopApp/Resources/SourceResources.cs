using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Resources
{
    public static class SourceResources
    {
        private static string dir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"EBWebView\");
        private static string dir2 = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"CapstoneSources\");
        public static readonly Dictionary<string, string> ResourceDirectories = new Dictionary<string, string>()
        {
            { "YoutubeBrowser", dir},
            { "PDFDocuments", dir2}

        };

        public static bool isAnyDirectoryExisting()
        {
            return ResourceDirectories.Values.Any(Directory.Exists);
        }
    }
}
