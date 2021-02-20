using MailingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Layers
{
    /// <summary>
    /// Business logic layer
    /// </summary>
    public class BusinessLayer
    {
        private readonly DataLayer _data;
        public BusinessLayer(DataLayer data)
        {
            _data = data;
        }
        public BusinessModelGroup GetBusinessModel(int groupId)
        {
            var group = _data.SelectGroupById(groupId);
            if (group != null)
            {
                var consumers = _data.SelectConsumersByGroupId(groupId);
                var model = new BusinessModelGroup(consumers, group);
                return model;
            }
            return null;
        }

        public int SaveBusinessModelGroup(BusinessModelGroup model)
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
            return _data.InsertGroup(group, consumers);
        }

        public bool DeleteBusinessModelGroup(int groupId)
        {
            return _data.DeleteGroup(groupId);
        }

        public bool PutBusinessModelGroup(BusinessModelGroup model)
        {
            var group = new MailingGroup
            {
                Id = model.Id,
                GroupOwnerId = model.GroupOwnerId,
                Name = model.GroupName
            };
            var consumers = model.Emails
                .Select(x => new MailConsumer { ConsumerAddress = x.Email, GroupId = model.Id }).ToList();
            return _data.UpdateGroup(model.Id, group, consumers);
        }

        public int RegisterUser(BusinessModelUser user) // TODO: Identity
        {
            var owner = new MailUser()
            {
                Password = user.Password,
                Username = user.Username
            };
            return _data.InsertUser(owner);
        }

    }
}
