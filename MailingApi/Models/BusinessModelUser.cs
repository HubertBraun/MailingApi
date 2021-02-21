using MailingApi.Helper;
using MailingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BusinessModelUser : IDentifier, IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Roles Role { get; set; }
    }
}
