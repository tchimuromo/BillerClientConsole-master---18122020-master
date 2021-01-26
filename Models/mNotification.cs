using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    /// <summary>
    /// models the notification class for the client console
    /// </summary>
    public class mNotification
    {
        public int id { get; set; }
        public string BillerCode { get; set; }
        public string message { get; set; }
        public DateTime date { get; } = DateTime.Now;
        public bool isRead { get; set; }
    }
}
