using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mServiceEnquiry
    {
        public int id { get; set; }
        public string payerName { get; set;}
        public string phoneNumber { get; set; }
        public string billercode { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public bool isRead { get; set; }
    }

    /// <summary>
    /// answers for the above sevice enquiry
    /// </summary>
    public class mEnquiryAnswer
    {
        public int id { get; set; }
        public int questionid { get; set; }
        public string question { get; set; }
        public string answer { get; set; }

    }

    //questions set by biller to be answers by the client
    public class mEnquiryQuestions
    {
        public int id { get; set; }
        public string question { get; set; }
        public string billercode { get; set; }
        public bool status { get; set; }
    }




   

}
