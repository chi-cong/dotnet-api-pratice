using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace dotnet_api.Services.SecurityService
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] Key;
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
        }

        public string generateToken(string userName)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, userName )
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor() { Subject = new ClaimsIdentity(claims), Expires = DateTime.UtcNow.AddMinutes(180), SigningCredentials = creds };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token.ToString();
        }
    }
}
