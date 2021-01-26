using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    /// <summary>
    /// models the feedback from the user
    /// </summary>
    public class mFeedback
    {
        public string id { get; set; }
        public string billercode { get; set; }
        public string feedback { get; set; }
        public string feedbacknumber { get; set; }
        public string noted { get; set; }
        public string customername { get; set; }
        public string customerphone { get; set; }
        public DateTime date { get; set; }
    }
}
