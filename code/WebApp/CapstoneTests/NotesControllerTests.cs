using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            var notes = new List<Notes>
            {
                new Notes { NotesId = 1, SourceId = sourceId, Content = "Note 1 content" },
                new Notes { NotesId = 2, SourceId = sourceId, Content = "Note 2 content" }
            };

            await _dbContext.Notes.AddRangeAsync(notes);
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
            Assert.That(retrievedNotes.Count, Is.EqualTo(2)); // Assuming two notes were seeded
        }

        /*
        [Test]
        public async Task UpdateNote_ExistingNoteId_ReturnsOkResult()
        {
            var dbContext = CreateMockDbContext();
            var noteId = 1;
            var existingNote = new Notes { NotesId = noteId, SourceId = 1, Content = "Original content" };
            dbContext.Notes.Add(existingNote);
            await dbContext.SaveChangesAsync();

            var controller = new NotesController(dbContext);
            var updatedContent = "Updated content";

            var result = await controller.UpdateNote(noteId, updatedContent);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That("Note updated successfully", Is.EqualTo(okResult.Value.ToString()));

            var updatedNote = await dbContext.Notes.FindAsync(noteId);
            Assert.That(updatedNote, Is.Not.Null);
            Assert.That(updatedContent, Is.EqualTo(updatedNote.Content));
        }
        */

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
        public async Task AddNote_InvalidModel_ReturnsBadRequest()
        {
            var controller = new NotesController(_dbContext);
            AddNoteModel invalidModel = null;

            var result = await controller.AddNote(invalidModel);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

    }
}
