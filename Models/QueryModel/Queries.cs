using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models.QueryModel
{
    public class Queries
    {
        [Key]
        public Guid QueryID { get; set; }
        public string applicationID { get; set; }
        public string applicationRef { get; set; }
        public string comment { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string dateCreated { get; set; }
        public string tableName { get; set; }
        public string emailAddress { get; set; }
        public string officeid { get; set; }
        public bool HasQuery { get; set; }
        public int QueryCount { get; set; }
    }
}
