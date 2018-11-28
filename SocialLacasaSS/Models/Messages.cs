using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialLacasa.Models
{
    public class Messages
    {

        public string Subject { get; set; }
        public string TicketMessage { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public DateTime? MessageDate { get; set; }
        public bool SentByCustomer { get; set; }
        public int TicketId { get; set; }

    }
}