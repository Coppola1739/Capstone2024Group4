using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppCapstoneTest.Tests
{
    public class NotesModelTest
    {
        [Test]
        public void TestNotesDefaultConstructor()
        {
            Notes notes = new Notes();
            Assert.IsNotNull(notes);
            Assert.That(notes.SourceId, Is.EqualTo(-1));


        }

        [Test]
        public void TestNotesConstructor()
        {
            Notes notes = new Notes(-1, 2, "content");
            Assert.IsNotNull(notes);
            Assert.That(notes.NotesId, Is.EqualTo(-1));
            Assert.That(notes.SourceId, Is.EqualTo(2));
            Assert.That(notes.Content, Is.EqualTo("content"));
            Assert.IsTrue(notes.ToString().Contains(notes.Content));


        }
    }
}
