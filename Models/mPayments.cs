using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mPayments
    {
        public int Id { get; set; }

        public string BillerCode { get; set; }

        public string PaymentDate { get; set; }
        public decimal PaidAmount { get; set; }
        public string TrackingID { get; set; }
        public string BatchNo { get; set; }
    }
}
