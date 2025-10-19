using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UnApIFood.Models;

namespace UnApIFood.Utils
{
    public static class JWTUtil
    {
        public static string GenerateJWT(Login login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtil.JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("active", "1"),
                new Claim("guid", Guid.NewGuid().ToString()),
                new Claim("rId", "Admin")
            };

            var token = new JwtSecurityToken(ConfigUtil.JWTIssuer,
                ConfigUtil.JWTAudience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
