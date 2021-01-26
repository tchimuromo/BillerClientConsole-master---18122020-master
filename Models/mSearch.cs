using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Models
{
    public class mSearchInfo
    {
        public string search_ID { get; set; } = Guid.NewGuid().ToString();
        public string Searcher_ID { get; set; }
        public string Search_For { get; set; }
        public string Purpose { get; set; }
        public string SearchDate { get; set; }
        public string Satus { get; set; }
        public string Reason_For_Search { get; set; }
        public string Examiner { get; set; }
        public string ApprovedDate { get; set; }
        public string Cost { get; set; }
        public string ExamanerTaskID { get; set; }
        public string Justification { get; set; }
        public string SortingOffice { get; set; }
        public string Desigination { get; set; }
        public string SearchRef { get; set; }
        public string Payment { get; set; }

    }

    public class PostResponse
    {
        public string res { get; set; }
        public string msg { get; set; }

    }

    public class GetNamesResponse
    {
        public string res { get; set; }
        public List<mSearchNames> msg { get; set; }
    } 

    public class PostSearchResponse
    {
        public string res { get; set; }
        public string id { get; set; }
        public string msg { get; set; }
    }

    public class AssinedTasksResponse
    {
        public string res { get; set; }
        public AssignedTaskResponseData data { get; set; }

    }

    public class AssignedTaskResponseData
    {
        public string contentType { get; set; }
        public string serializerSettings { get; set; }
        public string statusCode { get; set; }
        public List<mTasks> value { get; set; }
    }

    public class postSearch
    {
        public string Search_For { get; set; }
        public string Brief { get; set; }
        public string Justification { get; set; }
        public string sortingOffice { get; set; }
        public string Desigination { get; set; }
        public string Reason { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string name3 { get; set; }
        public string name4 { get; set; }
        public string name5 { get; set; }
        public string name6 { get; set; }
    }
    public class mSearchNames
    {
        [Required]
        public string Name_ID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Search_ID { get; set; }

      
    }

  


    public class mSearch
    {
        [Required]
        public mSearchInfo searchInfo { get; set; }

        [Required]
        public List<mSearchNames> SearchNames { get; set; } = new List<mSearchNames>();
    }

    public class mTasks
    {

        public string _id { get; set; }
        public string Service { get; set; }

        public string Assigner { get; set; }

        public string AssignTo { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public string ExpDateofComp { get; set; }

        public int NoOfRecords { get; set; }
    }
     public class test
    {
        public string NationalId { get; set; }
    }

    public class MemberDto
    {
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string PhyicalAddress{ get; set; }
        public bool IsAMember { get; set; }
        public bool IsADirector { get; set; }
        public bool IsCompanySecretary { get; set; }
        public string EntityNumber { get; set; }
        public string OrdinaryShares { get; set; }
        public string PreferenceShares { get; set; }
        public string TotalShares { get; set; }
    }

    public enum sex
    {
        male,
        female
        
    }

    //public class EntityDto
    //{
    //    public string EntityName { get; set; }
    //    public string EntityNumber { get; set; }
    //    public string CountryOfOrigin { get; set; }
    //    public string OrdinaryShares { get; set; }
    //    public string PreferenceShares { get; set; }
    //    public string MyProperty { get; set; }
    //}
}
