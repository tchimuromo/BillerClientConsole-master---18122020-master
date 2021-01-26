using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mReceipts
    {
     
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public string PayerName { get; set; }
        public string PayerPhoneNumber { get; set; }
        public string PayerAccount { get; set; }
        public decimal AmountPaid { get; set; }
        public int Posted { get; set; }
        public string BillerCode { get; set; }
        public string BillerProductID { get; set; }
        public decimal BillerProductQuantity { get; set; }
        public string TransactionDate { get; set; }
        public string TrackingId { get; set; }
        public bool PaidToBiller { get; set; }
        public string Notes { get; set; }
        public string OrderID { get; set; }
        public bool ReversedByBiller { get; set; }
    }

     public class PostStatusUpdate
    {
        public string transnumber { get; set; }
        public int status { get; set; }
    }
    public class PostPaymentUpdate
    {
        public string transnumber { get; set; }
        public int status { get; set; }
    }
}
