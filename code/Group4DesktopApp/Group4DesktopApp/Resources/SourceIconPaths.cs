using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.Resources
{
    public static class SourceIconPaths
    {
        private static string workingDirectory = Environment.CurrentDirectory;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        [ExcludeFromCodeCoverage]
        public static string GetIconPath(string sourceType)
        {
            string path = string.Empty;
            string fileName = string.Empty;
            string type = sourceType.ToUpper();

            switch (type)
            {
                case "PDF":
                    fileName = "pdf-icon.png";
                    path = Path.Combine(projectDirectory, @"Assets\", fileName);
                    break;

                case "VIDEO":
                    fileName = "youtube-icon.png";
                    path = Path.Combine(projectDirectory, @"Assets\", fileName);
                    break;

                default:
                    fileName = "unknown-source.png";
                    path = Path.Combine(projectDirectory, @"Assets\", fileName);
                    break;
            }
            return path;
        }
    }
}
