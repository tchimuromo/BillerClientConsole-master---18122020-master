using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class shareClause
    {
        public string _id { get; set; }

        public string description { get; set; }

        public string memo_id { get; set; }

        public bool HasQuery { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public string dateCreated { get; set; }
        public string dateUpdate { get; set; }
    }
}
