using Group4DesktopApp.Model;
using Group4DesktopApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    /// <summary>
    /// Test Class for all the Utilities Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class UtilitiesFunctionTests
    {
        /// <summary>
        /// Test Link Parser on an invalid Youtube Link
        /// </summary>
        [Test]
        public void linkParserIsInvalidYoutubeLink()
        {
            string link = "https://courses.cs.westga.edu/";
            bool isValid = LinkParser.IsYoutubeLink(link);
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Test Link Parser on a valid Youtube Link
        /// </summary>
        [Test]
        public void linkParserValidLink()
        {
            string link = "https://www.youtube.com/watch?v=a3ICNMQW7Ok";
            bool isValid = LinkParser.IsYoutubeLink(link);
            Assert.IsTrue(isValid);
        }
        /// <summary>
        /// Test Link Parser on extracting an ID from an invalid Youtube Link
        /// </summary>
        [Test]
        public void linkParserAttemptToExtractIDFromInvalidLink()
        {
            string link = "https://courses.cs.westga.edu/";
            string? id = LinkParser.ExtractYoutubeLinkID(link);
            Assert.IsNull(id);
        }
        /// <summary>
        /// Test Link Parser on extracting an ID from a valid Youtube Link
        /// </summary>
        [Test]
        public void linkParserExtractIDFromValidLink()
        {
            string link = "https://www.youtube.com/watch?v=a3ICNMQW7Ok";
            string? id = LinkParser.ExtractYoutubeLinkID(link);
            Assert.That(id, Is.EqualTo("a3ICNMQW7Ok"));
        }

        /// <summary>
        /// Test Char Checker on an actual special character
        /// </summary>
        [Test]
        public void charCheckerCharIsSpecialChar()
        {
            char chr = '@';
            bool isSpecialChar = CharChecker.IsSpecialCharacter(chr);
            Assert.IsTrue(isSpecialChar);
        }
        /// <summary>
        /// Test Char Checker on not a special character
        /// </summary>
        [Test]
        public void charCheckerCharIsNotSpecialChar()
        {
            char chr = 'A';
            bool isSpecialChar = CharChecker.IsSpecialCharacter(chr);
            Assert.IsFalse(isSpecialChar);
        }
    }
}
