using BankingSystemTest;
using BankingSystemTest.Areas.BankSystemAPI.Models;
using BankingSystemTest.Services;

namespace TestProject1
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void SignUpWithEmptyUserID()
        {
            SignupDto signupDto = new SignupDto();
            BankingSystemServiceProvider.AuthenticationService.CreateUser(signupDto);
        }
        
        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void SignUpWithEmptyPassword()
        {
            SignupDto signupDto = new SignupDto() { UserId="test" };
            BankingSystemServiceProvider.AuthenticationService.CreateUser(signupDto);
        }

        [TestMethod]
        public void SignUpAndLogin()
        {
            SignupDto signupDto = new SignupDto() { UserId = "test", Password="password" };
            BankingSystemServiceProvider.AuthenticationService.CreateUser(signupDto);

            LoginDto loginDto = new LoginDto() { UserId = "test", Password = "password" };
            var user = BankingSystemServiceProvider.AuthenticationService.Login(loginDto);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void LoginIncorrectUserId()
        {
            LoginDto loginDto = new LoginDto() { UserId = "IncorrectUserId", Password = "password" };
            var user = BankingSystemServiceProvider.AuthenticationService.Login(loginDto);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void LoginIncorrectPassword()
        {
            LoginDto loginDto = new LoginDto() { UserId = "test", Password = "password" };
            var user = BankingSystemServiceProvider.AuthenticationService.Login(loginDto);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, "test");

            loginDto = new LoginDto() { UserId = "test", Password = "incorrect password" };
            BankingSystemServiceProvider.AuthenticationService.Login(loginDto);
        }
    }
}