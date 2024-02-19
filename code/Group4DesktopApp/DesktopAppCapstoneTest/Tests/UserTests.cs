using NUnit.Framework;
using Group4DesktopApp;
using Group4DesktopApp.UserControls;
using System.Security.Cryptography.X509Certificates;
using Group4DesktopApp.Model;

namespace DesktopAppCapstoneTest.Tests
{
    public class UserTests
    {

        [Test]
        public void TestGetUserID()
        {
            User user = new User(1, "Jeffrey353");
            Assert.That(user.UserId, Is.EqualTo(1));


        }

        [Test]
        public void TestGetUserName()
        {
            User user = new User(1, "Jeffrey353");
            Assert.That(user.UserName, Is.EqualTo("Jeffrey353"));


        }
    }
}