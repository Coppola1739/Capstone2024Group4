using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Server.Controllers;
using WebApp.Server.Data;
using WebApp.Server.Models;

namespace CapstoneTests
{
    public class NotesControllerTests
    {
        private CapstoneDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CapstoneDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new CapstoneDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        private async Task SeedDatabaseAsync()
        {
            var sourceId = 1;

            // Seed Source
            var source = new Source
            {
                SourceId = sourceId,
                UserId = 1, // Provide appropriate user ID
                SourceName = "Example Source",
                UploadDate = DateTime.UtcNow,
                Content = Encoding.UTF8.GetBytes("Example Source Content"),
                SourceType = "Example Type",
                AuthorFirstName = "John",
                AuthorLastName = "Doe",
                Title = "Example Title"
            };

            // Seed Notes
            var notes = new List<Notes>
            {
                new Notes { NotesId = 1, SourceId = sourceId, Content = "Note 1 content" },
                new Notes { NotesId = 2, SourceId = sourceId, Content = "Note 2 content" }
            };

            // Seed Tag
            var tag = new Tag
            {
                TagName = "Tag1"
            };

            // Seed NoteTags
            var noteTags = new NoteTags
            {
                TagName = "Tag1",
                NotesId = 1
            };

            // Add entities to DbContext and save changes
            await _dbContext.Source.AddAsync(source);
            await _dbContext.Notes.AddRangeAsync(notes);
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.NoteTags.AddAsync(noteTags);

            await _dbContext.SaveChangesAsync();
        }


        [Test]
        public async Task GetNotesBySourceId_ExistingSourceId_ReturnsOkResult()
        {
            await SeedDatabaseAsync();

            var controller = new NotesController(_dbContext);
            var sourceId = 1;

            var result = await controller.GetNotesBySourceId(sourceId);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var retrievedNotes = okResult.Value as List<Notes>;
            Assert.That(retrievedNotes, Is.Not.Null);
            Assert.That(retrievedNotes.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetNotesBySourceId_ExistingSourceId_ReturnsInternalServerError()
        {
            await SeedDatabaseAsync();

            var controller = new NotesController(_dbContext);
            var sourceId = 1;

            _dbContext.Notes = null;
            var result = await controller.GetNotesBySourceId(sourceId);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task AddNote_ValidModel_ReturnsOkResult()
        {
            var controller = new NotesController(_dbContext);
            var addNoteModel = new AddNoteModel { SourceId = 1, Content = "New note content" };

            var result = await controller.AddNote(addNoteModel);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Is.EqualTo("{ Message = Note added successfully }"));

            var newNote = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Content == addNoteModel.Content);
            Assert.That(newNote, Is.Not.Null);
            Assert.That(newNote.Content, Is.EqualTo(addNoteModel.Content));
        }

        [Test]
        public async Task AddNote_ValidModel_ReturnsInternalServerError()
        {
            var controller = new NotesController(_dbContext);
            var addNoteModel = new AddNoteModel { SourceId = 1, Content = "New note content" };
            _dbContext.Notes = null;
            var result = await controller.AddNote(addNoteModel);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task AddNote_InvalidModel_ReturnsBadRequest()
        {
            var controller = new NotesController(_dbContext);
            AddNoteModel invalidModel = null;

            var result = await controller.AddNote(invalidModel);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task UpdateNote_ExistingNoteId_ReturnsOkResult()
        {
            // Arrange
            var noteId = 1;
            var existingNote = new Notes { NotesId = noteId, SourceId = 1, Content = "Original content" };
            _dbContext.Notes.Add(existingNote);
            await _dbContext.SaveChangesAsync();

            var controller = new NotesController(_dbContext);
            var updatedContent = "Updated content";

            // Act
            var result = await controller.UpdateNote(noteId, updatedContent);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Is.EqualTo("{ Message = Note updated successfully }"));

            var updatedNote = await _dbContext.Notes.FindAsync(noteId);
            Assert.That(updatedNote, Is.Not.Null);
            Assert.That(updatedContent, Is.EqualTo(updatedNote.Content));
        }

        [Test]
        public async Task DeleteNote_ExistingNoteId_ReturnsOkResult()
        {
            // Arrange
            var noteId = 1;
            var existingNote = new Notes { NotesId = noteId, SourceId = 1, Content = "Note content" };
            _dbContext.Notes.Add(existingNote);
            await _dbContext.SaveChangesAsync();

            var controller = new NotesController(_dbContext);

            // Act
            var result = await controller.DeleteNote(noteId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Is.EqualTo("{ Message = Note deleted successfully }"));

            var deletedNote = await _dbContext.Notes.FindAsync(noteId);
            Assert.That(deletedNote, Is.Null);
        }

        [Test]
        public async Task DeleteNote_NonExistingNoteId_ReturnsNotFound()
        {
            var nonExistingNoteId = 9999;
            var controller = new NotesController(_dbContext);

            var result = await controller.DeleteNote(nonExistingNoteId);
            
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }


        [Test]
        public async Task UpdateNote_ExceptionThrown_ReturnsInternalServerError()
        {
            var noteId = 1;
            var existingNote = new Notes { NotesId = noteId, SourceId = 1, Content = "Original content" };
            _dbContext.Notes.Add(existingNote);
            await _dbContext.SaveChangesAsync();

            var controller = new NotesController(_dbContext);

            var updatedContent = "Updated content";

            _dbContext.Notes = null;

            var result = await controller.UpdateNote(noteId, updatedContent);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task DeleteNote_ExceptionThrown_ReturnsInternalServerError()
        {
            var noteId = 1;
            var existingNote = new Notes { NotesId = noteId, SourceId = 1, Content = "Note content" };
            _dbContext.Notes.Add(existingNote);
            await _dbContext.SaveChangesAsync();

            var controller = new NotesController(_dbContext);

            _dbContext.Notes = null;

            var result = await controller.DeleteNote(noteId);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));

        }

            [Test]
            public async Task GetNotesByTag_ExistingTag_ReturnsOkResult()
            {
                // Arrange
                await SeedDatabaseAsync();
                var controller = new NotesController(_dbContext);
                var tagName = "Tag1";

                // Act
                var result = await controller.GetNotesByTag(tagName);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);

                var notes = okResult.Value as List<Notes>;
                Assert.That(notes, Is.Not.Null);
                Assert.That(notes.Count, Is.EqualTo(1));
            }

            [Test]
            public async Task GetNotesByTag_TagNotFound_ReturnsEmptyList()
            {
                // Arrange
                await SeedDatabaseAsync();
                var controller = new NotesController(_dbContext);
                var tagName = "NonExistingTag";

                // Act
                var result = await controller.GetNotesByTag(tagName);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);

                var notes = okResult.Value as List<Notes>;
                Assert.That(notes, Is.Not.Null);
                Assert.That(notes.Count, Is.EqualTo(0));
            }
            [Test]
            public async Task GetSourceByNoteId_ExistingNoteId_ReturnsOkResult()
            {
                // Arrange
                await SeedDatabaseAsync();
                var controller = new NotesController(_dbContext);
                var noteId = 1;

                // Act
                var result = await controller.GetSourceByNoteId(noteId);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null, "OkObjectResult is null");

                if (okResult != null)
                {
                    var source = okResult.Value as Source;
                    Assert.That(source, Is.Not.Null, "Source object is null");

                    // Add additional assertion for source properties if necessary

                    Assert.That(source.SourceId, Is.EqualTo(1), "SourceId does not match expected value");
                }

                // Add logging to examine the result and source object
                Console.WriteLine($"Result: {result}");
            }


        [Test]
            public async Task GetSourceByNoteId_NoteIdNotFound_ReturnsNotFound()
            {
                // Arrange
                await SeedDatabaseAsync();
                var controller = new NotesController(_dbContext);
                var nonExistingNoteId = 9999;

                // Act
                var result = await controller.GetSourceByNoteId(nonExistingNoteId);

                // Assert
                Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
                var notFoundResult = result as NotFoundObjectResult;
                Assert.That(notFoundResult.Value.ToString(), Is.EqualTo("{ Message = Source not found for the provided note ID }"));
            }

    }
}
