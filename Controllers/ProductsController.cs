using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;
using Webdev.Payments;
using Syncfusion.EJ2.Base;
using Webdev;
using System.Collections;
using IronBarCode;
using System.Drawing;
using BillerClientConsole.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    [Route("Products")]
    [Route("Home")]
    public class ProductsController : Controller
    {
        private dbContext db = new dbContext();
        public static List<mmainClause> objects = new List<mmainClause>();
        private static List<ShareClauseExaminerDto> shareClause = new List<ShareClauseExaminerDto>();
        private static List<LiabilityClauseExaminerDto> liabilityClause = new List<LiabilityClauseExaminerDto>();
        private static List<MemorandumExaminerDto> memoObjects = new List<MemorandumExaminerDto>();
        private static List<ArticlesExaminerDto> selectedModel = new List<ArticlesExaminerDto>();
        private static List<AmmendedArticlesExaminerDto> ammendedArticles = new List<AmmendedArticlesExaminerDto>();
        private static List<MemberExaminerDto> members = new List<MemberExaminerDto>();
        private static List<EntityExaminerDto> memberEntities = new List<EntityExaminerDto>();
        public static List<mCompanyResponse> companyResponses = new List<mCompanyResponse>();
        private readonly QueryDbContext context;

        public ProductsController(QueryDbContext context)
        {
            this.context = context;
        }

        [HttpGet("AddNewProduct")]
        public IActionResult AddNewProduct1()
        {
            ViewBag.title = "New Search";
            List<mSearchNames> sn = new List<mSearchNames>();
          
            var data = sn;
            ViewBag.dataSource = data;

            return View();
        }

        [HttpGet("GetApplicationFromExaminer")]
        public IActionResult ApplicationFromExaminer(string ApplicationID)
        {
            return View();
        }


        [HttpGet("UnassignedTasks")]
        public IActionResult UnassignedTasks(string ToDisplay)
        {
            ViewBag.title = "About";
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(ToDisplay))
            {
                if (ToDisplay.Equals("CompanyApplications"))
                {
                    var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                    var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
                    dynamic dataa_j = JsonConvert.DeserializeObject(resp);
                    var companies = dataa_j.data.value;
                    List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
                    List<mCompanyResponse> unassignedApplications = companyApplications.Where(s => string.IsNullOrEmpty(s.companyInfo.Examiner) && !string.IsNullOrEmpty(s.companyInfo.Payment) && !string.IsNullOrEmpty(s.companyInfo.Search_Ref)).ToList(); //!string.IsNullOrEmpty(s.companyInfo.Payment) && 
                    ViewBag.Applications = unassignedApplications;
                    ViewBag.ToDisplay = ToDisplay;
                    //ViewBag.ToDisplay = ToDisplay;
                    //ViewBag.CompanyApplications = companyApplications.Count;
                    //return RedirectToAction("AssignTasks", new { onDisplay = "CompanyApplication" });
                }
            }
            else
            {
                var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
                dynamic dataa_j = JsonConvert.DeserializeObject(resp);
                var companies = dataa_j.data.value;
                List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
                List<mCompanyResponse> unassignedApplications = companyApplications.Where(s => string.IsNullOrEmpty(s.companyInfo.Examiner) && !string.IsNullOrEmpty(s.companyInfo.Payment) && !string.IsNullOrEmpty(s.companyInfo.Search_Ref)).ToList(); //
                ViewBag.CompanyApplications = unassignedApplications.Count;
                

                var res = client.GetAsync($"{Globals.Globals.end_point_get_name_searches}").Result.Content.ReadAsStringAsync().Result;
                dynamic data_j = JsonConvert.DeserializeObject(res);
                var searchNames = data_j.data.value;
                List<mSearch> names = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
                names = names.Where(z => z.searchInfo.Satus == "Pending" && z.searchInfo.Payment == "Paid").ToList();
                List<mSearch> harare = names.Where(z => z.searchInfo.SortingOffice == "HARARE").ToList();
                List<mSearch> bulawyo = names.Where(z => z.searchInfo.SortingOffice == "BULAWAYO").ToList();
                ViewBag.Count = names.Count;
                
                ViewBag.HarareCount = harare.Count;
                ViewBag.Bulawayo = bulawyo.Count;
                int disp = 0;
                if(names.Count > 0|| harare.Count > 0 || bulawyo.Count > 0 || unassignedApplications.Count > 0)
                {
                    disp = 1;
                }

                ViewBag.Display = disp;
            }

            
            return View();
        }

        [HttpGet("GetCompantTask")]
        public IActionResult CompanyTask(string TaskId)
        {
            ViewBag.title = "About";
            var client = new HttpClient();
            
            var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
            dynamic dataa_j = JsonConvert.DeserializeObject(resp);
            var companies = dataa_j.data.value;
            
            List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            List<mCompanyResponse> assignedApplications = 
                companyApplications.Where(s => !string.IsNullOrEmpty(s.companyInfo.Payment) && !string.IsNullOrEmpty(s.companyInfo.Search_Ref) && !string.IsNullOrEmpty(s.companyInfo.ExaminerTaskId) && s.companyInfo.ExaminerTaskId.Equals(TaskId)).ToList(); //
            ViewBag.CompanyApplications = assignedApplications;

            return View();
        }

        [HttpGet("AssignTasks")]
        public IActionResult AssignTasks( string onDisplay)
        {
            var client = new HttpClient();
            ViewBag.title = "About";
            if (string.IsNullOrEmpty(onDisplay))
            {
                var res = client.GetAsync($"{Globals.Globals.end_point_get_name_searches}").Result.Content.ReadAsStringAsync().Result;
                dynamic data_j = JsonConvert.DeserializeObject(res);
                var searchNames = data_j.data.value;
                List<mSearch> names = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
                names = names.Where(z => z.searchInfo.Satus == "Pending").ToList();
                ViewBag.Count = names.Count;
                
            }else if (onDisplay.Equals("CompanyApplication"))
            {
                var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
                dynamic dataa_j = JsonConvert.DeserializeObject(resp);
                var companies = dataa_j.data.value;
                List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
                List<mCompanyResponse> unassignedApplications = companyApplications.Where(s => string.IsNullOrEmpty(s.companyInfo.Examiner) && !string.IsNullOrEmpty(s.companyInfo.Payment) && !string.IsNullOrEmpty(s.companyInfo.Search_Ref)).ToList();
                ViewBag.ToDisplay = onDisplay;
                ViewBag.CompanyApplications = unassignedApplications.Count;
            }
            var examiner = db.AspNetUsersInternal.Where(e => e.role == 2).ToList();
            var num = examiner.Count;
            ViewBag.people = examiner;

            return View();

        }

        [HttpPost("AssignCompanyForExamination")]
        public async Task<JsonResult> AssignCompanyForExamination(string Examiner,string SearchRef)
        {
            var client = new HttpClient();
            var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={SearchRef}").Result.Content.ReadAsStringAsync();
            dynamic json_dataa = JsonConvert.DeserializeObject(rhisponzi);
            dynamic dataa = json_dataa;
            try
            {
                dataa = json_dataa.data.value;
            }
            catch (Exception ex)
            {
                
            }

            mCompanyResponse companyApplication = JsonConvert.DeserializeObject<mCompanyResponse>(dataa.ToString());
            companyApplication.companyInfo.Examiner = Examiner;

            var response = await client.PostAsJsonAsync<mCompanyResponse>($"{Globals.Globals.end_point_assign_company_for_examination}", companyApplication).Result.Content.ReadAsStringAsync();
            dynamic json_da = JsonConvert.DeserializeObject(response);
            dynamic data = json_da;
            PostResponse pr = JsonConvert.DeserializeObject<PostResponse>(data.ToString());

            if(pr.res == "ok")
            {
                return Json(new
                {
                    res = pr.res,
                    message = "Application assigned"
                });
            }

            return Json(new
            {
                res = "failed",
                message = "Application assigning failed"
            });

        }

        [HttpGet("GetNameSearchesByUserByTaskID")]
        public async Task<IActionResult> DrillIntoDetails(string taskId)
        {
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
           
            ViewBag.title = "Name search detail";
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_get_name_search_by_user_by_task_id}?UserID={user.Email}&TaskID={taskId}").Result.Content.ReadAsStringAsync();

            dynamic data_j = JsonConvert.DeserializeObject(res);
            dynamic json_data = JsonConvert.DeserializeObject(res);
            var searchNamess = data_j.data.value;
            List<mSearch> names = JsonConvert.DeserializeObject<List<mSearch>>(searchNamess.ToString());
            var namess = names.Where(z => z.searchInfo.Satus == "Assigned"||z.searchInfo.Satus == "Approved"||z.searchInfo.Satus == "Rejected").Take(1).FirstOrDefault();
            
            var searchInfo = namess.searchInfo;
            var searchNames = namess.SearchNames;
            ViewBag.TaskID = taskId;
            ViewBag.dateSubmitted = searchInfo.SearchDate;
            ViewBag.purpose = searchInfo.Purpose;
            ViewBag.natureOfBusiness = searchInfo.Search_For;
            ViewBag.Justification = searchInfo.Justification;
            ViewBag.searchDate = searchInfo.SearchDate;
            ViewBag.searchId = searchInfo.search_ID;
            ViewBag.designation = searchInfo.Desigination;
            ViewBag.applicant = searchInfo.Searcher_ID;
            ViewBag.reference = searchInfo.search_ID;

            //List<mSearchNames> namesp = JsonConvert.DeserializeObject<List<mSearchNames>>(searchNames.ToString());
            

            ViewBag.names = searchNames;
            
            return View();
        }

        

        [HttpGet("Services")]
        public IActionResult Services()
        {
            ViewBag.title = "New Search";
            List<mSearchNames> sn = new List<mSearchNames>();

            var data = sn;
            ViewBag.dataSource = data;

            return View();

        }

        [HttpPost("PostTask")]
        public async Task<IActionResult> AssignTasks(mTasks task)
        {
            
            var db = new dbContext();
            //get examiners for drop down
            var examiner = db.AspNetUsersInternal.Where(e => e.role == 2).ToList();
            var num = examiner.Count;
            ViewBag.people = examiner;

            task._id = Guid.NewGuid().ToString();
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync<mTasks>($"{Globals.Globals.end_point_post_task}", task).Result.Content.ReadAsStringAsync();
            PostSearchResponse ps = JsonConvert.DeserializeObject<PostSearchResponse>(response);
            db.Dispose();
            ViewBag.id = ps.id;
            ViewBag.title = "Assign tasks";
            ViewBag.service = task.Service;
            ViewBag.assigner = task.Assigner;
            ViewBag.assignTo = task.AssignTo;
            ViewBag.date = task.Date;
            ViewBag.status = task.Status;
            ViewBag.expDateofComp = task.ExpDateofComp;
            ViewBag.noOfRecords = task.NoOfRecords;
            return View();
        }


        [HttpPost("SubmitSearch/{tempsearchID}")]
        public async Task<IActionResult> SubmitSearch(string tempsearchID)
        {
            var db = new dbContext();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_submit_name_search}?tempsearchID={tempsearchID}").Result.Content.ReadAsStringAsync();
            PostSearchResponse psr = JsonConvert.DeserializeObject<PostSearchResponse>(res);
            db.Dispose();

            //if(psr.res == "ok")
            //{
            //    Globals.Globals.searchApplicationID = "";
            //    Globals.Globals.tempSearchId1 = "";
            //    return RedirectToAction("Paynow", new { gateway = tempsearchID});
            //}
            return View();
        }

        [HttpPost("CheckAvailbility")]
        public async Task<JsonResult> CheckAvailbility(string name)
        {
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_check_name_availability}?name={name}").Result.Content.ReadAsStringAsync();
            PostResponse ps = JsonConvert.DeserializeObject<PostResponse>(res);
            db.Dispose();

            return Json(new
            {
                res = ps.res,
                message = ps.msg
            });
        }

        [HttpGet("NameSearchesByUserID")]
        public async Task<JsonResult> CheckNameStartAndContains(string name)
        {
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync("http://192.168.40.51/Company/api/v1/GetNameSearchesByUser?UserID=admin%40rubiem.com").Result.Content.ReadAsStringAsync();
            GetNamesResponse ps = JsonConvert.DeserializeObject<GetNamesResponse>(res);
            db.Dispose();

            //ps.msg = ps.msg.Where(z => z.Name != name);
            return Json(new
            {
                res = ps.res,
                message = (ps.msg).ToList()
            });
        }

        [HttpGet("CheckNameStartAndContainsWith")]
        public async Task<JsonResult> CheckNameStartAndContain(string name)
        {
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_check_name_start_contains_in_name_search}?name={name}").Result.Content.ReadAsStringAsync();
            GetNamesResponse ps = JsonConvert.DeserializeObject<GetNamesResponse>(res);
            db.Dispose();

            IEnumerable<mSearchNames> names = ps.msg.Where(z => z.Name != name);
            return Json(new
            {
                res = ps.res,
                data = names
            });
        }

        [HttpGet("CheckNameStartsWith")]
        public async Task<JsonResult> CheckNameStartsWith(string name, string searchId)
        {
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_check_name_start_with_name_search}?name={name}").Result.Content.ReadAsStringAsync();
            GetNamesResponse ps = JsonConvert.DeserializeObject<GetNamesResponse>(res);
            db.Dispose();

            //ps.msg = ps.msg.Where(z => z.Name != name && z.Search_ID != searchId).ToList(); ;


            return Json(new 
            {
                res = ps.res,
                data = ps.msg
            });
        }

        [HttpPost("ApproveSearchedName")]
        public async Task<JsonResult> ApproveSearchedName(mSearchNames name, string TaskID,string status )
        {
            PostResponse ps;
            var db = new dbContext();
            var client = new HttpClient();
            var clientw = new HttpClient();
            var clientp = new HttpClient();
            name.Status = status;

            if (name.Status == "Reserved")
            {
                var rese = clientp.GetAsync($"{Globals.Globals.end_point_get_update_task}?TasksID={TaskID}").Result.Content.ReadAsStringAsync().Result;

                var res = clientw.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_search_id}?ID={name.Search_ID}").Result.Content.ReadAsStringAsync().Result;
                dynamic data_j = JsonConvert.DeserializeObject(res);
                var searchNames = data_j.data.value.searchNames;
                List<mSearchNames> names = JsonConvert.DeserializeObject<List<mSearchNames>>(searchNames.ToString());
                List<mSearchNames> notConsidered = names.Where(z => z.Status.Equals("Pending") && z.Name_ID != name.Name_ID).ToList();
                //names = names.Where(z => z.Name_ID != name.Name_ID || z.Status != "Blacklisted" || name.Status != "Rejected").ToList();
                
                foreach (mSearchNames zita in notConsidered)
                {
                    zita.Status = "Not Considered";
                    var clientr = new HttpClient();
                    var resr = await clientr.PostAsJsonAsync<mSearchNames>($"{Globals.Globals.end_point_approved_search_name}", zita).Result.Content.ReadAsStringAsync();
                }
                var resp = await client.PostAsJsonAsync<mSearchNames>($"{Globals.Globals.end_point_approved_search_name}", name).Result.Content.ReadAsStringAsync();
                ps = JsonConvert.DeserializeObject<PostResponse>(resp);

                if(ps.res == "ok")
                {
                    var response = clientw.GetAsync($"{Globals.Globals.end_point_get_name_search_by_id}?ID={name.Search_ID}").Result.Content.ReadAsStringAsync().Result;
                    dynamic json_data = JsonConvert.DeserializeObject(response);
                    var data = json_data.data.value.searchInfo;
                    mSearchInfo search = JsonConvert.DeserializeObject<mSearchInfo>(data.ToString());
                    if(search!= null)
                    {
                        search.ApprovedDate = DateTime.Now.ToString();
                        search.Satus = "Approved";
                        var resr = await clientw.PostAsJsonAsync<mSearchInfo>($"{Globals.Globals.end_point_approve_search}", search).Result.Content.ReadAsStringAsync();
                    }
                }
            }

            else
            {
                var res = await client.PostAsJsonAsync<mSearchNames>($"{Globals.Globals.end_point_approved_search_name}", name).Result.Content.ReadAsStringAsync();
                ps = JsonConvert.DeserializeObject<PostResponse>(res);
                if(ps.res == "ok")
                {
                    var ress = clientw.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_search_id}?ID={name.Search_ID}").Result.Content.ReadAsStringAsync().Result;
                    dynamic data_j = JsonConvert.DeserializeObject(ress);
                    var searchNames = data_j.data.value.searchNames;
                    List<mSearchNames> names = JsonConvert.DeserializeObject<List<mSearchNames>>(searchNames.ToString());
                    names = names.Where(z =>  z.Status == "Pending" ).ToList();
                    if (names.Count < 1)
                    {
                        var rese = clientp.GetAsync($"{Globals.Globals.end_point_get_update_task}?TaskID={TaskID}").Result.Content.ReadAsStringAsync().Result;
                        var response = clientw.GetAsync($"{Globals.Globals.end_point_get_name_search_by_id}?ID={name.Search_ID}").Result.Content.ReadAsStringAsync().Result;
                        dynamic json_data = JsonConvert.DeserializeObject(response);
                        var data = json_data.data.value[0].searchInfo;
                        mSearchInfo search = JsonConvert.DeserializeObject<mSearchInfo>(data.ToString());
                        if (search != null)
                        {
                            search.ApprovedDate = DateTime.Now.ToString();
                            search.Satus = "Rejected";

                            var resr = await clientw.PostAsJsonAsync<mSearchInfo>($"{Globals.Globals.end_point_approve_search}", search).Result.Content.ReadAsStringAsync();
                        }
                    }
                }
                else
                {
                    ps.res = "err";
                    ps.msg = "Inalaid name ID";
                }
            }
            db.Dispose();

            return Json(new
            {
                res = ps.res,
                message = ps.msg
            });
        }

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// 

        [HttpGet("Detail")]
        public async Task<IActionResult> CompanyExamination(string reference)
        {
            ViewBag.Title = "Customer";

            var client = new HttpClient();

            var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={reference}").Result.Content.ReadAsStringAsync();
           
            dynamic json_dataa = JsonConvert.DeserializeObject(rhisponzi);
            dynamic dataa = json_dataa;
            try
            {
                dataa = json_dataa.data.value;
            }catch(Exception ex)
            {
                return BadRequest();
            }
            
            mCompanyResponse companyApplication = JsonConvert.DeserializeObject<mCompanyResponse>(dataa.ToString());
            ViewBag.companyApplication = companyApplication;
            List<Subscriber> subscribers = new List<Subscriber>();
            ViewBag.Application = companyApplication.companyInfo.Application_Ref;
            ViewBag.ApplicationID=companyApplication.companyInfo.Search_Ref;//Search Ref or ApplicationID
            var nameOfficeResponse = await client.GetAsync($"{Globals.Globals.service_end_point}/{companyApplication.companyInfo.Search_Ref}/Namesearch/{companyApplication.companyInfo.Office}/Office").Result.Content.ReadAsStringAsync();
            dynamic nameOfficeJson = JsonConvert.DeserializeObject(nameOfficeResponse);
            NameOfficeResponse nameOffice = JsonConvert.DeserializeObject<NameOfficeResponse>(nameOfficeJson.ToString());
            ViewBag.NameOffice = nameOffice;

            foreach(var clause in companyApplication.memo.LiabilityClause)
            {
                ViewBag.Liability_liab = clause.description;
            }

            foreach(var clause in companyApplication.memo.SharesClause)
            {
                ViewBag.Share_share = clause.description;
            }

            ViewBag.article_type = companyApplication.articles.articles_type;

           // var k = 0;
            if (companyResponses == null)
            {

                companyResponses = new List<mCompanyResponse>();

                companyResponses.Add(companyApplication);
                var liability = companyApplication.memo.LiabilityClause;
                var ShareClause = companyApplication.memo.SharesClause;
                
                foreach(liabilityClause lc in liability)
                {
                    if (lc.Status == 0)
                    {
                        if (!string.IsNullOrEmpty(lc.description))
                        {
                            LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                            liablity.LiabilityClause = lc.description;
                            liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                            liabilityClause.Add(liablity);
                        }

                    }
                }
               
                foreach(sharesClause sc in ShareClause)
                {
                    if (sc.Status == 0)
                    {
                        if (!string.IsNullOrEmpty(sc.description))
                        {
                            ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                            share.ShareClause = sc.description;
                            share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                            shareClause.Add(share);
                        }
                    }

                }
                   
              

                foreach (mmainClause obj in companyApplication.memo.objects)
                {
                    MemorandumExaminerDto memo = new MemorandumExaminerDto();
                    memo.TheObject = obj.objective;
                    memo.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    memoObjects.Add(memo);
                }

                if (!string.IsNullOrEmpty(companyApplication.articles.articles_type))
                {
                    ArticlesExaminerDto articles = new ArticlesExaminerDto();
                    if (companyApplication.articles.articles_type == "table b")
                        articles.Article = "Table B";
                    else
                        articles.Article = "Table C";
                    articles.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    articles.ArticleId = companyApplication.articles._id;
                    selectedModel.Add(articles);
                }


                if (!string.IsNullOrEmpty(companyApplication.articles.articles))
                {
                    AmmendedArticlesExaminerDto ammended = new AmmendedArticlesExaminerDto();
                    ammended.AmmendedArticle = companyApplication.articles.articles;
                    ammendedArticles.Add(ammended);
                }


                foreach (mMembersInfo membr in companyApplication.members)
                {
                    mMembersPotifolio follio = null;
                    foreach (mMembersPotifolio potfo in companyApplication.membersPotifolios)
                    {
                        if (membr.member_id.Equals(potfo.member_id))
                        {
                            follio = potfo;
                            continue;
                        }
                    }
                    
                    MemberExaminerDto memberrs = new MemberExaminerDto();
                    EntityExaminerDto entities = new EntityExaminerDto();

                    if (membr.memberType.Equals("Person"))
                    {
                        memberrs.FirstName = membr.Names;
                        memberrs.Surname = membr.Surname;
                        memberrs.Nationality = membr.Nationality;
                        memberrs.NationalId = membr.ID_No;
                        memberrs.PhysicalAddress = membr.Street + membr.City;
                        memberrs.ApplicationId = companyApplication.companyInfo.Application_Ref;

                        memberrs.TotalShares = follio.number_of_shares;
                        string roles = "";
                        if (follio.IsCoSec == 1)
                            roles = roles + "Secretary,";
                        if (follio.IsDirector == 1)
                            roles = roles + "Director,";
                        if (follio.IsMember == 1)
                            roles = roles + "Member,";

                        memberrs.Roles = roles;

                        members.Add(memberrs);

                    }
                    else
                    {
                        entities.EntityName = membr.Names;
                        entities.EntityCountryOfOrigin = membr.Nationality;
                        entities.EntityTotalShares = follio.number_of_shares;

                        memberEntities.Add(entities);
                    }
                }
            }
            else
            {
                var cor = companyResponses.Where(s => s.companyInfo.Search_Ref == reference).FirstOrDefault();
                if (cor == null)
                {
                    companyResponses.Add(companyApplication);
                    foreach(liabilityClause lc in companyApplication.memo.LiabilityClause)
                    {
                        if (lc.Status == 0)
                        {
                            if (!string.IsNullOrEmpty(lc.description))
                            {
                                LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                                liablity.LiabilityClause = lc.description;
                                liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                                liabilityClause.Add(liablity);
                                ViewBag.LiabilityClause = liablity;
                            }

                        }
                    }

                   foreach(sharesClause sc in companyApplication.memo.SharesClause)
                    {
                        if (sc.Status == 0)
                        {
                            if (!string.IsNullOrEmpty(sc.description))
                            {
                                ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                                share.ShareClause = sc.description;
                                share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                                shareClause.Add(share);
                                ViewBag.ShareClause = share;
                            }

                        }
                    }
                    

                    foreach (mmainClause obj in companyApplication.memo.objects)
                    {
                        MemorandumExaminerDto memo = new MemorandumExaminerDto();
                        memo.TheObject = obj.objective;
                        memo.Application_Ref = companyApplication.companyInfo.Application_Ref;
                        memoObjects.Add(memo);
                    }

                    if (!string.IsNullOrEmpty(companyApplication.articles.articles_type))
                    {
                        ArticlesExaminerDto articles = new ArticlesExaminerDto();
                        if (companyApplication.articles.articles_type == "table b")
                        {
                            articles.Article = "Table B";
                            articles.Application_Ref = companyApplication.companyInfo.Application_Ref;
                            articles.ArticleId = companyApplication.articles._id;
                        }
                        else
                        {
                            articles.Article = "Table C";
                            articles.Application_Ref = companyApplication.companyInfo.Application_Ref;
                            articles.ArticleId = companyApplication.articles._id;
                        }
                            
                        selectedModel.Add(articles);
                        ViewBag.Articles = articles;
                    }

                    foreach (mMembersInfo membr in companyApplication.members)
                    {
                        mMembersPotifolio follio = null;
                        foreach (mMembersPotifolio potfo in companyApplication.membersPotifolios)
                        {
                            if (membr.member_id.Equals(potfo.member_id))
                            {
                                follio = potfo;
                                continue;
                            }
                        }

                        MemberExaminerDto memberrs = new MemberExaminerDto();
                        EntityExaminerDto entities = new EntityExaminerDto();

                        if (membr.memberType.Equals("Person"))
                        {
                            memberrs.FirstName = membr.Names;
                            memberrs.Surname = membr.Surname;
                            memberrs.Nationality = membr.Nationality;
                            memberrs.NationalId = membr.ID_No;
                            memberrs.PhysicalAddress = membr.City;
                            memberrs.ApplicationId = companyApplication.companyInfo.Application_Ref;
                            memberrs.OrdinaryShares = follio.OrdinaryShares.ToString();
                            memberrs.PreferenceShares = follio.PreferenceShares.ToString();

                            memberrs.TotalShares = follio.number_of_shares;
                            string roles = "";
                            if (follio.IsCoSec == 1)
                                roles = roles + "Secretary,";
                            if (follio.IsDirector == 1)
                                roles = roles + "Director,";
                            if (follio.IsMember == 1)
                                roles = roles + "Member,";

                            memberrs.Roles = roles;

                            members.Add(memberrs);

                        }
                        else
                        {
                            entities.EntityName = membr.Names;
                            entities.EntityCountryOfOrigin = membr.Nationality;
                            entities.EntityNumber = membr.member_id;
                            entities.EntityPreferenceShares = follio.PreferenceShares.ToString();
                            entities.EntityOrdinaryShares = follio.OrdinaryShares.ToString();
                            entities.EntityTotalShares = follio.number_of_shares;

                            memberEntities.Add(entities);
                        }

                        Subscriber subscriber = new Subscriber();
                        subscriber.Id = membr.ID_No;
                        subscriber.FullName = membr.Names + " " + membr.Surname;
                        subscriber.Address = membr.Street + " " + membr.City;
                        if (follio.IsCoSec == 1)
                        {
                            subscriber.Occupation = "Company secretary";
                        }
                        if (follio.IsDirector == 1)
                        {
                            subscriber.Occupation = "Director";
                        }
                        if (follio.IsMember == 1)
                        {
                            subscriber.Occupation = "Member";
                        }
                        subscriber.Date = companyApplication.companyInfo.Date_Of_Application;
                        subscriber.TotalShares = follio.number_of_shares;
                        subscribers.Add(subscriber);
                    }

                    ViewBag.Subcribers = subscribers;
                    if (!string.IsNullOrEmpty(companyApplication.articles.articles))
                    {
                        AmmendedArticlesExaminerDto ammended = new AmmendedArticlesExaminerDto();
                        ammended.AmmendedArticle = companyApplication.articles.articles;
                        ammended.ArticlesId = companyApplication.articles._id;
                        ammended.Application_Ref = companyApplication.companyInfo.Application_Ref;
                        ammendedArticles.Add(ammended);
                    }
                }
            }
            





            List<object> DataRange = new List<object>();
            //List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "1,000 Rows 11 Columns", Value = "1000" });
            DataRange.Add(new { Text = "10,000 Rows 11 Columns", Value = "10000" });
            DataRange.Add(new { Text = "1,00,000 Rows 11 Columns", Value = "100000" });
            ViewBag.Data = DataRange;

            //Code to populate My Card
            var ApplicationQueries = context.Queries.Where(q => q.applicationRef == companyApplication.companyInfo.Application_Ref && q.status == "Pending").ToList();
            ViewBag.ApplicationQueries = ApplicationQueries;
          
            //var order = OrdersDetails.GetAllRecords();
            //ViewBag.datasource = order;
            return View();
        }

        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddProduct(postSearch product)
        {

            mSearch ms = new mSearch();
            mSearchInfo ms1 = new mSearchInfo();
            ms1.Payment = "Not paid";
            ms1.Search_For = product.Search_For.ToString().ToUpper();
            ms1.Justification = product.Justification.ToUpper();
            ms1.Purpose = product.Brief.ToUpper();
            ms1.Searcher_ID = User.Identity.Name;
            ms1.SortingOffice = product.sortingOffice.ToUpper();
            ms1.Desigination = product.Desigination.ToUpper();
            ms1.Reason_For_Search = product.Reason.ToUpper();

           
                Globals.Globals.tempSearchId1 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId1 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId2 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId3 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId4 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId5 = Guid.NewGuid().ToString();
                Globals.Globals.tempSearchNameId6 = Guid.NewGuid().ToString();
           

            ms1.search_ID = Globals.Globals.tempSearchId1;
           
           
            List<mSearchNames> snames = new List<mSearchNames>();
            if (!string.IsNullOrEmpty(product.name1))
                snames.Add(new mSearchNames
                {
                    Name = product.name1.ToUpper(),
                    Name_ID = Globals.Globals.tempSearchNameId1,
                    Status = "Pending",
                    Search_ID = ms1.search_ID
                });
            if (!string.IsNullOrEmpty(product.name2))
                snames.Add(new mSearchNames
                {
                    Name = product.name2.ToUpper(),
                    Name_ID = Globals.Globals.tempSearchNameId2,
                    Status = "Pending",
                    Search_ID = ms1.search_ID
                });
            if (!string.IsNullOrEmpty(product.name3))
                snames.Add(new mSearchNames
                {
                    Name = product.name3.ToUpper(),
                    Name_ID = Globals.Globals.tempSearchNameId3,
                    Status = "Pending",
                    Search_ID = ms1.search_ID
                });
            if (!string.IsNullOrEmpty(product.name4))
                snames.Add(new mSearchNames
                {
                    Name = product.name4.ToUpper(),
                    Name_ID = Globals.Globals.tempSearchNameId4,
                    Status = "Pending",
                    Search_ID = ms1.search_ID
                });
            if (!string.IsNullOrEmpty(product.name5))
                snames.Add(new mSearchNames
                {
                    Name = product.name5.ToUpper(),
                    Name_ID = Globals.Globals.tempSearchNameId5,
                    Status = "Pending",
                    Search_ID = ms1.search_ID
                });
            //if (!string.IsNullOrEmpty(product.name6))
            //snames.Add(new mSearchNames { Name = product.name6.ToUpper(), Name_ID =Globals.Globals.tempSearchNameId6,Search_ID = ms1.search_ID });

            ms.searchInfo = ms1;
            ms.SearchNames = snames;

            ViewBag.title = "New Search";
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync<mSearch>($"{Globals.Globals.end_point_add_search}", ms).Result.Content.ReadAsStringAsync();
            PostSearchResponse ps =  JsonConvert.DeserializeObject<PostSearchResponse>(response);
            if (ps.res == "ok")
            {

            }
            Globals.Globals.searchApplicationID = Globals.Globals.tempSearchId1;
            return RedirectToAction("ListBillerProducts");
        }


        /// <summary>
        /// fetch the product and display it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ViewDetails/{id}")]
        public IActionResult ViewDetails(string id)
        {
            ViewBag.title = "View Product Details";
            var client = new HttpClient();
            var response = client.GetAsync($"{Globals.Globals.end_point_fetchBillerProductById}?id={id}").Result.Content.ReadAsStringAsync().Result;
            ViewBag.product = JsonConvert.DeserializeObject(response);
            return View();
        }

        [HttpGet("ListBillerProducts")]
        [HttpGet("")]
        public IActionResult ListBillerProducts(int pageNumber, string date_from, string date_to)
        {
            ViewBag.title = "Products";
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/products/BillerProducts";
            TempData["id"] = Globals.Globals.searchApplicationID;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            return View();
        }

        /// <summary>
        /// update the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(mProductOffline product)
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_updateBillerProduct}", product);
            return RedirectToAction("ListBillerProducts");
        }


        [HttpGet("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = new HttpClient();
            var resp = await client.GetAsync($"{Globals.Globals.end_point_DeleteProductBillerProductById}?id={id}");
            return RedirectToAction("ListBillerProducts");
        }

        //code to add PDF to HTML Page Razor syntax
        [Route("Paynow")]
        public async Task<IActionResult> Paynow(string gateway,string amount)
        {
            ViewBag.title = "Products";

            if(amount == null || amount == "")
            {
                //var paynow = new Paynow("9848", "9cb58118-b8e3-45bc-b5b5-66a29ce71309");
                var paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");

                paynow.ResultUrl = "https://deedsapp.ttcsglobal.com/gateways/paynow/update";
                paynow.ReturnUrl = $"https://deedsapp.ttcsglobal.com/Products/Paynow?gateway={gateway}&amount=80";
                    //"http://example.com/return?gateway=paynow";
                // The return url can be set at later stages. You might want to do this if you want to pass data to the return url (like the reference of the transaction)

                // Create a new payment 
                var payment = paynow.CreatePayment(Guid.NewGuid().ToString(),"brightonkofu@outlook.com");

                payment.Add("Name search", 1);
                // Send payment to paynow
                var response = paynow.Send(payment); //, "0771111111", "ecocash"

                // Add items to the payment


                // Check if payment was sent without error
                if (response.Success())
                {
                    // Get the url to redirect the user to so they can make payment
                    var link = response.RedirectLink();
                    ViewBag.PaynowLink = link;

                    // Get the poll url of the transaction
                    var pollUrl = response.PollUrl();

                    var status = paynow.PollTransaction(pollUrl);

                    ViewBag.Paid = "false";
                    Globals.Globals.payment = paynow;
                    Globals.Globals.response = response;

                    //if (status.Paid())
                    //{
                    //    ViewBag.Paid = "true";
                    //}
                    //else
                    //{
                    //    ViewBag.Paid = "false";
                    //}
                }

            }
            else
            {
                var payment = Globals.Globals.payment;
                var response = Globals.Globals.response;                

                var status = payment.PollTransaction(response.PollUrl());
                if (status.Paid())
                {
                    var client = new HttpClient();
                    var responsey = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_pay_for_search}", gateway).Result.Content.ReadAsStringAsync();
                    PostSearchResponse psr = JsonConvert.DeserializeObject<PostSearchResponse>(responsey);

                    ViewBag.Paid = "true";
                    ViewBag.Amount = status.Amount;
                    ViewBag.RefNumber = status.Reference;
                }
                else
                {
                    ViewBag.Paid = "false";
                }
            }
            // Create an instance of the paynow class
            return View();
        }

        [HttpPost("Paid")]
        public IActionResult Paid()
        {
            ViewBag.title = "About";
            return View();
        }

        [HttpGet("ConvertToCR6")]
        public ActionResult Convert(string nameRef)
        {
            /////////////////////////////////////////////////////////////////////////
            //Using Iron PDF
            //  IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            var clientw = new HttpClient();
            var res = clientw.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_search_id}?ID={nameRef}").Result.Content.ReadAsStringAsync().Result;
            var i = 0;

            dynamic json_data = JsonConvert.DeserializeObject(res);
            var data = json_data.data.value.searchNames;
            List<mSearchNames> names = JsonConvert.DeserializeObject<List<mSearchNames>>(data.ToString());

            var name = names.Where(z => z.Status == "Reserved").FirstOrDefault();

            var dattta = json_data.data.value.searchInfo;
            mSearchInfo searchInfo = JsonConvert.DeserializeObject<mSearchInfo>(dattta.ToString());

            i = 2;

            DateTime regDate = DateTime.Parse(searchInfo.ApprovedDate);
            DateTime expDate = regDate.AddDays(30);
            // using ironpdf



            var Renderer = new IronPdf.HtmlToPdf();


            List<string> HtmlList = new List<string>();
            string[] HtmlArray;
            // Render an HTML document or snippet as a string



            string Dummy = "dummy data";

            //HEADER STARTS HERE


            string style = @"<style> body{ background: white); } div{ padding:40px; padding-top:10px; padding-bottom:16%; }  </style > ";

            HtmlList.Add(style);

            string html = " <body> <page size = 'A4'> <div> <p style = 'text-align: center;' ><img src = 'Views/Assets/img/2000px-Coat_of_arms_of_Zimbabwe.svg.png' /></p>     <p style = 'text-align: center;'> &nbsp;</p>" +
                "<center style = 'text-align: center; font-size:18pt;'><b>ZIMBABWE</b></center><br> <p style = 'text-align: center; font-size:16pt;'>  COMPANIES AND OTHER BUSINESS ENTITIES ACT <br>[CHAPTER 24:31] <br><br>Form C.V. 4 </p> <p style = 'text-align: center;' > &nbsp;</p> ";
            HtmlList.Add(html);

            string html2 = "<p style='font-size:14pt;'>COMPANIES REGISTRATION OFFICE<br> P.O.BOX CY 117<br> Causeway<br> Harare<br> Tel 0242775544-6<br> <br> </p>";
            HtmlList.Add(html2);

            var searcherId = searchInfo.Searcher_ID;
            var user = db.AspNetUsersDetails.Where(z => z.email == searcherId).FirstOrDefault();

            string html3 = $"<p style='font-size:14pt;'> {user.firstname} {user.lastname}<br> {user.address}<br> {user.city}<br> {user.country}<br> <br> <p style='font-size:14pt;'>Dear Sir/Madam</p></p> ";
            HtmlList.Add(html3);

            string html4 = $"<p style='font-size:16pt;'><b>REF: {searchInfo.SearchRef}  {name.Name} P/L </b> </p> ";
            HtmlList.Add(html4);

            string html5 = $" <p style='font-size:14pt;'> I acknowledge your application for company name search and do hereby confirm that the above choice has been reserved up to {expDate} </p> <p style='text - align: center; '>&nbsp;</p>  ";
            HtmlList.Add(html5);

            string html6 = @"<p style='font-size:14pt;'>Yours faithfully,</p> <p style='text - align: center; '>&nbsp;</p> <p style='font-size:14pt;'> For Registrar  </p >    <h></h>  </div> ";
            HtmlList.Add(html6);
            

            //GeneratedBarcode QRWithLogo = QRCodeWriter.CreateQrCode($"Company Name: " +
            //   $"Company Number:" + "url to app");
            //QRWithLogo.ResizeTo(100, 50).SetMargins(1).ChangeBarCodeColor(Color.Black);
            //QRWithLogo.SaveAsPng($"C:/My/QRCode.png").SaveAsPdf($"C:/My/QRCode.png.pdf");



            //create the final html string
            // HtmlList.Add(html11;          
            HtmlArray = HtmlList.ToArray();
            string finalhtml = string.Concat(HtmlArray);

            var PDF = Renderer.RenderHtmlAsPdf(finalhtml);
            string OutputPath = "C:/My/HtmlToPDFRAW.pdf";

            PDF.SaveAs(OutputPath);


            Renderer.RenderHtmlAsPdf(finalhtml).SaveAs(OutputPath);
          //  QRWithLogo.StampToExistingPdfPage("C:/My/" + "HtmlToPDFRAW.pdf", 280, 725, 1);
            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] byteArray = client.DownloadData(OutputPath);


            //////////////////////////////////////////////////////////////////////
            ViewBag.title = "New Search";
            return new FileContentResult(byteArray, "application/pdf");

        }

        [HttpPost("LiabilityClauseUrlDataSource")]
        public IActionResult LiabilityClauseUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (liabilityClause == null)
            //    liabilityClause = new List<LiabilityClauseExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetLiabilityObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<LiabilityClauseExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<LiabilityClauseExaminerDto> GetLiabilityObjects()
        {
            if (liabilityClause == null)
                liabilityClause = new List<LiabilityClauseExaminerDto>();
            int Count = liabilityClause.Count();
            return liabilityClause;
        }

        [HttpPost("LiabilityClauseCellEditUpdate")]
        public IActionResult LiabilityClauseCellEditUpdate([FromBody] CRUDModel<LiabilityClauseExaminerDto> value)
        {
            if (liabilityClause == null)
                liabilityClause = new List<LiabilityClauseExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }

        [HttpPost("UrlShareClauseDataSource")]
        public IActionResult UrlShareClauseDataSource([FromBody] DataManagerRequest dm)
        {
            //if (shareClause == null)
            //    shareClause = new List<ShareClauseExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetShareClauseObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<ShareClauseExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        
        }

        private List<ShareClauseExaminerDto> GetShareClauseObjects()
        {
            if (shareClause == null)
                shareClause = new List<ShareClauseExaminerDto>();
            int Count = shareClause.Count();
            return shareClause;
        }

        [HttpPost("ShareClauseCellEditUpdate")]
        public IActionResult ShareClauseCellEditUpdate([FromBody] CRUDModel<ShareClauseExaminerDto> value)
        {
            if (shareClause == null)
                shareClause = new List<ShareClauseExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }

        [HttpPost("MemorandumUrlDataSource")]
        public IActionResult MemorandumUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (memoObjects == null)
            //    memoObjects = new List<MemorandumExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetMemoObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<MemorandumExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<MemorandumExaminerDto> GetMemoObjects()
        {
            if (memoObjects == null)
                memoObjects = new List<MemorandumExaminerDto>();
          
            return memoObjects;
        }

        [HttpPost("MemorandumCellEditUpdate")]
        public IActionResult MemorandumCellEditUpdate([FromBody] CRUDModel<MemorandumExaminerDto> value)
        {
            var ord = value.Value;
            MemorandumExaminerDto val = memoObjects.Where(or => or.Obj_Num == value.Value.Obj_Num&&  or.Application_Ref== ord.Application_Ref).FirstOrDefault();
            if (val != null)
            {
                val.TheObjectHasQuery = ord.TheObjectHasQuery;
                val.TheObjectComment = ord.TheObjectComment;
            }
           
            
            
            return Json(memoObjects);
           
        }

        [HttpPost("SelectedArticleUrlDataSource")]
        public IActionResult SelectedArticleUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (selectedModel == null)
            //    selectedModel = new List<ArticlesExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetSelectedArticleObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<ArticlesExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<ArticlesExaminerDto> GetSelectedArticleObjects()
        {
            if (selectedModel == null)
                selectedModel = new List<ArticlesExaminerDto>();
            int Count = selectedModel.Count();
            return selectedModel;
        }

        [HttpPost("SelectedArticleCellEditUpdate")]
        public IActionResult SelectedArticleCellEditUpdate([FromBody] CRUDModel<ArticlesExaminerDto> value)
        {
            if (selectedModel == null)
                selectedModel = new List<ArticlesExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }


        [HttpPost("AmmendedArticleUrlDataSource")]
        public IActionResult AmmendedArticleUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (ammendedArticles == null)
            //    ammendedArticles = new List<AmmendedArticlesExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetAmmendedArticleObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<AmmendedArticlesExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<AmmendedArticlesExaminerDto> GetAmmendedArticleObjects()
        {
            if (ammendedArticles == null)
                ammendedArticles = new List<AmmendedArticlesExaminerDto>();
            int Count = ammendedArticles.Count();
            return ammendedArticles;
        }

        [HttpPost("AmmendedArticleCellEditUpdate")]
        public IActionResult AmmendedArticleCellEditUpdate([FromBody] CRUDModel<AmmendedArticlesExaminerDto> value)
        {
            if (ammendedArticles == null)
                ammendedArticles = new List<AmmendedArticlesExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }

        [HttpPost("MemberUrlDataSource")]
        public IActionResult MemberUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (members == null)
            //    members = new List<MemberExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetMemberObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<MemberExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<MemberExaminerDto> GetMemberObjects()
        {
            if (members == null)
                members = new List<MemberExaminerDto>();
            int Count = members.Count();
            return members;
        }

        [HttpPost("MemberCellEditUpdate")]
        public IActionResult MemberCellEditUpdate([FromBody] CRUDModel<MemberExaminerDto> value)
        {
            if (members == null)
                members = new List<MemberExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }
        [HttpPost("UrlDatasource")]

        public IActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<MemorandumExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        //public ActionResult CellEditUpdate([FromBody] CRUDModel<Orders> value)
        //{
        //    var ord = value.Value;
        //    Orders val = order.Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
        //    val.OrderID = ord.OrderID;
        //    val.EmployeeID = ord.EmployeeID;
        //    val.CustomerID = ord.CustomerID;
        //    val.Freight = ord.Freight;
        //    val.OrderDate = ord.OrderDate;
        //    val.ShipCity = ord.ShipCity;
        //    return Json(value.Value);
        //}

        public List<MemorandumExaminerDto> GetObjects()
        {
            if (memoObjects == null)
                memoObjects = new List<MemorandumExaminerDto>();
           
            return memoObjects;
        }

        ////insert the record
        [HttpPost("CellEditInsert")]

        public ActionResult CellEditInsert([FromBody] CRUDModel<mmainClause> value)
        {
            if (objects == null)
                objects = new List<mmainClause>();
            objects.Insert(0, value.Value);
            return Json(value);
        }
        [HttpPost("EntityUrlDataSource")]
        public IActionResult EntityUrlDataSource([FromBody] DataManagerRequest dm)
        {
            //if (memberEntities == null)
            //    memberEntities = new List<EntityExaminerDto>();
            //shareClause.Insert(0, value.Value);
            //return Json(value);

            IEnumerable DataSource = GetEntityMembersObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<EntityExaminerDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<EntityExaminerDto> GetEntityMembersObjects()
        {
            if (memberEntities == null)
                memberEntities = new List<EntityExaminerDto>();
            int Count = memberEntities.Count();
            return memberEntities;
        }

        [HttpPost("EntityCellEditUpdate")]
        public IActionResult EntityCellEditUpdate([FromBody] CRUDModel<EntityExaminerDto> value)
        {
            if (memberEntities == null)
                memberEntities = new List<EntityExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
