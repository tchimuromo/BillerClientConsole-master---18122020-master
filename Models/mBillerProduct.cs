using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mBillerProduct
    {

        public string id { get; set; }

        public decimal price { get; set; }
        public string description { get; set; }

        public string image_url { get; set; }

        public bool available { get; set; }

        public string name { get; set; }




    }
}
