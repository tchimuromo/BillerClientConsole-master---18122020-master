using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class MemoViewModel
    {
        //int step, string liab, string shareClo, string applicationRef
        public int step { get; set; }
        public string shareClo { get; set; }
        public string applicationRef { get; set; }
        public string liab { get; set; }
    }
}
