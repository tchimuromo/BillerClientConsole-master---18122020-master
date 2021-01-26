using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    //models the offline billers products
    public class mProductOffline
    {
        public int id { get; set; }
        public string productcode { get; set; }

        public decimal price { get; set; }
        public string description { get; set; }

        public string image_url { get; set; }

        public bool available { get; set; }

        public string name { get; set; }
        
        public string billercode { get; set; }
        public bool disabled { get; set; }
    }
}
