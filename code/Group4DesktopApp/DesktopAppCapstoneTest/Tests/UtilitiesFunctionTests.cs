using Group4DesktopApp.Model;
using Group4DesktopApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    public class UtilitiesFunctionTests
    {
        [Test]
        public void linkParserIsInvalidYoutubeLink()
        {
            string link = "https://courses.cs.westga.edu/";
            bool isValid = LinkParser.IsYoutubeLink(link);
            Assert.IsFalse(isValid);
        }

        [Test]
        public void linkParserValidLink()
        {
            string link = "https://www.youtube.com/watch?v=a3ICNMQW7Ok";
            bool isValid = LinkParser.IsYoutubeLink(link);
            Assert.IsTrue(isValid);
        }

        [Test]
        public void linkParserAttemptToExtractIDFromInvalidLink()
        {
            string link = "https://courses.cs.westga.edu/";
            string? id = LinkParser.ExtractYoutubeLinkID(link);
            Assert.IsNull(id);
        }

        [Test]
        public void linkParserExtractIDFromValidLink()
        {
            string link = "https://www.youtube.com/watch?v=a3ICNMQW7Ok";
            string? id = LinkParser.ExtractYoutubeLinkID(link);
            Assert.That(id, Is.EqualTo("a3ICNMQW7Ok"));
        }

        [Test]
        public void charCheckerCharIsSpecialChar()
        {
            char chr = '@';
            bool isSpecialChar = CharChecker.IsSpecialCharacter(chr);
            Assert.IsTrue(isSpecialChar);
        }

        [Test]
        public void charCheckerCharIsNotSpecialChar()
        {
            char chr = 'A';
            bool isSpecialChar = CharChecker.IsSpecialCharacter(chr);
            Assert.IsFalse(isSpecialChar);
        }
    }
}
