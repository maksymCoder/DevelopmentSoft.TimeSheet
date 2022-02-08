using NUnit.Framework;
using TimeSheet.App.Services;

namespace TimeSheet.Tests
{
    public class AuthServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("������")]
        [TestCase("������")]
        [TestCase("�������")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            
            var service = new AuthService();
                
            var result = service.Login(lastName);

            Assert.IsNotNull(UserSession.Sessions);
            Assert.IsNotEmpty(UserSession.Sessions);
            Assert.IsTrue(UserSession.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }
        [Test]
        public void Login_InvokeLoginTwiceForOneLastName_ShouldReturnTrue()
        {
            var lastName = "������";
            var service = new AuthService();

            var result = service.Login(lastName);
            result = service.Login(lastName);

            Assert.IsNotNull(UserSession.Sessions);
            Assert.IsNotEmpty(UserSession.Sessions);
            Assert.IsTrue(UserSession.Sessions.Contains(lastName)); 
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("TestUser")]
        public void Login_ShouldReturnFalse(string lastName)
        {
            
            var service = new AuthService();

            var result = service.Login(lastName);

            Assert.IsFalse(result);
            
        }

    }
}