using MailingApi.Helper;
using MailingApi.Interfaces;
using MailingApi.Layers;
using MailingApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MailingApi.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private AppSettings _appSettings;
        private IAuthenticationLayer _layer;
        public UserAuthenticationService(IOptions<AppSettings> appSettings, IAuthenticationLayer layer)
        {
            _appSettings = appSettings.Value;
            _layer = layer;
        }

        public BusinessModelUser Authenticate(string username, string password)
        {
            var hasher = new PasswordHasher<string>();
            var user = _layer.GetUser(username);
            var verify = hasher.VerifyHashedPassword(username, user.Password, password);
            if(user == null || verify == PasswordVerificationResult.Failed)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.TokenDurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public bool Register(string username, string password)
        {
            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(username, password);
            var user = _layer.RegisterUser(username, hash);
            return user != -1;
        }
    }
}
