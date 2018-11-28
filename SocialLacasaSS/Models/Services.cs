using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialLacasa.Models
{
    public class Services
    {
        public int SWserviceId { get; set; }
        public string ServiceType { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int MinOrder { get; set; }
        public int MaxOrder { get; set; }
        public int CatagoryId { get; set; }
        public decimal Rate { get; set; }

    }
}