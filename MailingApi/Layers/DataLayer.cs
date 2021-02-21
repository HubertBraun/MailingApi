using MailingApi.Interfaces;
using MailingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Layers
{
    /// <summary>
    /// Data Layer
    /// </summary>
    public class DataLayer : IDataLayer
    {
        private MailingApiContext _context;
        public DataLayer(MailingApiContext context)
        {
            _context = context;
        }
        public IEnumerable<MailingGroup> SelectGroupsByOwnerId(int ownerId)
        {
            return _context.Groups.Where(x => x.GroupOwnerId == ownerId).ToList();
        }

        public MailingGroup SelectGroupById(int groupId)
        {
            return _context.Groups.Where(x => x.Id == groupId).FirstOrDefault();
        }

        public MailUser SelectOwnerById(int ownerId)
        {
            return _context.GroupOwners.Where(x => x.Id == ownerId).FirstOrDefault();
        }

        public IEnumerable<MailConsumer> SelectConsumersByGroupId(int groupId)
        {
            return _context.Consumers.Where(x => x.GroupId == groupId).OrderBy(x => x.Id).ToList();
        }

        public MailUser SelectUser(string username)
        {
            return _context.GroupOwners.Where(x => x.Username == username).FirstOrDefault();
        }

        public int InsertGroup(MailingGroup group, IEnumerable<MailConsumer> consumers)
        {
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
                return group.Id;
            }
            catch(Exception e) // TODO: Logging
            {
                return -1;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {
                var group = SelectGroupById(groupId);
                if (group != null)
                {
                    var consumers = SelectConsumersByGroupId(groupId);
                    using var transaction = _context.Database.BeginTransaction();
                    _context.Groups.Remove(group);
                    _context.Consumers.RemoveRange(consumers);
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

        public bool UpdateGroup(int groupId, MailingGroup newGroup, IEnumerable<MailConsumer> newConsumers)
        {
            try
            {
                var group = SelectGroupById(groupId);
                if (group != null)
                {
                    using var transaction = _context.Database.BeginTransaction();
                    var consumers = SelectConsumersByGroupId(groupId);
                    _context.Consumers.RemoveRange(consumers);
                    _context.Consumers.AddRange(newConsumers);
                    group.GroupOwnerId = newGroup.GroupOwnerId;
                    group.Name = newGroup.Name;
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

        public int InsertUser(MailUser owner)
        {
            try
            {
                _context.GroupOwners.Add(owner);
                _context.SaveChanges();
                return owner.Id;
            }
            catch (Exception e) // TODO: Logging
            {
                return -1;
            }
        }
    }
}
