using Group4DesktopApp.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    public class SourceTypeIconsTest
    {
        [Test]
        public void returnsPathToPDFIcon()
        {
            string path = SourceIconPaths.GetIconPath("PDF");
            Assert.IsTrue(path.Contains("pdf-icon.png"));
        }
        [Test]
        public void returnsPathToYoutubeIcon()
        {
            string path = SourceIconPaths.GetIconPath("VIDEO");
            Assert.IsTrue(path.Contains("youtube-icon.png"));
        }
        [Test]
        public void returnsPathToDefaultIcon()
        {
            string path = SourceIconPaths.GetIconPath("RandomSourceType");
            Assert.IsTrue(path.Contains("unknown-source.png"));
        }
    }
}
