using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialLacasa.Models
{
    public class MassOrder
    {
        public int ServiceId { get; set; }
        public string Link { get; set; }
        public int Quantity { get; set; }
        public decimal Charge { get; set; }
    }
}