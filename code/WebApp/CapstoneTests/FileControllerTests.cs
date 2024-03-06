using NUnit.Framework;
using NUnit.Compatibility;
using Microsoft.AspNetCore.Mvc;
using WebApp.Server.Controllers;
using WebApp.Server.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Models;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using NUnit.Framework.Internal;
using Microsoft.AspNetCore.Http;

namespace CapstoneTests
{
    [TestFixture]
    public class FileControllerTests
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

        [Test]
        public async Task UploadVideo_ValidModel_ReturnsOkResult()
        {
            var model = new VideoUploadModel
            {
                VideoLink = "testlink.com",
                SourceName = "Test",
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                Title = "Test",
                SourceType = "video"
            };
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            var result = await controller.UploadVideo(model);

            var okResult = result as OkObjectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That("{ Message = Video link uploaded successfully }", Is.EqualTo(okResult.Value.ToString()));
        }
        [Test]
        public async Task UploadVideo_ValidModel_ReturnsInternalServerError()
        {
            var model = new VideoUploadModel
            {
                VideoLink = "testlink.com",
                SourceName = "Test",
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                Title = "Test",
                SourceType = "video"
            };
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);
            _dbContext.Source = null;
            var result = await controller.UploadVideo(model);
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task UploadPdf_ValidModel_ReturnsOkResult()
        {
            var pdfContent = Encoding.UTF8.GetBytes("Test PDF Content");
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(pdfContent.Length);
            formFileMock.Setup(f => f.FileName).Returns("test.pdf");
            formFileMock.Setup(f => f.ContentType).Returns("application/pdf");
            formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(pdfContent));

            var model = new FileUploadModel
            {
                SourceName = "TestSource",
                AuthorFirstName = "TestAuthor",
                AuthorLastName = "TestAuthor",
                Title = "TestTitle",
                SourceType = "pdf",
                PdfFile = formFileMock.Object
            };

            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            var result = await controller.UploadPdf(model);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That("{ Message = PDF uploaded successfully }", Is.EqualTo(okResult.Value.ToString()));

            var savedSource = await dbContext.Source.FirstOrDefaultAsync();
            Assert.That(savedSource, Is.Not.Null);
            Assert.That(savedSource.SourceName, Is.EqualTo(model.SourceName));
        }


        [Test]
        public async Task GetUsersSources_ValidUserId_ReturnsOkResult()
        {
            var dbContext = _dbContext;
            dbContext.Source.Add(new Source
            {
                UserId = 1,
                SourceName = "Test1",
                UploadDate = DateTime.UtcNow,
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                SourceType = "pdf",
                Title = "Test",
                Content = Encoding.UTF8.GetBytes("Test")
            });
            dbContext.Source.Add(new Source
            {
                UserId = 1, 
                SourceName = "Test2", 
                UploadDate = DateTime.UtcNow, 
                AuthorFirstName = "Test2", 
                AuthorLastName = "Test2", 
                SourceType = "pdf", 
                Title = "Test2", 
                Content = Encoding.UTF8.GetBytes("Test2")
            });
            await dbContext.SaveChangesAsync();

            var controller = new FileController(dbContext);

            
            var result = await controller.GetUsersSources(1);


            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
        }

        [Test]
        public async Task GetSourceById_ValidId_ReturnsOkResult()
        {
            var dbContext = _dbContext;  
            var source = new Source {
                UserId = 1,
                SourceName = "Test1",
                UploadDate = DateTime.UtcNow,
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                SourceType = "pdf",
                Title = "Test",
                Content = Encoding.UTF8.GetBytes("Test")
            };
            dbContext.Source.Add(source);
            await dbContext.SaveChangesAsync();

            var controller = new FileController(dbContext);

            var result = await controller.GetSourceById(1);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var retrievedSource = okResult.Value as Source;
            Assert.That(source.SourceId, Is.EqualTo(retrievedSource.SourceId));
        }

        [Test]
        public async Task UploadVideo_InvalidModel_ReturnsBadRequest()
        {
            var model = new VideoUploadModel();
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            var result = await controller.UploadVideo(model);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public async Task UploadPdf_InvalidModel_ReturnsBadRequest()
        {
            var model = new FileUploadModel();
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            var result = await controller.UploadPdf(model);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public async Task GetSourceById_InvalidId_ReturnsNotFound()
        {
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            var result = await controller.GetSourceById(9999);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetSourceById_InValidId_ReturnsInternalServerError()
        {
            var dbContext = _dbContext;
            var controller = new FileController(dbContext);

            _dbContext.Source = null;
            var result = await controller.GetSourceById(9999);


            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }


        [Test]
        public async Task DeleteSource_ValidId_ReturnsOkResult()
        {
            var sourceId = 1;
            var source = new Source
            {
                SourceId = sourceId,
                UserId = 1,
                SourceName = "Test",
                UploadDate = DateTime.UtcNow,
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                SourceType = "pdf",
                Title = "Test",
                Content = Encoding.UTF8.GetBytes("Test")
            };

            _dbContext.Source.Add(source);
            await _dbContext.SaveChangesAsync();

            var controller = new FileController(_dbContext);

            var result = await controller.DeleteSource(sourceId);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            var deletedSource = await _dbContext.Source.FindAsync(sourceId);
            Assert.That(deletedSource, Is.Null);
        }

        [Test]
        public async Task DeleteSource_InvalidId_ReturnsNotFound()
        {
            var invalidSourceId = 9999;
            var controller = new FileController(_dbContext);

            var result = await controller.DeleteSource(invalidSourceId);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());

            var existingSource = await _dbContext.Source.FindAsync(invalidSourceId);
            Assert.That(existingSource, Is.Null);
        }


        [Test]
        public async Task DeleteSource_DbContextIsNull_ReturnsInternalServerError()
        {
            var sourceId = 1;
            var source = new Source
            {
                SourceId = sourceId,
                UserId = 1,
                SourceName = "Test",
                UploadDate = DateTime.UtcNow,
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                SourceType = "pdf",
                Title = "Test",
                Content = Encoding.UTF8.GetBytes("Test")
            };

            _dbContext.Source.Add(source);
            await _dbContext.SaveChangesAsync();

            var controller = new FileController(_dbContext);

            _dbContext.Source = null;

            var result = await controller.DeleteSource(sourceId);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }


    }
}
