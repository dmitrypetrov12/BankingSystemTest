using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingSystemTest
{
    public class JWTAuth
    {
        public static string GenerateJwtToken(IConfiguration config, string userId, string userName)
        {
            // Create the claims for the token, including the user's name and any roles or permissions they have
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            // Create the security key for the token, using the secret key stored in your app's configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]));

            // Create the signing credentials for the token, using the HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                //issuer: config["JwtSettings:Issuer"],
                //audience: config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            // Serialize the token to a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GetToken(HttpRequest request)
        {
            return request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }
        public static string AuthorizeJwtToken(IConfiguration config, string token)
        {            
            // Create a new instance of JwtSecurityTokenHandler
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Set the parameters for token validation
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Check if the token issuer is valid
                ValidateAudience = false, // Check if the token audience is valid
                ValidateIssuerSigningKey = true, // Check if the token signing key is valid
                ValidateLifetime = true // Check if the token has expired
            };

            // Set the signing key for token validation
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]));

            try
            {
                // Validate the token
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                // If the token is valid, you can access the claims in the token
                return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            catch (SecurityTokenException ex)
            {
                return null;
            }
        }
    }
}
