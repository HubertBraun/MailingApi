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
        private AuthenticationLayer _layer;
        public UserAuthenticationService(IOptions<AppSettings> appSettings, MailingApiContext context)
        {
            _appSettings = appSettings.Value;
            _layer = new AuthenticationLayer(new DataLayer(context));
        }

        public BusinessModelUser Authenticate(string username, string password)
        {
            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(username, password);
            var user = _layer.GetUserWithoutPassword(username, hash);
            
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenDurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

    }
}
