using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Dtos
{
    public class Query
    {
        public string ApplicationRef { get; set; }
        public bool HasQuery { get; set; }
        public string Comment { get; set; }
    }
}
