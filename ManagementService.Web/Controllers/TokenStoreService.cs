using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using ManagementService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ManagementService.Web.Controllers
{
    public interface ITokenStoreService
    {
        Task<(string AccessToken, List<Claim> claims)> createAccessTokenAsync(User user, List<string> roles);
    }

    public class TokenStoreService : ITokenStoreService
    {
        private IUserService _userService { get; set; }
        public TokenStoreService(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<(string AccessToken, List<Claim> claims)> createAccessTokenAsync(User user,List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, "http://localhost:44330/"),
                new Claim(JwtRegisteredClaimNames.Iss, "http://localhost:44330/", ClaimValueTypes.String, "http://localhost:44330/"),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, "http://localhost:44330/"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, "http://localhost:44330/"),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, "http://localhost:44330/"),
                new Claim("DisplayName", user.Firstname + " " + user.LastName, ClaimValueTypes.String, "http://localhost:44330/"),
                new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(user), ClaimValueTypes.String, "http://localhost:44330/")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, "http://localhost:44330/"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VeryStrongKey#123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: "http://localhost:44330/",
                audience: "http://localhost:44330/",
                claims: claims,
                notBefore: now,
                expires: now.AddDays(30),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }
        
    }
}
