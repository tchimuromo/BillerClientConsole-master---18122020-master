using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{

    //public class mCompanyInfor
    //{

    //    public string RegNumber { get; set; }


    //    public string Application_Ref { get; set; }


    //    public string Search_ID { get; set; }

    //    public string Search_Name_ID { get; set; }

    //    public string Name { get; set; }

    //    public string Email { get; set; }

    //    public int No_Of_Directors { get; set; }

    //    public string Type { get; set; }

    //    public string Date_Of_Incoperation { get; set; }

    //    public string Date_Of_Application { get; set; }

    //    public string Status { get; set; }
    //    public string Approved_By { get; set; }

    //    public string Examiner { get; set; }

    //    public string Comments { get; set; }

    //    public string Co_Secretary { get; set; }

    //    public string Registered_Address { get; set; }

    //    public string City { get; set; }

    //    public string Country { get; set; }


    //    public double Athorised_Share { get; set; }
    //    public double Issued_Share { get; set; }

    //    public string Objective { get; set; }

    //    public string Other_Objectives { get; set; }

    //    public string Articles { get; set; }

    //    public string AppliedBy { get; set; }

    //}

    //public class mCompanyInfo
    //{

    //    public string RegNumber { get; set; }

    //    public string Application_Ref { get; set; }


    //    public string Search_Ref { get; set; }

    //    public string Search_Name_ID { get; set; }

    //    public string Name { get; set; }

    //    public string Email { get; set; }

    //    public int No_Of_Directors { get; set; }

    //    public string Type { get; set; }

    //    public string Date_Of_Incoperation { get; set; }

    //    public string Date_Of_Application { get; set; }

    //    public string Status { get; set; }

    //    public string Approved_By { get; set; }

    //    public string Examiner { get; set; }

    //    public string Comments { get; set; }

    //    public string Co_Secretary { get; set; }

    //    public string Registered_Address { get; set; }

    //    public string City { get; set; }

    //    public string Country { get; set; }

    //    public double Athorised_Share { get; set; }

    //    public double Issued_Share { get; set; }

    //    public string Articles { get; set; }

    //    public string AppliedBy { get; set; }

        //=================================================================================

    //    public string IndustrySector { get; set; }
    //    public string TelephoneExtension { get; set; }
    //    public string Telephone { get; set; }
    //    public string Mobile { get; set; }


    //}


    //public class mMemorandum
    //{
    //    public string _id { get; set; }

    //    public string RegNumber { get; set; }

    //    public string Application_Ref { get; set; }


    //    public string liabilityClause { get; set; }



    //    public string sharesClause { get; set; }

        //=====================================================
    //    public bool tableB { get; set; }
    //    public bool tableC { get; set; }
    //    public string ammendedArticles { get; set; }

    //}

    //public class mmainClause
    //{
    //    public string _id { get; set; }
    //    public string RegNumber { get; set; }

    //    public string Application_Ref { get; set; }

    //    public string objective { get; set; }
    //    public string objType { get; set; }
    //}


    //public class mDirectorsPotifolio
    //{
    //    public string director_id { get; set; }
    //    public string company_director_id { get; set; }
    //    public string Company_Reg { get; set; }
    //    public string Date_Of_Appointment { get; set; }
    //    public string Date_Of_Resignation { get; set; }

    //    public string Status { get; set; }

    //    public bool CompanySec { get; set; }
    //    public string Application_Ref { get; set; }
    //}

    //public class mMembersPotifolio
    //{
    //    public string company_member_id { get; set; }
    //    public string member_id { get; set; }


    //    public string Company_Reg { get; set; }
    //    public string number_of_shares { get; set; }
    //    public string Date_Of_Apointment { get; set; }




    //    public string Application_Ref { get; set; }
    //}
    //public class mDirectorInfo
    //{
    //    public string director_id { get; set; }
    //    public string ID_No { get; set; }
    //    public string Street { get; set; }
    //    public string City { get; set; }
    //    public string Surname { get; set; }
    //    public string Names { get; set; }
    //    public string Initials { get; set; }

    //}

    //public class mMembersInfo
    //{
    //    public string member_id { get; set; }
    //    public string ID_No { get; set; }
    //    public string Street { get; set; }
    //    public string City { get; set; }
    //    public string Surname { get; set; }
    //    public string Names { get; set; }
    //    public string Initials { get; set; }

    //    public string Nationality { get; set; }

    //}



    //public class mCompany
    //{
    //    public mCompanyInfo CompanyInfo { get; set; }
    //    public mMemorandum memo { get; set; }
    //    public List<mMembersInfo> members { get; set; } = new List<mMembersInfo>();
    //    public List<mDirectorInfo> DirectorsInfo { get; set; } = new List<mDirectorInfo>();

    //}

    //public class CoInfoFieldsIn
    //{
    //    public string industrySector { get; set; }
    //    public string physicalAddress { get; set; }
    //    public string city { get; set; }
    //    public string telephone { get; set; }
    //    public string telephoneExt { get; set; }
    //    public string mobileNumber { get; set; }
    //    public string emailAddress { get; set; }
    //}

    //public class MemoFieldsIn1
    //{
    //    public string mainObject { get; set; }
    //    public string otherObjectives { get; set; }
    //}

    //public class MemoFieldsIn2
    //{
    //    public string LiabilityClause { get; set; }
    //    public string tableB { get; set; }
    //    public string tableC { get; set; }
    //    public string ammendedArticles { get; set; }
    //}

    //public class AuthorisedCapitalIn
    //{
    //    public string ordAuth { get; set; }
    //    public string ordNorm { get; set; }
    //    public string prefTotal { get; set; }
    //    public string prefShareValue { get; set; }
    //    public string AuthorisedCap { get; set; }
    //}

    //public class Member
    //{
    //    public string nationalId { get; set; }
    //    public string nationality { get; set; }
    //    public string fullName { get; set; }
    //    public string gender { get; set; }
    //    public string physicalAddress { get; set; }
    //    public string ordinaryShares { get; set; }
    //    public string preferenceShres { get; set; }
    //    public string totalShares { get; set; }

    //}

    //public class CompanyMember
    //{
    //    public string name { get; set; }
    //    public string companyNumber { get; set; }
    //    public string countryOfOrigin { get; set; }
    //    public string ordinaryShares { get; set; }
    //    public string preferenceShares { get; set; }
    //    public string totalShares { get; set; }
    //}

    //public class Director
    //{
    //    public string nationalID { get; set; }
    //    public string nationality { get; set; }
    //    public string name { get; set; }
    //    public string gender { get; set; }
    //    public string residentialAddress { get; set; }

    //}

    //public class Co
    //{
    //    public mCompanyInfo pvt { get; set; }
    //    public mMemorandum memo { get; set; }
    //    public List<mmainClause> Objectives { get; set; }
    //    public AuthorisedCapitalIn capitals { get; set; }
    //    public List<Member> HumanMembers { get; set; }
    //    public List<CompanyMember> entityMembers { get; set; }
    //    public List<Director> AllCompanyDirectors { get; set; }
    //}
}


