using BillerClientConsole.Data;
using BillerClientConsole.Models;
using BillerClientConsole.Models.QueryModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BillerClientConsole.Controllers
{
    [Route("Queries")]
    public class QueriesController : Controller
    {
        //private dbContext db = new dbContext();
        //public static List<mmainClause> objects = new List<mmainClause>();
        //private static List<ShareClauseExaminerDto> shareClause = new List<ShareClauseExaminerDto>();
        //private static List<LiabilityClauseExaminerDto> liabilityClause = new List<LiabilityClauseExaminerDto>();
        //private static List<MemorandumExaminerDto> memoObjects = new List<MemorandumExaminerDto>();
        //private static List<ArticlesExaminerDto> selectedModel = new List<ArticlesExaminerDto>();
        //public static List<MemberDto> entityMember = new List<MemberDto>();
        //private static List<AmmendedArticlesExaminerDto> ammendedArticles = new List<AmmendedArticlesExaminerDto>();
        //public static List<MemberExaminerDto> members = new List<MemberExaminerDto>();
        //private static List<EntityExaminerDto> memberEntities1 = new List<EntityExaminerDto>();
        //private static List<MemberDto> memberEntities = new List<MemberDto>();
        //public static List<mCompanyResponse> companyResponses = new List<mCompanyResponse>();
        //private readonly QueryDbContext context;

        // private readonly QueryDbContext context;

        private dbContext db = new dbContext();
        public static List<mmainClause> objects = new List<mmainClause>();
        private static List<ShareClauseExaminerDto> shareClause = new List<ShareClauseExaminerDto>();
        private static List<LiabilityClauseExaminerDto> liabilityClause = new List<LiabilityClauseExaminerDto>();
        private static List<MemorandumExaminerDto> memoObjects = new List<MemorandumExaminerDto>();
        private static List<ArticlesExaminerDto> selectedModel = new List<ArticlesExaminerDto>();
        public static List<MemberDto> entityMember = new List<MemberDto>();
        private static List<AmmendedArticlesExaminerDto> ammendedArticles = new List<AmmendedArticlesExaminerDto>();
        public static List<MemberExaminerDto> members = new List<MemberExaminerDto>();
        // private static List<MemberDto> memberEntities1 = new List<MemberDto>();
        private static List<MemberDto> memberEntities = new List<MemberDto>();
        public static List<mCompanyResponse> companyResponses = new List<mCompanyResponse>();
        private readonly QueryDbContext context;

       // private readonly QueryDbContext context;

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
        [HttpPost("MemberCellEditInsert")]
        public ActionResult MemberCellEditInsert([FromBody] CRUDModel<MemberExaminerDto> value)
        {
            if (members == null)
                members = new List<MemberExaminerDto>();
            if (value != null)
                members.Insert(0, value.Value);
            return Json(value);
        }
        /// <summary>
        /// ResolveShareClauseQuery
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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
            PostSearchResponse ps = JsonConvert.DeserializeObject<PostSearchResponse>(response);
            if (ps.res == "ok")
            {

            }
            Globals.Globals.searchApplicationID = Globals.Globals.tempSearchId1;
            return RedirectToAction("ListBillerProducts");   //ViewBag.datasource = order;
            return View();

        }

        [HttpPost("CompanyUrlDataSource")]
        public IActionResult CompanyUrlDataSource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetMemberEntityObjects();
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
            int count = DataSource.Cast<MemberDto>().Count();
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
        private IEnumerable GetMemberEntityObjects()
        {
            return entityMember;
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
            int count = DataSource.Cast<MemberDto>().Count();
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
        private List<MemberDto> GetEntityMembersObjects()
        {
            if (memberEntities == null)
                memberEntities = new List<MemberDto>();
            int Count = memberEntities.Count();
            return memberEntities;
        }

        [HttpPost("MemberCellEditUpdate")]
        public IActionResult MemberCellEditUpdate([FromBody] CRUDModel<MemberExaminerDto> value)
        {
            if (members == null)
                members = new List<MemberExaminerDto>();
            //shareClause.Insert(0, value.Value);
            return Json(value);
        }
        [HttpPost("EntityCellEditInsert")]
        public ActionResult EntityCellEditInsert([FromBody] CRUDModel<MemberDto> value)
        {
            if (entityMember == null)
                entityMember = new List<MemberDto>();
            if (value != null)
                entityMember.Insert(0, value.Value);
            return Json(value);
        }


        //Get Members Data
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
        public List<MemorandumExaminerDto> GetObjects()
        {
            if (memoObjects == null)
                memoObjects = new List<MemorandumExaminerDto>();

            return memoObjects;
        }
        [HttpPost("AddCompanyMemorandum")]
        public async Task<ActionResult> AddCompanyMemorandumAsync(MemoViewModel model)
        {
            var client = new HttpClient();
            if (model.applicationRef != null)
            {
                var respo = await client.PostAsJsonAsync($"{Globals.Globals.end_point_Update_memoInfo}", model);
                if (respo.IsSuccessStatusCode)
                    return RedirectToAction("Dashboard", "Home");
                return BadRequest();
            }
            else
                return Ok();



        }

        //[HttpGet("ApplicationResubmission")]
        //public async Task<IActionResult> ApplicationResubmission()
        //{

        //}
        [HttpGet("ApplicationResubmission/{id}")]
        public async Task<IActionResult> ApplicationResubmission(string id)
        {
            if (id != null)
            {
                HttpClient client = new HttpClient();
                var queryExists = context.Queries.Where(query => query.applicationID == id && query.status == "Pending").ToList();
                if (queryExists.Count > 0)
                {

                    return BadRequest();

                }
                else
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync($"{Globals.Globals.end_point_resubmit_application}/{id}", "");//Result.Content.ReadAsStringAsync()
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();

                    }
                    return BadRequest();

                }

            }
            else
            {
                return BadRequest();
            }

        }
        public QueriesController(QueryDbContext context)
        {
            this.context = context;
        }
        [HttpPost("RegisteredOfficeHasQuery")]
        public async Task<IActionResult> RegisteredOfficeHasQuery(Queries query)
        {
            //Code to check if there exist a Query with same ref number
            var QueryExits = context.Queries.Where(q => q.applicationRef == query.applicationRef && q.tableName == "Step2");
            if (QueryExits.Count() > 0)
            {
                if (query.HasQuery == true)
                {
                    //If it exists and has another query Update the current 
                    Queries queryExists = QueryExits.FirstOrDefault();
                    queryExists.QueryCount += 1;
                    queryExists.comment = query.comment;
                    queryExists.status = "Pending";


                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step2",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var queryExists = QueryExits.FirstOrDefault();
                    queryExists.status = "Resolved";
                    queryExists.HasQuery = false;
                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();
                    //Updating the Query History Table
                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Resolved",
                        tableName = "Step2",

                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                if (query.HasQuery == true)
                {
                    query.status = "Pending";
                    query.dateCreated = DateTime.Now.ToString();
                    query.QueryCount += 1;
                    query.tableName = "Step2";
                    context.Queries.Add(query);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step2",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        //ShareClause Query Handling
        [HttpPost("LiabilityAndShareClauseHasQuery")]
        public async Task<IActionResult> ShareClauseHasQuery(Queries query)
        {
            var QueryExits = context.Queries.Where(q => q.applicationRef == query.applicationRef && q.tableName == "Step4");
            if (QueryExits.Count() > 0)
            {
                if (query.HasQuery == true)
                {
                    //If it exists and has another query Update the current 
                    Queries queryExists = QueryExits.FirstOrDefault();
                    queryExists.QueryCount += 1;
                    queryExists.comment = query.comment; ;
                    queryExists.status = "Pending";

                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step4",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var queryExists = QueryExits.FirstOrDefault();
                    queryExists.status = "Resolved";
                    queryExists.HasQuery = false;
                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();
                    //Updating the Query History Table
                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Resolved",
                        tableName = "Step4",

                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                if (query.HasQuery == true)
                {
                    query.status = "Pending";
                    query.dateCreated = DateTime.Now.ToString();
                    query.QueryCount += 1;
                    query.tableName = "Step4";
                    context.Queries.Add(query);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step4",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("MembersHasQuery")]
        public async Task<IActionResult> MembersHasQuery(Queries query)
        {
            var QueryExits = context.Queries.Where(q => q.applicationRef == query.applicationRef && q.tableName == "Step3");
            if (QueryExits.Count() > 0)
            {
                if (query.HasQuery == true)
                {
                    //If it exists and has another query Update the current 
                    Queries queryExists = QueryExits.FirstOrDefault();
                    queryExists.QueryCount += 1;
                    queryExists.comment = query.comment; ;
                    queryExists.status = "Pending";

                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step3",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var queryExists = QueryExits.FirstOrDefault();
                    queryExists.status = "Resolved";
                    queryExists.HasQuery = false;
                    context.Queries.Update(queryExists);
                    await context.SaveChangesAsync();
                    //Updating the Query History Table
                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Resolved",
                        tableName = "Step3",

                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                if (query.HasQuery == true)
                {
                    query.status = "Pending";
                    query.dateCreated = DateTime.Now.ToString();
                    query.QueryCount += 1;
                    query.tableName = "Step3";
                    context.Queries.Add(query);
                    await context.SaveChangesAsync();

                    var forqueryhistory = new QueryHistory()
                    {
                        applicationRef = query.applicationRef,
                        comment = query.comment,
                        dateCreated = DateTime.Now.ToString(),
                        status = "Pending",
                        tableName = "Step3",
                    };
                    context.QueryHistory.Add(forqueryhistory);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("QueryCard/{applicationID}")]
        public IActionResult QueryCard(string applicationID)
        {
            List<Queries> query = new List<Queries>();
           
                var query1 = context.Queries
                    .Where(q => q.applicationID == applicationID && q.status == "Pending")
                    .ToList();
                foreach (var query1item in query1)
                {
                    query.Add(query1item);
                }

            ViewBag.applicationID = applicationID;
            return View(query);
        }


        [HttpGet("ResolveQuery/{id}")]
        [HttpGet("ResolveQuery/{applicationRef}")]
        public async Task<IActionResult> ResolveQuery( string step, string applicationRef, string id=null, string applicationID=null)
        {
            var client = new HttpClient();
            //Code to get Registered Office Details
            if (step == "Step2")
            {
                var registeredOfficeExists = await client.GetAsync($"{Globals.Globals.service_end_point}/RegisteredOffice/{id}").Result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RegisteredOffice>(registeredOfficeExists);
                return View(model);
            }
            else if (step == "Step3")
            {   //ViewBag.CompanyApplication = companyApplication;
                // Redirecting to an another Action with the model data from database........
                return RedirectToAction("ResolveMembersQuery", new { applicationID = applicationID });
                
                ///return ResolveMembersQuery(companyApplication);
            }
            else if (step == "Step4")
            {
                return RedirectToAction("ResolveShareClauseQuery", new { applicationID=applicationID});
            }

                   
            return NotFound();
        }

        [HttpGet("QueryCard/{applicationID}")]
        public IActionResult QueryCard(string applicationID)
        {
            List<Queries> query = new List<Queries>();

            var query1 = context.Queries
                .Where(q => q.applicationID == applicationID && q.status == "Pending")
                .ToList();
            foreach (var query1item in query1)
            {
                query.Add(query1item);
            }

            ViewBag.applicationID = applicationID;
            return View(query);
        }


        [HttpGet("ResolveQuery/{id}")]
        [HttpGet("ResolveQuery/{applicationRef}")]
        public async Task<IActionResult> ResolveQuery(string step, string applicationRef, string id = null, string applicationID = null)
        {
            var client = new HttpClient();
            //Code to get Registered Office Details
            if (step == "Step2")
            {
                var registeredOfficeExists = await client.GetAsync($"{Globals.Globals.service_end_point}/RegisteredOffice/{id}").Result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RegisteredOffice>(registeredOfficeExists);
                return View(model);
            }
            else if (step == "Step3")
            {   //ViewBag.CompanyApplication = companyApplication;
                // Redirecting to an another Action with the model data from database........
                return RedirectToAction("ResolveMembersQuery", new { applicationID = applicationID });

                ///return ResolveMembersQuery(companyApplication);
            }
            else if (step == "Step4")
            {
                return RedirectToAction("ResolveShareClauseQuery", new { applicationID = applicationID });
            }


            return NotFound();
        }

        [HttpGet("ResolveShareClauseQuery")]
        public async Task<IActionResult> ResolveShareClauseQuery(string applicationID)
        {
            var client = new HttpClient();

            var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={applicationID}").Result.Content.ReadAsStringAsync();

            dynamic json_dataa = JsonConvert.DeserializeObject(rhisponzi);
            dynamic dataa = json_dataa;
            try
            {
                dataa = json_dataa.data.value;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            mCompanyResponse companyApplication = JsonConvert.DeserializeObject<mCompanyResponse>(dataa.ToString());
            ViewBag.companyApplication = companyApplication;
            List<Subscriber> subscribers = new List<Subscriber>();
            ViewBag.Application = companyApplication.companyInfo.Application_Ref;
            ViewBag.ApplicationID = companyApplication.companyInfo.Search_Ref;//Search Ref or ApplicationID
            var nameOfficeResponse = await client.GetAsync($"{Globals.Globals.service_end_point}/{companyApplication.companyInfo.Search_Ref}/Namesearch/{companyApplication.companyInfo.Office}/Office").Result.Content.ReadAsStringAsync();
            dynamic nameOfficeJson = JsonConvert.DeserializeObject(nameOfficeResponse);
            NameOfficeResponse nameOffice = JsonConvert.DeserializeObject<NameOfficeResponse>(nameOfficeJson.ToString());
            ViewBag.NameOffice = nameOffice;
            ViewBag.ApplicationRef = companyApplication.memo._id;
            //foreach (var clause in companyApplication.memo.LiabilityClause)
            //{
            //    ViewBag.Liability_liab = clause.description;
            //}

            //foreach (var clause in companyApplication.memo.SharesClause)
            //{
            //    ViewBag.Share_share = clause.description;
            //}

            ViewBag.article_type = companyApplication.articles.articles_type;

            // var k = 0;
            if (companyResponses == null)
            {

                companyResponses = new List<mCompanyResponse>();

                companyResponses.Add(companyApplication);
                var liability = companyApplication.memo.LiabilityClause;
                var ShareClause = companyApplication.memo.SharesClause;

                //foreach (liabilityClause lc in liability)
                //{
                //    if (lc.Status == 0)
                //    {
                //        if (!string.IsNullOrEmpty(lc.description))
                //        {
                //            LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                //            liablity.LiabilityClause = lc.description;
                //            liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                //            liabilityClause.Add(liablity);
                //        }

                //    }
                //}

                //foreach (sharesClause sc in ShareClause)
                //{
                //    if (sc.Status == 0)
                //    {
                //        if (!string.IsNullOrEmpty(sc.description))
                //        {
                //            ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                //            share.ShareClause = sc.description;
                //            share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                //            shareClause.Add(share);
                //        }
                //    }

                //}



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
                    MemberDto entities = new MemberDto();

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
                        entities.FirstName = membr.Names;
                        entities.Nationality = membr.Nationality;
                        entities.TotalShares = follio.number_of_shares.ToString();

                        memberEntities.Add(entities);
                    }
                }
            }
            else
            {
                var cor = companyResponses.Where(s => s.companyInfo.Search_Ref == applicationID).FirstOrDefault();
                if (cor == null)
                {
                    companyResponses.Add(companyApplication);
                    ////foreach (liabilityClause lc in companyApplication.memo.LiabilityClause)
                    ////{
                    ////    if (lc.Status == 0)
                    ////    {
                    ////        if (!string.IsNullOrEmpty(lc.description))
                    ////        {
                    ////            LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                    ////            liablity.LiabilityClause = lc.description;
                    ////            liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    ////            liabilityClause.Add(liablity);
                    ////            ViewBag.LiabilityClause = liablity;
                    ////        }

                    ////    }
                    ////}

                    //foreach (sharesClause sc in companyApplication.memo.SharesClause)
                    //{
                    //    if (sc.Status == 0)
                    //    {
                    //        if (!string.IsNullOrEmpty(sc.description))
                    //        {
                    //            ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                    //            share.ShareClause = sc.description;
                    //            share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    //            shareClause.Add(share);
                    //            ViewBag.ShareClause = share;
                    //        }

                    //    }
                    //}


                    //foreach (mmainClause obj in companyApplication.memo.objects)
                    //{
                    //    MemorandumExaminerDto memo = new MemorandumExaminerDto();
                    //    memo.TheObject = obj.objective;
                    //    memo.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    //    memoObjects.Add(memo);
                    //}

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
                        MemberDto entities = new MemberDto();

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
                            entities.FirstName = membr.Names;
                            entities.Nationality = membr.Nationality;
                            entities.EntityNumber = membr.member_id;
                            entities.PreferenceShares = follio.PreferenceShares.ToString();
                            entities.OrdinaryShares = follio.OrdinaryShares.ToString();
                            entities.TotalShares = follio.number_of_shares.ToString();

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
            ViewBag.ApplicationID = applicationID;

            //var order = OrdersDetails.GetAllRecords();
            //ViewBag.datasource = order;
            return View();

        }
        [HttpGet("UpdateShareClause/{applicationID}")]
        public IActionResult UpdateShareClauseQuery(string applicationID)
        {
            var query = context.Queries.Where(e => e.applicationID == applicationID && e.status == "Pending" && e.tableName == "Step4");
            var queryObject = query.FirstOrDefault();
            ViewBag.applicationID = queryObject.applicationID;
            queryObject.status = "Resolved";
            context.Queries.Update(queryObject);
            context.SaveChanges();
            return Ok();
        }

        [HttpGet("ResolveMembersQuery")]
        public async Task<IActionResult> ResolveMembersQuery(string applicationID)
        {
            // ViewBag.Title = "Customer";

            var client = new HttpClient();

            var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={applicationID}").Result.Content.ReadAsStringAsync();

            dynamic json_dataa = JsonConvert.DeserializeObject(rhisponzi);
            dynamic dataa = json_dataa;
            try
            {
                dataa = json_dataa.data.value;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            mCompanyResponse companyApplication = JsonConvert.DeserializeObject<mCompanyResponse>(dataa.ToString());
            ViewBag.companyApplication = companyApplication;
            List<Subscriber> subscribers = new List<Subscriber>();
            ViewBag.Application = companyApplication.companyInfo.Application_Ref;
            ViewBag.ApplicationID = companyApplication.companyInfo.Search_Ref;//Search Ref or ApplicationID
            var nameOfficeResponse = await client.GetAsync($"{Globals.Globals.service_end_point}/{companyApplication.companyInfo.Search_Ref}/Namesearch/{companyApplication.companyInfo.Office}/Office").Result.Content.ReadAsStringAsync();
            dynamic nameOfficeJson = JsonConvert.DeserializeObject(nameOfficeResponse);
            NameOfficeResponse nameOffice = JsonConvert.DeserializeObject<NameOfficeResponse>(nameOfficeJson.ToString());
            ViewBag.NameOffice = nameOffice;



            //foreach (var clause in companyApplication.memo.LiabilityClause)
            //{
            //    ViewBag.Liability_liab = clause.description;
            //}

            //foreach (var clause in companyApplication.memo.SharesClause)
            //{
            //    ViewBag.Share_share = clause.description;
            //}

            ViewBag.article_type = companyApplication.articles.articles_type;

            // var k = 0;
            if (companyResponses == null)
            {

                companyResponses = new List<mCompanyResponse>();

                companyResponses.Add(companyApplication);
                var liability = companyApplication.memo.LiabilityClause;
                var ShareClause = companyApplication.memo.SharesClause;

                //foreach (liabilityClause lc in liability)
                //{
                //    if (lc.Status == 0)
                //    {
                //        if (!string.IsNullOrEmpty(lc.description))
                //        {
                //            LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                //            liablity.LiabilityClause = lc.description;
                //            liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                //            liabilityClause.Add(liablity);
                //        }

                //    }
                //}

                //foreach (sharesClause sc in ShareClause)
                //{
                //    if (sc.Status == 0)
                //    {
                //        if (!string.IsNullOrEmpty(sc.description))
                //        {
                //            ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                //            share.ShareClause = sc.description;
                //            share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                //            shareClause.Add(share);
                //        }
                //    }

                //}



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
                    MemberDto entities = new MemberDto();

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
                        entities.FirstName = membr.Names;
                        entities.Nationality = membr.Nationality;
                        entities.TotalShares = follio.number_of_shares.ToString();

                        memberEntities.Add(entities);
                    }
                }
            }
            else
            {
                var cor = companyResponses.Where(s => s.companyInfo.Search_Ref == applicationID).FirstOrDefault();
                if (cor == null)
                {
                    companyResponses.Add(companyApplication);
                    ////foreach (liabilityClause lc in companyApplication.memo.LiabilityClause)
                    ////{
                    ////    if (lc.Status == 0)
                    ////    {
                    ////        if (!string.IsNullOrEmpty(lc.description))
                    ////        {
                    ////            LiabilityClauseExaminerDto liablity = new LiabilityClauseExaminerDto();
                    ////            liablity.LiabilityClause = lc.description;
                    ////            liablity.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    ////            liabilityClause.Add(liablity);
                    ////            ViewBag.LiabilityClause = liablity;
                    ////        }

                    ////    }
                    ////}

                    //foreach (sharesClause sc in companyApplication.memo.SharesClause)
                    //{
                    //    if (sc.Status == 0)
                    //    {
                    //        if (!string.IsNullOrEmpty(sc.description))
                    //        {
                    //            ShareClauseExaminerDto share = new ShareClauseExaminerDto();
                    //            share.ShareClause = sc.description;
                    //            share.Application_Ref = companyApplication.companyInfo.Application_Ref;
                    //            shareClause.Add(share);
                    //            ViewBag.ShareClause = share;
                    //        }

                    //    }
                    //}


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
                        MemberDto entities = new MemberDto();

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
                            entities.FirstName = membr.Names;
                            entities.Nationality = membr.Nationality;
                            entities.EntityNumber = membr.member_id;
                            entities.PreferenceShares = follio.PreferenceShares.ToString();
                            entities.OrdinaryShares = follio.OrdinaryShares.ToString();
                            entities.TotalShares = follio.number_of_shares.ToString();

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
            string[] sex = { "Male", "Female" };
            ViewBag.sex = sex;
            ViewBag.applicationID = applicationID;

            //var order = OrdersDetails.GetAllRecords();
            //ViewBag.datasource = order;
            return View();
        }
        [HttpGet("UpdateMembers/{applicationID}")]
        public IActionResult UpdateMembers(string applicationID)
        {
            var query = context.Queries.Where(e => e.applicationID == applicationID && e.status == "Pending" && e.tableName == "Step3");
            var queryObject = query.FirstOrDefault();
            ViewBag.applicationID = queryObject.applicationID;
            if (query.Count() > 0)
            {
                queryObject.status = "Resolved";
                context.Queries.Update(queryObject);
                context.SaveChanges();
                return RedirectToAction("QueryCard","Queries", new { applicationID = applicationID });
            }
            return RedirectToAction("QueryCard","Queries", new { applicationID = applicationID });
        }

       // Delete the record
       [HttpPost("CellEditDelete")]
        public ActionResult CellEditDelete([FromBody] CRUDModel<MemberDto> value)
        {
            var m = value.Value;
        MemberDto val = entityMember.Where(e => e.EntityNumber == m.EntityNumber).FirstOrDefault();
            entityMember.Remove(val);
       // entityMember.Remove(entityMember.Where(e => e.NationalId == m.NationalId)).FirstOrDefault();
            return Json(value);
    }

    [HttpPost("PostResolveQuery")]

        public async Task<IActionResult> PostResolveQuery(RegisteredOffice model)
        {
            var client = new HttpClient();
            if (ModelState.IsValid)
            {
               var result= await client.PostAsJsonAsync($"{Globals.Globals.service_end_point}/UpdateRegisteredOffice", model);
                if (result.IsSuccessStatusCode)
                {
                    var query = context.Queries.Where(e => e.officeid == model.OfficeId && e.status == "Pending" && e.tableName == "Step2");
                    var queryObject = query.FirstOrDefault();
                    var applicationID = queryObject.applicationID;
                    queryObject.status = "Resolved";
                    context.Queries.Update(queryObject);
                    context.SaveChanges();
                    return RedirectToAction("QueryCard", new { applicationID = applicationID });
                }
            }
            return View(model);
        }


    }
}
