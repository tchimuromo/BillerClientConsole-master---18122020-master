using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class Subscriber
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string Date { get; set; }
        public int OrdinaryShares { get; set; }
        public int PreferenceShares { get; set; }
        public int TotalShares { get; set; }
    }
}
