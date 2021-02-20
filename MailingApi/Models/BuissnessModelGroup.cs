using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    public class BuissnessModelGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<BuissnessModelEmails> Emails { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        [JsonIgnore]
        public string OwnerPassword { get; set; }
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
            OwnerName = user.Password;
        }
    }
}
