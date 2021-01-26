using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class PaymentDto
    {
        public string UserId { get; set; }
        public string PaymentId { get; set; }
        public string PaynowReference { get; set; }
        public string Date { get; set; }
        public string CreditId { get; set; }
        public string Description { get; set; }
        public double AmountDr { get; set; }
        public double AmountCr { get; set; }
    }
}
