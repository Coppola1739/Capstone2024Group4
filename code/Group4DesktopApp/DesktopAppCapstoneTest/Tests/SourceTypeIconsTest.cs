using Group4DesktopApp.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    /// <summary>
    /// Test Class for all the Source Icon Paths
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class SourceTypeIconsTest
    {
        /// <summary>
        /// Test the path to PDF icon is returned.
        /// </summary>
        [Test]
        public void returnsPathToPDFIcon()
        {
            string path = SourceIconPaths.GetIconPath("PDF");
            Assert.IsTrue(path.Contains("pdf-icon.png"));
        }

        /// <summary>
        /// Test the path to Youtube icon is returned.
        /// </summary>
        [Test]
        public void returnsPathToYoutubeIcon()
        {
            string path = SourceIconPaths.GetIconPath("VIDEO");
            Assert.IsTrue(path.Contains("youtube-icon.png"));
        }

        /// <summary>
        /// Test the path to Default source icon is returned.
        /// </summary>
        [Test]
        public void returnsPathToDefaultIcon()
        {
            string path = SourceIconPaths.GetIconPath("RandomSourceType");
            Assert.IsTrue(path.Contains("unknown-source.png"));
        }
    }
}
