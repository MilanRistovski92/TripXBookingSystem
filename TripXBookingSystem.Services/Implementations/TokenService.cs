using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string GenerateToken()
        {
            var key = "YourSecretKeyHere1234567891234567891234";
            var issuer = "http://localhost:44380";
            var audience = "TestAudience";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "user_id"),
                    new Claim(JwtRegisteredClaimNames.Email, "example@example.com")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
