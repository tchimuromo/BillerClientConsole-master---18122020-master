using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class LiabilityClauseExaminerDto
    {
        public string Application_Ref { get; set; }
        public string MemoId { get; set; }
        public string LiabilityClause { get; set; }
        public string LiabilityHasQuery { get; set; }
        public string LiabilityQueryComment { get; set; }
    }


    public class ShareClauseExaminerDto
    {
        public string Application_Ref { get; set; }
        public string MemoId { get; set; }
        public string ShareClause { get; set; }
        public string ShareClauseHasQuery { get; set; }
        public string ShareClauseComment { get; set; }
    }

    public class MemorandumExaminerDto
    {
        public int Obj_Num { get; set; }
        public string Application_Ref { get; set; }
        public string MemoId { get; set; }
        public string TheObject { get; set; }
        public string TheObjectHasQuery { get; set; }
        public string TheObjectComment { get; set; }
    }

    public class ArticlesExaminerDto
    {
        public string Application_Ref { get; set; }
        public string ArticleId { get; set; }
        public string Article { get; set; }
        public string ArticleHasQuery { get; set; }
        public string ArticleComment { get; set; }
    }

    public class AmmendedArticlesExaminerDto
    {
        public string ArticlesId { get; set; }
        public string Application_Ref { get; set; }
        public string AmmendedArticle { get; set; }
        public string AmmendedHasQuery { get; set; }
        public string AmmendedComment { get; set; }
    }

    public class MemberExaminerDto
    {
        public string ApplicationId { get; set; }

        public string MemoId { get; set; }

        public string NationalId { get; set; }

        public string Nationality { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string PhysicalAddress { get; set; }

        public string Roles { get; set; }

        public string OrdinaryShares { get; set; }

        public string PreferenceShares { get; set; }

        public int TotalShares { get; set; }

        public string MemberHasQuery { get; set; }

        public string MemberComment { get; set; }
    }


    public class EntityExaminerDto
    {
        public string ApplicationId { get; set; }
        public string EntityName { get; set; }
        public string EntityNumber { get; set; }
        public string EntityCountryOfOrigin { get; set; }
        public string EntityOrdinaryShares { get; set; }
        public string EntityPreferenceShares { get; set; }
        public int EntityTotalShares { get; set; }
        public string EntityHasQuery { get; set; }
        public string EntityComment { get; set; }
    }

}
