using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mBankAccount
    {
        public int Id { get; set; }
        public string BillerCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public eBank Bank { get; set; }
    }
    public enum eCurrencyCode
    {
        ZWL,
        USD,
        RAND,
        PULA
    }
    public enum eBank
    {
        AGRICULTURAL_DEVELOPMENT_BANK_OF_ZIMBABWE,
        BANCABC_ZIMBABWE,
        FIRST_CAPITAL_BANK_LIMITED,
        CBZ_BANK_LIMITED,
        ECOBANK_ZIMBABWE_LIMITED,
        FBC_BANK_LIMITED,
        NEDBANK_ZIMBABWE_LIMITED,
        METBANK,
        NMB_BANK_LIMITED,
        STANBIC_BANK_ZIMBABWE_LIMITED,
        STANDARD_CHARTERED_BANK_ZIMBABWE_LIMITED,
        STEWARD_BANK,
        ZB_BANK_LIMITED
    }
}
