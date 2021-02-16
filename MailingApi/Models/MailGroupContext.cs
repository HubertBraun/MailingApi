using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class MailGroupContext : DbContext
    {
        public DbSet<MailConsumer> Consumers { get; set; }
        public DbSet<MailingGroup> Groups { get; set; }
        public DbSet<MailUser> GroupOwners { get; set; }
        public MailGroupContext(DbContextOptions options) : base(options)
        {

        }

        public MailGroupContext(DbContextOptions options, DbSet<MailConsumer> consumers, DbSet<MailingGroup> groups, DbSet<MailUser> groupOwners) : base(options)
        {
            Consumers = consumers;
            Groups = groups;
            GroupOwners = groupOwners;
        }
    }
}
