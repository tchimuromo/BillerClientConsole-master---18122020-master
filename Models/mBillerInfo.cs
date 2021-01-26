using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mBillerInfo
    {

        public int Id { get; set; }
        
        public string BillerCode { get; set; }
        
        public string BillerName { get; set; }
        
        public string ContactPerson { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string ContactEmail { get; set; }
        
        public eBillerType BillerType { get; set; }
        
        public eBillerCategory BillerCategory { get; set; }
        
        public bool hasProducts { get; set; }
        
        public bool hasAccounts { get; set; }
        
        public bool hasCart { get; set; }
        
        public ClientIndentifier ClientIndentifier { get; set; }
        
        public bool hasAdditionalData { get; set; }
        public string RequestEndPoint { get; set; }

        public string CredentialsUsername { get; set; }
        public string CredentialsPassword { get; set; }
        public string ConnectionType { get; set; }

        public int PaymentCycle { get; set; }

        
        public bool BillerStatus { get; set; }
        public string Enabler { get; set; }
    }


    public enum ClientIndentifier
    {
        token,
        billerCustomerAccount
    }
    public enum eBillerCategory
    {

        UNIVERSITIES,
        SCHOOLS,
        HOSPITALS,
        HOTELS,
        SUPERMARKETS,
        PHARMACEUTICALS,
        AUTOMOTIVE,
        TRANSPORT,
        FUEL,
        INSURANCE,
        CHURCHES,
        FASTFOODS
    }
    public enum eBillerType
    {
        ONLINE,
        OFFLINE
    }


}
