using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtAuthentication.Config
{
    public static class JwtGenerator
    {
        public static string GenerateTokenString()
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, "teste@teste")
            };
            var securityToken = new JwtSecurityToken(claims:claims);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
