using BankingSystemTest.Areas.BankSystemAPI.Models;

namespace BankingSystemTest.Services
{
    public interface IAuthenticationService
    {
        //
        // Summary:
        //     Creates a User from SignupDto.
        public void CreateUser(SignupDto signupDto);
        //
        // Summary:
        //     Validates User Id and Password and return User if found, otherwise throw an exception.
        public User Login(LoginDto userDto);
    }
}
