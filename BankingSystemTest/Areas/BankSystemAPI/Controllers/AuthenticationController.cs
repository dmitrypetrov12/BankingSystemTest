using BankingSystemTest.Areas.BankSystemAPI.Models;
using BankingSystemTest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingSystemTest.Areas.BankSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] SignupDto signupDto)
        {
            try
            {
                BankingSystemServiceProvider.AuthenticationService.CreateUser(signupDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto userDto)
        {
            try
            {
                var user = BankingSystemServiceProvider.AuthenticationService.Login(userDto);
                var token = JWTAuth.GenerateJwtToken(_config, user.Id, user.Email);
                return Ok(new {user, token });
            }
            catch (Exception ex)
            {
                return BadRequest("User ID or password is invalid"); // hide the real reason 
            }
        }
    }
}
