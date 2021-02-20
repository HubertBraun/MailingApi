using MailingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Layers
{
    /// <summary>
    /// Buissnes logic layer
    /// </summary>
    public class BuisnessLayer
    {
        private readonly MailingApiContext _context;
        public BuisnessLayer(MailingApiContext context)
        {
            _context = context;
        }
        public BuissnessModelGroup GetBuissnesModel(int groupId)
        {
            var group = _context.Groups.Where(x => x.Id == groupId).FirstOrDefault();
            if (group != null)
            {
                var owner = _context.GroupOwners.Where(x => x.Id == group.GroupOwnerId).FirstOrDefault();
                var consumers = _context.Consumers.Where(x => x.GroupId == groupId).OrderBy(x => x.Id);
                var model = new BuissnessModelGroup(consumers, group);
                return model;
            }
            return null;
        }

        public int SaveBuissnesModelGroup(BuissnessModelGroup model)
        {
            var group = new MailingGroup
            {
                Name = model.GroupName,
                GroupOwnerId = model.GroupOwnerId
            };
            var consumers = new List<MailConsumer>();
            foreach (var e in model.Emails)
            {
                var consumer = new MailConsumer
                {
                    ConsumerAddress = e.Email,
                    GroupId = group.Id
                };
                consumers.Add(consumer);
            }
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                _context.Groups.Add(group);
                _context.SaveChanges();
                foreach (var c in consumers)
                {
                    c.GroupId = group.Id; // id generated after SaveChanges()
                    _context.Add(c);
                }
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e) // TODO: Logging
            {
                return -1;
            }
            return group.Id;
        }

        public bool DeleteBuissnesModelGroup(int groupId)
        {
            try
            {
                var group = _context.Groups.Where(x => x.Id == groupId).FirstOrDefault();
                if (group != null)
                {
                    var consumers = _context.Consumers.Where(x => x.GroupId == groupId).OrderBy(x => x.Id);
                    using var transaction = _context.Database.BeginTransaction();
                    _context.Groups.Remove(group);
                    foreach (var c in consumers)
                    {
                        _context.Consumers.Remove(c);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception e) // TODO: Logging
            {
                return false;
            }
        }

        public int RegisterUser(BuissnesModelUser user) // TODO: Identity
        {
            var owner = new MailUser()
            {
                Password = user.Password,
                Username = user.Username
            };
            try
            {
                _context.GroupOwners.Add(owner);
                _context.SaveChanges();
            }
            catch (Exception e) // TODO: Logging
            {
                return -1;
            }
            return owner.Id;
        }
    }
}
