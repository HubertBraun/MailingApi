using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class MailGroupContext : DbContext
    {
        public int Id { get; set; }
        public DbSet<MailConsumer> Consumers { get; set; }
        public string GroupName { get; set; }
        public MailUser GroupOwner { get; set; } 
        public MailGroupContext(DbContextOptions options) : base(options)
        {

        }

        public MailGroupContext(DbContextOptions options, DbSet<MailConsumer> consumers, string groupName, MailUser owner) : base(options)
        {
            Consumers = consumers;
            GroupName = groupName;
            GroupOwner = owner;
        }
    }
}
