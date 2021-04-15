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
    public class Subscribers
    {       public string ApplicationRef { get; set; }
        public string memo_id { get; set; }
        public string _id { get; set; }
        public string proxyid {get;set;}
        public string id_no { get; set; }
        public string address { get; set; }

        public string surname { get; set; }
        public string names { get; set; }
        public string initials { get; set; }
        public string type { get; set; }
        public string nationality { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string occupation { get; set; }

        public int oshares { get; set; }

        public int pshares { get; set; }

        public string email { get; set; }
        public int IsMember { get; set; }

        public int IsDirector { get; set; }

        public int IsCoSec { get; set; } 
    
 }
}
