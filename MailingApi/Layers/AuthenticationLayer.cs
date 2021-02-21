using MailingApi.Interfaces;
using MailingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Layers
{
    public class AuthenticationLayer : IAuthenticationLayer
    {
        private readonly IDataLayer _data;
        public AuthenticationLayer(IDataLayer data)
        {
            _data = data;
        }
        public int RegisterUser(BusinessModelUser user)
        {
            var owner = new MailUser()
            {
                Password = user.Password,
                Username = user.Username
            };
            return _data.InsertUser(owner);
        }

        public BusinessModelUser GetUserWithoutPassword(string name, string hashPassword)
        {
            var user = _data.SelectUser(name, hashPassword);
            var model = new BusinessModelUser
            {
                Id = user.Id,
                Username = user.Username,
                Password = "",
            };
            return model;
        }
    }
}
