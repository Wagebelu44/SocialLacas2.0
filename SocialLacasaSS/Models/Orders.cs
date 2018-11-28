using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialLacasa.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public decimal Charge { get; set; }
        public int StartCount { get; set; }
        public int Quantity { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public int Remains { get; set; }
    }
}