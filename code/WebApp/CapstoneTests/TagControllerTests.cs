using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class TagControllerTests
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
            var noteTags = new List<NoteTags>
            {
                new NoteTags { NotesId = 1, TagName = "Tag1" },
                new NoteTags { NotesId = 1, TagName = "Tag2" }
            };

            await _dbContext.NoteTags.AddRangeAsync(noteTags);
            await _dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task GetTagByNotesID_ExistingNotesId_ReturnsOkResult()
        {
            await SeedDatabaseAsync();

            var controller = new TagController(_dbContext);
            var notesId = 1;

            var result = await controller.GetTagByNotesID(notesId);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var tags = okResult.Value as List<string>;
            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task AddTag_ValidModel_ReturnsOkResult()
        {
            var controller = new TagController(_dbContext);
            var model = new NoteTags { NotesId = 1, TagName = "NewTag" };

            var result = await controller.AddTag(model);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Is.EqualTo("{ Message = Tag added successfully }"));

            var addedTag = await _dbContext.NoteTags.FirstOrDefaultAsync(nt => nt.NotesId == model.NotesId && nt.TagName == model.TagName);
            Assert.That(addedTag, Is.Not.Null);
            Assert.That(addedTag.TagName, Is.EqualTo(model.TagName));
        }

        [Test]
        public async Task AddTag_DuplicateTag_ReturnsBadRequest()
        {
            await SeedDatabaseAsync();

            var controller = new TagController(_dbContext);
            var model = new NoteTags { NotesId = 1, TagName = "Tag1" };

            var result = await controller.AddTag(model);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value.ToString(), Is.EqualTo("{ Message = Tag already exists for the note }"));
        }

        [Test]
        public async Task RemoveTag_ExistingTagAndNote_ReturnsOkResult()
        {
            await SeedDatabaseAsync();

            var controller = new TagController(_dbContext);
            var tagName = "Tag1";
            var notesId = 1;

            var result = await controller.RemoveTag(tagName, notesId);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Is.EqualTo("{ Message = Tag removed successfully }"));

            var removedTag = await _dbContext.NoteTags.FirstOrDefaultAsync(nt => nt.NotesId == notesId && nt.TagName == tagName);
            Assert.That(removedTag, Is.Null);
        }

        [Test]
        public async Task RemoveTag_TagNotFound_ReturnsNotFound()
        {
            var controller = new TagController(_dbContext);
            var tagName = "NonExistingTag";
            var notesId = 1;

            var result = await controller.RemoveTag(tagName, notesId);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.Value.ToString(), Is.EqualTo("{ Message = Tag not found for the note }"));
        }

        [Test]
        public async Task RemoveTag_InvalidTagName_ReturnsBadRequest()
        {
            var controller = new TagController(_dbContext);
            string invalidTagName = null;
            var notesId = 1;

            var result = await controller.RemoveTag(invalidTagName, notesId);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value.ToString(), Is.EqualTo("{ Message = Invalid tag name }"));
        }
    }
}
