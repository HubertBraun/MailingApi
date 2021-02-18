using MailingApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailingApi.Context
{
    public class MailingApiContext : DbContext
    {
        public DbSet<MailConsumer> Consumers { get; set; }
        public DbSet<MailingGroup> Groups { get; set; }
        public DbSet<MailUser> GroupOwners { get; set; }
        public MailingApiContext(DbContextOptions options) : base(options)
        {

        }

        public MailingApiContext(DbContextOptions options, DbSet<MailConsumer> consumers, DbSet<MailingGroup> groups, DbSet<MailUser> groupOwners) : base(options)
        {
            Consumers = consumers;
            Groups = groups;
            GroupOwners = groupOwners;
        }

        private int AddAndSave(object o)
        {
            Add(o);
            return SaveChanges();
        }


        public MailConsumer GetConsumer(int id)
        {
            return Consumers.Where(x => x.ConsumerId == id).FirstOrDefault();
        }

        public MailingGroup GetGroup(int id)
        {
            return Groups.Where(x => x.Id == id).FirstOrDefault();
        }

        public MailUser GetUser(int id)
        {
            return GroupOwners.Where(x => x.Id == id).FirstOrDefault();
        }

        public BuissnessModelGroup GetBuissnesModelGroup(int groupId)
        {
            var group = Groups.Where(x => x.Id == groupId).FirstOrDefault();
            if (group != null)
            {
                var owner = GroupOwners.Where(x => x.Id == group.GroupOwnerId).FirstOrDefault();
                var consumers = Consumers.Where(x => x.GroupId == groupId);
                var model = new BuissnessModelGroup(consumers, group, owner);
                return model;
            }
            return null;
        }

    }
}
