using Demo.Core.Models.Identity;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> _userManager)
        {

            var authClaims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("DisplayName",user.DisplayName),
                new Claim("MobilePhone" ,user.PhoneNumber),
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                authClaims.Add(new Claim("Role", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"])); 

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


