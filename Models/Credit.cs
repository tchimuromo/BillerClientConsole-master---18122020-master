using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class Credit
    {
        public string UserId { get; set; }
        public string CreditId { get; set; }
        public string Service { get; set; }
        public int Count { get; set; }
    }
}
