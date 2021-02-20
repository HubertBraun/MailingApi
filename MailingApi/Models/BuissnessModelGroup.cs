using MailingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BuissnessModelGroup : IDentifier, IGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<BuissnessModelEmails> Emails { get; set; }
        public int GroupOwnerId { get; set; }

        public BuissnessModelGroup()
        {

        }

        public BuissnessModelGroup(IEnumerable<MailConsumer> consumers, MailingGroup group)
        {
            Id = group.Id;
            Emails = consumers.Select(x => new BuissnessModelEmails() { Email = x.ConsumerAddress, Id = x.Id }).ToList();
            GroupOwnerId = group.GroupOwnerId;
            GroupName = group.Name;
        }
    }
}
