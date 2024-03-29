using Group4DesktopApp.Model;
using Group4DesktopApp.View;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    /// <summary>
    /// Test Class for all the Source Model Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class SourceModelTests
    {
        /// <summary>
        /// Test the Source Model Default constructor 
        /// </summary>
        [Test]
        public void TestSourceDefaultConstructor()
        {
            Source source = new Source();
            Assert.IsNotNull(source);
            Assert.That(source.SourceId, Is.EqualTo(-1));


        }

        /// <summary>
        /// Tests the Source constructor with parameters.
        /// </summary>
        [Test]
        public void TestSourceConstructor()
        {
            byte[] testArray = new byte[0];
            Source source = new Source(-1,2,"name",DateTime.Now, testArray, "PDF","Jeffr","Emek","PDFTitle");
            Assert.IsNotNull(source);
            Assert.That(source.SourceId, Is.EqualTo(-1));
            Assert.That(source.UserId, Is.EqualTo(2));
            Assert.That(source.SourceName, Is.EqualTo("name"));
            Assert.That(source.Content, Is.EqualTo(testArray));
            Assert.That(source.SourceType, Is.EqualTo("PDF"));
            Assert.That(source.AuthorFirstName, Is.EqualTo("Jeffr"));
            Assert.That(source.AuthorLastName, Is.EqualTo("Emek"));
            Assert.That(source.Title, Is.EqualTo("PDFTitle"));
            Assert.IsTrue(source.ToString().Contains(source.SourceName));


        }
    }
}
