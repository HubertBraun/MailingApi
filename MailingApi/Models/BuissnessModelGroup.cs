using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BuissnessModelGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<BuissnessModelEmails> Emails { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }

        public BuissnessModelGroup()
        {

        }

        public BuissnessModelGroup(IEnumerable<MailConsumer> consumers, MailingGroup group, MailUser user)
        {
            Emails = consumers.Select(x => new BuissnessModelEmails() { Email = x.ConsumerAddress, EmailId = x.ConsumerId }).ToList();
            GroupId = group.Id;
            OwnerId = group.GroupOwnerId;
            GroupName = group.Name;
            OwnerName = user.Username;
        }
    }
}
