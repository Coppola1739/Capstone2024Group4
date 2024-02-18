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
    [TestFixture]
    public class UserControllerTests
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
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            var controller = new UserController(_dbContext);
            var usersToAdd = new List<User>
            {
                new User { Username = "user1", Password = "password1" },
                new User { Username = "user2", Password = "password2" }
            };

            await _dbContext.Users.AddRangeAsync(usersToAdd);
            await _dbContext.SaveChangesAsync();

            var result = await controller.Get();

            var users = result.Value as List<User>;
            Assert.That(users, Is.Not.Null);
            Assert.That(users.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetUserIdByLogin_ValidCredentials_ReturnsUserId()
        {
            var controller = new UserController(_dbContext);
            var username = "testUser";
            var password = "testPassword";
            var user = new User { Username = username, Password = password };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await controller.getUserIdByLogin(username, password);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(user.UserId));
        }

        [Test]
        public async Task CreateAccount_ValidModel_ReturnsOkResult()
        {
            var controller = new UserController(_dbContext);
            var model = new UserModel { userName = "testUser", passWord = "testPassword" };

            var result = await controller.CreateAccount(model);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Does.Contain("Account successfully created!"));

            var users = await _dbContext.Users.ToListAsync();
            Assert.That(users, Is.Not.Null);
            Assert.That(users.Count, Is.EqualTo(1));
            Assert.That(users[0].Username, Is.EqualTo(model.userName));
            Assert.That(users[0].Password, Is.EqualTo(model.passWord));
        }

        [Test]
        public async Task getUserIdByLogin_InvalidCredentials_ReturnsNotFound()
        {
            var controller = new UserController(_dbContext);
            string invalidUsername = null;
            string invalidPassword = "testPassword";

            var result = await controller.getUserIdByLogin(invalidUsername, invalidPassword);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task CreateAccount_InvalidModel_ReturnsBadRequest()
        {
            var controller = new UserController(_dbContext);
            UserModel invalidModel = null;

            var result = await controller.CreateAccount(invalidModel);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public async Task getUserIdByLogin_InternalServerError_ReturnsInternalServerError()
        {
            var controller = new UserController(_dbContext);

            _dbContext.Users = null;

            var result = await controller.getUserIdByLogin("testUser", "testPassword");

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
        [Test]
        public async Task CreateAccount_InternalServerError_ReturnsInternalServerError()
        {
            var controller = new UserController(_dbContext);
            var model = new UserModel { userName = "testUser", passWord = "testPassword" };

            _dbContext.Users = null;

            var result = await controller.CreateAccount(model);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task Get_InternalServerError_ReturnsInternalServerError()
        {
            var controller = new UserController(_dbContext);
            _dbContext.Users = null;

            var result = await controller.Get();

            Assert.That(result.Result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = (ObjectResult)result.Result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));

            var errorMessage = statusCodeResult.Value.ToString();
            Assert.That(errorMessage, Is.EqualTo("{ Message = Internal Server Error }"));
        }

        [Test]
        public async Task GetUserIdByLogin_FoundUserIsNull_ReturnsNotFound()
        {
            var controller = new UserController(_dbContext);

            var result = await controller.getUserIdByLogin("nonexistentUser", "nonexistentPassword");

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetUserIdByLogin_FoundUserIsEmpty_ReturnsNotFound()
        {
            var controller = new UserController(_dbContext);

            var result = await controller.getUserIdByLogin("emptyUser", "emptyPassword");

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
