using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Models
{
    [Table("MailConsumer")]
    public class MailConsumer
    {
        [Key]
        [Column("con_id")]
        public string ConsumerId { get; set; }
        [Column("con_address")]
        public string ConsumerAddress { get; set; }
        
    }

}
