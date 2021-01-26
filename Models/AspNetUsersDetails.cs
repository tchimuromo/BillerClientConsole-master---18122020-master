using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BillerClientConsole.Models
{
    public partial class AspNetUsersDetails
    {
       

         public string Id { get; set; }
        public string email { get; set; }
            public string natid { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; } 
           public int role { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string UserId { get; set; }
        //  public int Role { get; set; }
            public string roleName{ get; set; }

    }
}


