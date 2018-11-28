using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialLacasa.Models
{
    public class Tickets
    {

        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string TicketMessage { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}