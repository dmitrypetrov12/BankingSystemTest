using BankingSystemTest.Areas.BankSystemAPI.Models;

namespace BankingSystemTest.Services
{
    //
    // Summary:
    //      Instance of the IAuthenticationService useing in-memory data
    public class AuthenticationService : IAuthenticationService
    {
        private List<User> _users;
        private Dictionary<string, string> _passwords;
        public AuthenticationService() 
        {
            //Default data
            _users = new List<User>() { new User() { Id = "testUser", Email = "testUser@gmail.com", FirstName = "Mickey", LastName = "Mouse" } };
            _passwords = new Dictionary<string, string>() { { "testUser", "p" } };
        }
        public void CreateUser(SignupDto signupDto)
        {
            // validate request
            if (string.IsNullOrEmpty(signupDto.UserId) || string.IsNullOrEmpty(signupDto.Password))
            {
                throw new AuthenticationException("User ID and password are required.");
            }

            if (_users.Any((u) => u.Id == signupDto.UserId))
            {
                throw new AuthenticationException("User ID already exists.");
            }

            _users.Add(new User() { Id = signupDto.UserId, FirstName = signupDto.FirstName, LastName = signupDto.LastName, Email = signupDto.Email});
            _passwords.Add(signupDto.UserId, signupDto.Password);
        }
        public User Login(LoginDto userDto)
        {
            var user = GetUserInfo(userDto.UserId);
            if (!_passwords.ContainsKey(user.Id) || _passwords[user.Id] != userDto.Password) 
            {
                throw new AuthenticationException("Password is invalid");
            }
            return user;
        }

        private User GetUserInfo(string userId)
        {
            var user = _users.FirstOrDefault((u) => u.Id == userId);
            if (user == null)
            {
                throw new AuthenticationException("User ID not found.");
            }
            return user;
        }
    }
}
