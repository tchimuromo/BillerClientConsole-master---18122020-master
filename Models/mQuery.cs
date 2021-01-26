using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{

    public class OfficeQueryModel
    {
        public string comment { get; set; }
        public string status { get; set; }
        public string emailAddress { get; set; }
        public bool HasQuery { get; set; }
        public string officeid { get; set; }
        public int QueryCount { get; set; }
    }

    public class mQuery
    {
        public string applicationRef { get; set; }
        public string comment { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string dateCreated { get; set; }
        public string tableName { get; set; }
        public string emailAddress { get; set; }
        public string officeid { get; set; }
        public bool HasQuery { get; set; }
    }
}
