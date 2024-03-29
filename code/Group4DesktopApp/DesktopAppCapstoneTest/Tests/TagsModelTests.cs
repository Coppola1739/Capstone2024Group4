using Group4DesktopApp.Model;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    /// <summary>
    /// Test Class for all the Tag Model Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class TagsModelTests
    {
        /// <summary>
        /// Tests the tags default constructor.
        /// </summary>
        [Test]
        public void TestTagsDefaultConstructor()
        {
            Tags tag = new Tags();
            Assert.IsNotNull(tag);
        }

        /// <summary>
        /// Tests the tags parameterized constructor.
        /// </summary>
        [Test]
        public void TestTagsConstructor()
        {
            Tags tag = new Tags("TestTag");
            Assert.IsNotNull(tag);
            Assert.That(tag.TagName.Equals("TestTag"));
        }

        /// <summary>
        /// Tests setting a tag name.
        /// </summary>
        public void TestTagsSetName()
        {
            Tags tag = new Tags();
            tag.TagName = "test";
            Assert.IsNotNull(tag);
            Assert.That(tag.TagName.Equals("test"));
        }
    }
}
