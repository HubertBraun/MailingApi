using MailingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BusinessModelEmails : IDentifier
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
