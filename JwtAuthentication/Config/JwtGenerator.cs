using MicroServices.Shared.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Config
{
    public class JwtGenerator
    {
        public string GenerateTokenString()
        {
            var handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfig.TOKENSECRET));
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, "teste@teste")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };
            var tokenString = handler.CreateToken(descriptor);
            var token = handler.WriteToken(tokenString);
            return token;
        }
    }
}
