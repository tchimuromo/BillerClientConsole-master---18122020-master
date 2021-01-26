using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mCompanyInfo
    {

        public string RegNumber { get; set; }

        public string Application_Ref { get; set; }

        public string Payment { get; set; }

        public string Search_Ref { get; set; }

        public string Search_Name_ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int No_Of_Directors { get; set; }

        public string Type { get; set; }

        public string Date_Of_Incoperation { get; set; }

        public string Date_Of_Application { get; set; }

        public string Status { get; set; }

        public string Approved_By { get; set; }

        public string Examiner { get; set; }

        public string Comments { get; set; }

        public string Co_Secretary { get; set; }

        public string Registered_Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public double Athorised_Share { get; set; }

        public double Issued_Share { get; set; }

        public string Articles { get; set; }

        public string AppliedBy { get; set; }

        public int step { get; set; }

        public string liabilityClause { get; set; }

        public string shareClause { get; set; }

        public string Objectives { get; set; }

        public string TableB { get; set; }

        public string TableC { get; set; }

        public string Ammended { get; set; }

        public string Members { get; set; }

        public string MemberEntities { get; set; }

        public string Article { get; set; }

        public string Reference { get; set; }

        public string ExaminerTaskId { get; set; }

        public int HasQuery { get; set; }

        public string Comment { get; set; }

        public string Telephone { get; set; }

        public string  MobileNumber { get; set; }

        public string PostalAddress { get; set; }
        public string Office { get; set; }

    }

    public class postArticles
    {
        public int step { get; set; }

        public mArticles Articles { get; set; }
    }

    public class mArticles
    {

        public string _id { get; set; }

        public string Application_Ref { get; set; }
        public string articles { get; set; }

        public string articles_type { get; set; }
    }

    public class mCompanyAllInfo
    {
        public mCompanyInfo companyInfo { get; set; }
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        public string MobileNumber { get; set; }
        public string Objectives { get; set; }
        public string TableB { get; set; }
        public string TableC { get; set; }
        public string Ammended { get; set; }
        public string Members { get; set; }
        public string MemberEntities { get; set; }
        public string Directors { get; set; }

    }
    //public class PostMemo

    //{
    //    public mMemorandum memo { get; set; }

    //    public int step { get; set; }
    //}

    public class mMemorandum
    {
        public int memostep { get; set; }
        public string _id { get; set; }
        public string Application_Ref { get; set; }

        public List<liabilityClause> LiabilityClause { get; set; }


        public List<sharesClause> SharesClause { get; set; }

        public List<mmainClause> objects { get; set; } = new List<mmainClause>();
    }


    public class mmainClause
    {
        public string _id { get; set; }
        public string obj_num { get; set; }
        public string memo_id { get; set; }
        public string Application_Ref { get; set; }
        public string objective { get; set; }
        public string objType { get; set; }
        public int HasQuery { get; set; }
        public string Comment { get; set; }
    }
    public class mmainClause1
    {
        public string _id { get; set; }
        public string Obj_num1 { get; set; }
        public string memo_id { get; set; }
        public string objective2 { get; set; }
        public string objType { get; set; }
    }


    public class mDirectorsPotifolio
    {
        public string director_id { get; set; }
        public string company_director_id { get; set; }
        public string Company_Reg { get; set; }
        public string Date_Of_Appointment { get; set; }
        public string Date_Of_Resignation { get; set; }
        public string Status { get; set; }
        public bool CompanySec { get; set; }
        public string Application_Ref { get; set; }
    }

    public class CompanyResponse
    {
        mCompanyInfo companyInfo { get; set; }
        mMemorandum memo { get; set; }
        List<mMembersPotifolio> membersPotifolios { get; set; }
        mArticles articles { get; set; }
        List<mMembersInfo> members { get; set; }
    }

    public class PostMembers
    {
        public string _id { get; set; }
        public string step { get; set; }
        public string ApplicationRef { get; set; }
        public List<mMembersInfo> members { get; set; }
        public List<mMembersPotifolio> membersPotifolio { get; set; }
    }


    public class mMembersPotifolio
    {
        public string company_member_id { get; set; }
        public string member_id { get; set; }

        public int OrdinaryShares { get; set; }

        public int PreferenceShares { get; set; }
        public int number_of_shares { get; set; }
        public string Date_Of_Appointment { get; set; }
        public string Date_Of_Resignation { get; set; }
        public string Application_Ref { get; set; }
        public int IsMember { get; set; }
        public int IsDirector { get; set; }
        public int IsCoSec { get; set; }
    }

    public class mMembersInfo
    {
        public string member_id { get; set; }
        public string ID_No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public string Initials { get; set; }
        public string memberType { get; set; }
        public string Nationality { get; set; }

    }



    public class mCompany
    {
        public mCompanyInfo CompanyInfo { get; set; }
        public mMemorandum memo { get; set; }
        public List<mMembersPotifolio> MembersPotifolios { get; set; }
        public List<mDirectorsPotifolio> DirectorsPotifolios { get; set; }
        public List<mMembersInfo> members { get; set; } = new List<mMembersInfo>();
        //public List<mDirectorInfo> DirectorsInfo { get; set; } = new List<mDirectorInfo>();
    }

    public class mCompanyResponse
    {
        public mCompanyInfo companyInfo { get; set; }

        public mMemorandum memo { get; set; }

        public List<mMembersPotifolio> membersPotifolios { get; set; }

        public mArticles articles { get; set; }
        public List<mMembersInfo> members { get; set; } = new List<mMembersInfo>();


    }

    public class sharesClause
    {
        public string _id { get; set; }

        public string description { get; set; }

        public string memo_id { get; set; }

        public int HasQuery { get; set; }

        public string Comment { get; set; }

        public int Status { get; set; }
    }

    public class liabilityClause
    {
        public string _id { get; set; }

        public string description { get; set; }

        public string memo_id { get; set; }

        public int HasQuery { get; set; }

        public string Comment { get; set; }

        public int Status { get; set; }
    }


    public class PostmMemorandum
    {
       
        public string _id { get; set; }

        public string Application_Ref { get; set; }

        public liabilityClause LiabilityClause { get; set; }


        public sharesClause SharesClause { get; set; }

        public List<mmainClause> objects { get; set; } = new List<mmainClause>();

    }

    public class mMeMorandum
    {
       
        public string _id { get; set; }

        public string Application_Ref { get; set; }

        public List<liabilityClause> LiabilityClause { get; set; }


        public List<sharesClause> SharesClause { get; set; }

        public List<mmainClause> objects { get; set; } = new List<mmainClause>();

    }

    public class PostMemo

    {
        public PostmMemorandum memo { get; set; }

        public int step { get; set; }
    }

    public class PaymentsResponse
    {
        public double AccountBalance { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }

    public class CreditPurchaseDto
    {
        public string UserID { get; set; }
        public string Service { get; set; }
        public int NumberOfCredits { get; set; }
    }

    public class CreditCounts
    {
        public int NameSearch { get; set; }
        public int PvtLimitedCompany { get; set; }
    }

    public class RegisteredOffice
    {
        public string OfficeId { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public int HasQuery { get; set; }
        public string Comment { get; set; }
        public string status { get; set; }
        public int QueryCount { get; set; }
    }
}
