using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.JWT
{
    public class JwtGenerator : IJwt
    {
        private readonly IConfiguration _configuration;
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;  
        }
        public string GenerateJwt(RegisteredUser user)
        {
            string employerClaim = user.IsEmployer ? "Employer" : "Applicant";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, employerClaim)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );
            var str = new JwtSecurityTokenHandler().WriteToken(token);
            return str;
        }
    }
}
