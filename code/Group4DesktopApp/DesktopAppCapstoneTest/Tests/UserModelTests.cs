using NUnit.Framework;
using Group4DesktopApp;
using System.Security.Cryptography.X509Certificates;
using Group4DesktopApp.Model;

namespace DesktopAppCapstoneTest.Tests
{
    /// <summary>
    /// Test Class for all the User Model Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class UserModelTests
    {
        /// <summary>
        /// Tests User Model Getting User ID.
        /// </summary>
        [Test]
        public void TestGetUserID()
        {
            User user = new User(1, "Jeffrey353");
            Assert.That(user.UserId, Is.EqualTo(1));


        }

        /// <summary>
        /// Tests the User Model Getting username.
        /// </summary>
        [Test]
        public void TestGetUserName()
        {
            User user = new User(1, "Jeffrey353");
            Assert.That(user.UserName, Is.EqualTo("Jeffrey353"));


        }
    }
}