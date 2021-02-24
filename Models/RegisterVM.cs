using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class RegisterVM
    {
        [EmailAddress]
       
        public string email { get; set; }
        public string password { get; set; }
        [Required(ErrorMessage = "National id is required")]
        [RegularExpression(@"(^\d{2}) (\d{4,7}) ([A-Z-a-z]{1} (\d{2}$))|(^\d{2})-(\d{4,7})-([A-Z-a-z]{1}-(\d{2}$))", ErrorMessage = "Please enter a valid National Id")]
        public string natid { get; set; }
        public string passwordb { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int role { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }




       // string email, string password, string natid, string passwordb, string firstname,, int role, string address, string city, string country
    }
}
