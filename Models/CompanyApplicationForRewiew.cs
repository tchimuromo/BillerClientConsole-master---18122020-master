using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class CompanyApplicationForRewiew
    {
        public NameForReview name { get; set; }
        public RegisteredOffice office { get; set; }
        public List<MemberForReview> members { get; set; } = new List<MemberForReview>();
        public MemoForReview memo { get; set; }
        public List<mArticles> articles { get; set; }
    }
}
