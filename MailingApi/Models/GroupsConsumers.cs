using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    [Table("GroupsConsumers")]
    public class GroupsConsumers
    {
        [Key]
        [Column("gc_id")]
        public int GroupsEmailsId { get; set; }
        [Column("gc_groupId")]
        public int GroupId { get; set; }
        [Column("gc_consumerId")]
        public int ConsumerId { get; set; }
    }
}
