﻿using MailingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BuissnessModelEmails : IDentifier
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
