using BillerClientConsole.Dtos;
using System.Collections.Generic;

namespace BillerClientConsole.Models
{
    public class MemoForReview
    {
        public string MemoId { get; set; }
        public string LiabilityClause { get; set; }
        public string ShareClause { get; set; }
        public List<MemoClauseDto> objects { get; set; } = new List<MemoClauseDto>();
    }
}