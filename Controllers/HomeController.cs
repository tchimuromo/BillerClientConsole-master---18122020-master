using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;
using BillerClientConsole.Models.QueryModel;
using BillerClientConsole.Data;

namespace BillerClientConsole.Controllers
{
    [Authorize]
    [Route("Home")]
    [Route("")]
    public class HomeController : Controller
    {

        private dbContext db = new dbContext();
        private readonly QueryDbContext context;

        // private QueryDbContext context = new QueryDbContext();
        public HomeController(QueryDbContext context)
        {
            this.context = context;
        }
        [Route("Dashboard")]
        [Route("")]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.title = "Dashboard";

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            
            ViewBag.Role = user.Role;

            var client = new HttpClient();

            //notifications
            //var notifications_read = await client.GetAsync($"{Globals.Globals.end_point_countReadNotificationsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var notifications_un_read = await client.GetAsync($"{Globals.Globals.end_point_countUnReadNotificationsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var notifications_total = await client.GetAsync($"{Globals.Globals.end_point_countAllNotificationsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();

            ViewBag.notifications_read = 0;
            ViewBag.notifications_un_read = 0;
            ViewBag.notifications_total = 0;
            
            
            //products
            //var products_active = await client.GetAsync($"{Globals.Globals.end_point_countAllActiveProductsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var products_in_active = await client.GetAsync($"{Globals.Globals.end_point_countAllInActiveProductsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var products_total = await client.GetAsync($"{Globals.Globals.end_point_countAllProductsForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();

            ViewBag.products_active = 0;
            ViewBag.products_in_active = 0;
            ViewBag.products_total = 0;


            //transactions and payments
            //var payment_history = await client.GetAsync($"{Globals.Globals.end_point_countAllPaymentHistoryForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var transaction_history = await client.GetAsync($"{Globals.Globals.end_point_countAllTransactionHistoryForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();

            ViewBag.payment_history = 0;
            ViewBag.transaction_history = 0;

             //feedback
            // read_feedback = await client.GetAsync($"{Globals.Globals.end_point_countAllReadFeedBackForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            //var un_read_feedback = await client.GetAsync($"{Globals.Globals.end_point_countAllUnReadFeedBackForBiller}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();

            ViewBag.read_feedback = 0;
            ViewBag.un_read_feedback = 0;


            if (ViewBag.Role == 1)
            {
                return RedirectToAction("PrincipalDashboard");
            }
            else if (ViewBag.Role == 2)
            {
                return RedirectToAction("ExaminerTasks");
            }
            else if (ViewBag.Role == 4)
            {
                return RedirectToAction("RegistrarDashboard");
            }

            var resp = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_user_v1}?UserID={user.UserName}").Result.Content.ReadAsStringAsync();

            dynamic json_data = JsonConvert.DeserializeObject(resp);

            var data = json_data.data.value;
            List<mSearch> nameSearches = JsonConvert.DeserializeObject<List<mSearch>>(data.ToString());


            List<mSearchInfo> nameSearchSummary = new List<mSearchInfo>();

            foreach (mSearch search in nameSearches)
            {
                nameSearchSummary.Add(search.searchInfo);
            }
            nameSearchSummary = nameSearchSummary.OrderByDescending(z => z.SearchRef).ToList();
            ViewBag.nameSearches = nameSearchSummary;

            var responsey = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_user_id}?UserID={user.UserName}").Result.Content.ReadAsStringAsync();
            dynamic json_dataa = JsonConvert.DeserializeObject(responsey);
            var dataa = json_dataa.data.value;
            List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(dataa.ToString());
            companyApplications = companyApplications.OrderByDescending(x=> x.companyInfo.Date_Of_Application).ToList();

            
            var paymentsHistory = await client.GetAsync($"{Globals.Globals.end_point_payment}{user.UserName}").Result.Content.ReadAsStringAsync();
            var creditsHistory = await client.GetAsync($"{Globals.Globals.end_point_payments_credits}{user.UserName}").Result.Content.ReadAsStringAsync();

            dynamic hist_data = JsonConvert.DeserializeObject(paymentsHistory);
            PaymentsResponse response = JsonConvert.DeserializeObject<PaymentsResponse>(hist_data.ToString());

            dynamic json_data_credits = JsonConvert.DeserializeObject(creditsHistory);
            CreditCounts credits = JsonConvert.DeserializeObject<CreditCounts>(json_data_credits.ToString());

            int paymentsCount = response.Payments.Count();
            if(paymentsCount > 3) {
                List<PaymentDto> newPayments = new List<PaymentDto>();
                for (int i = 0; i < 3; i++)
                {
                    newPayments.Add(response.Payments[response.Payments.Count - (i + 1)]);
                }
                response.Payments = newPayments;
            }

            var regEntitiesSummaryResponse = await client.GetAsync($"{Globals.Globals.service_end_point}/{user.UserName}/RegisteredEntities").Result.Content.ReadAsStringAsync();
            dynamic regEntitiesSummaryJson;
            List<RegisteredEntitySummary> entitiesSummary = new List<RegisteredEntitySummary>();
            try
            {
                regEntitiesSummaryJson = JsonConvert.DeserializeObject(regEntitiesSummaryResponse);
                 entitiesSummary = JsonConvert.DeserializeObject<List<RegisteredEntitySummary>>(regEntitiesSummaryJson.ToString());
            }
            catch(Exception ex)
            {

            }
           


              ViewBag.Balance = response.AccountBalance;
            ViewBag.Payments = response.Payments;
            ViewBag.credits = credits;
            if(entitiesSummary.Count > 0)
            {
                ViewBag.RegEntitiesSummary = entitiesSummary;
            }
            
            ViewBag.EntityApplications = companyApplications;
           
           
            return View();
        }

        [HttpGet("PrincipalExaminer")]
        public IActionResult PrincipalDashboard()
        {
            //Count for number of unassigned tasks
            var client = new HttpClient();
            var res = client.GetAsync($"{Globals.Globals.end_point_get_name_searches}").Result.Content.ReadAsStringAsync().Result;
            dynamic data_j = JsonConvert.DeserializeObject(res);
            var searchNames = data_j.data.value;
            List<mSearch> names = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
            names = names.Where(z => z.searchInfo.Satus == "Pending" && z.searchInfo.Payment == "Paid").ToList();
            //ViewBag.Unassigned = names.Count;



            // Count for number of assigned tasks
            List<mSearch> namesb = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
            namesb = namesb.Where(z => z.searchInfo.Satus == "Assigned").ToList();
            ViewBag.Assigned = namesb.Count;//Assigned Company Applications 

            // Count for Namesearches Paid for
            //List<mSearch> namesc = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
            //namesc = namesc.Where(z => z.searchInfo.Satus == "Paid").ToList();
            //ViewBag.Paidfor = namesc.Count;

            //Number of companies
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
            dynamic dataa_j = JsonConvert.DeserializeObject(resp);
            var companies = dataa_j.data.value;
            List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            List<mCompanyResponse> unassignedApplications = companyApplications.Where(s => string.IsNullOrEmpty(s.companyInfo.Examiner) && !string.IsNullOrEmpty(s.companyInfo.Payment) && !string.IsNullOrEmpty(s.companyInfo.Search_Ref)).ToList(); //!string.IsNullOrEmpty(s.companyInfo.Payment) && 
            ViewBag.Applications = unassignedApplications;

            //Company Applications with Examiner's and Pending Status
            List<mCompanyResponse> AssignedCompanyApplications = companyApplications.Where(s => !string.IsNullOrEmpty(s.companyInfo.Examiner) && s.companyInfo.Status=="Assigned").ToList();//.OrderBy(s.c)
            ViewBag.AssignedCompanyApplications = AssignedCompanyApplications.Count;

            //var paidApplications = companyApplications.Where(z => z.companyInfo.Payment.Equals("Paid")).ToList();
            ViewBag.CompanyApps = companyApplications.Count;
            ViewBag.Unassigned = names.Count + unassignedApplications.Count;

            //Cpunt for total number of namesearches
            List<mSearch> namesd = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
            ViewBag.TotalNameSearches = namesd.Count;

            var response = client.GetAsync($"{Globals.Globals.end_point_getalltasks}").Result.Content.ReadAsStringAsync().Result;
            dynamic task = JsonConvert.DeserializeObject(response);
            var tasks = task.data.value;
            List<mTasks> AllTask = JsonConvert.DeserializeObject<List<mTasks>>(tasks.ToString());


            //Filter NameSearch Task and Select Pending Task 
            var SearchNameTasks = AllTask.Where(e => e.Service == "Name Search" && (e.Status == "Pending" || e.Status == "Assigned")).ToList();
            ViewBag.SearchNameTasks = SearchNameTasks.Count;
            ViewBag.title = "Principal Dashboard";
            return View();
        }

        //[HttpGet("ReassignCompanyApplicationTask")]
        //public IActionResult ReassignCompanyApplicationTask(string taskid)
        //{

        //}
        
        [HttpGet("AssignedCompanyApplications")]
        public IActionResult AssignedCompanyApplications()
        {
            HttpClient client = new HttpClient();
            //Get all the Tasks Including Company Applications
            var response = client.GetAsync($"{Globals.Globals.end_point_getalltasks}").Result.Content.ReadAsStringAsync().Result;
            dynamic task = JsonConvert.DeserializeObject(response);
            var tasks = task.data.value;
            List<mTasks> AllTask = JsonConvert.DeserializeObject<List<mTasks>>(tasks.ToString());


            //Filter NameSearch Task and Select Pending Task 
            var SearchNameTasks = AllTask.Where(e => e.Service == "Private Company Registration" && e.Status == "Pending").OrderBy(e=>e.ExpDateofComp).ToList();
            //Return a list of Pending NameSeraches
            return View(SearchNameTasks);

        }

        [HttpGet("ExaminerTasks")]
        public async Task<IActionResult> ExaminerTasks(string display)
        {
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.title = "Examiner tasks";
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_get_assigned_tasks}?UserID={user.Email}").Result.Content.ReadAsStringAsync(); //TODO change with examiner user id
            db.Dispose();
            AssinedTasksResponse resp = JsonConvert.DeserializeObject<AssinedTasksResponse>(res);
            db.Dispose();

            if (string.IsNullOrEmpty(display))
            {
                List<mTasks> tasks = new List<mTasks>();
                List<mTasks> Completedtasks = new List<mTasks>();

                  foreach (mTasks task in resp.data.value.Where(z => z.Status == "Pending").ToList())
                {
                    tasks.Add(task);
                }

                foreach (mTasks task in resp.data.value.Where(z => z.Status == "Completed").ToList())
                {
                    Completedtasks.Add(task);
                }

                var a = 1;
                tasks = tasks.OrderByDescending(z => z.ExpDateofComp).ToList();
                //tasks = task
                ViewBag.tasks = tasks;
                ViewBag.CompletedTasks = Completedtasks;
            }
            else if (display.Equals("CompanyApplications"))
            {
                var response = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
                dynamic dataa_j = JsonConvert.DeserializeObject(response);
                var companies = dataa_j.data.value;
                List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
                List<mCompanyResponse> companiesAlloc = companyApplications.Where(s => !string.IsNullOrEmpty(s.companyInfo.Examiner) && s.companyInfo.Examiner.Equals("Examiner 1")).ToList(); //TODO replace "Examiner 1" with user examiner
                List<mCompanyResponse> companiesAllocated = companiesAlloc.OrderByDescending(z => z.companyInfo.Date_Of_Application).ToList();

                ViewBag.CompanyApplications = companiesAllocated.OrderByDescending(z => z.companyInfo.Search_Ref).ToList();
                ViewBag.Display = display;
            }
            return View();
        }
        [Route("About")]
        public IActionResult About()
        {
            ViewBag.title = "About";
            return View();
        }


        [Route("Products")]
        public IActionResult Products()
        {
            ViewBag.title = "Products";
            return View();
        }

        [HttpGet("ReassignNamesearchTask")]
        public IActionResult ReassignNamesearchTask(string taskId)
        {
            HttpClient client = new HttpClient();
            //Get all the Tasks Including Company Applications
            var response = client.GetAsync($"{Globals.Globals.end_point_getalltasks}").Result.Content.ReadAsStringAsync().Result;
            dynamic task = JsonConvert.DeserializeObject(response);
            var tasks = task.data.value;
            List<mTasks> AllTask = JsonConvert.DeserializeObject<List<mTasks>>(tasks.ToString());
            var GetTask = AllTask.Where(e => e._id == taskId).FirstOrDefault();
            ViewBag.Assigner=GetTask.Assigner;
            ViewBag.Service = GetTask.Service;
            ViewBag.Status = GetTask.Status;
            ViewBag.Date = GetTask.Date;
            ViewBag.Id = GetTask._id;
            ViewBag.AssignTo = GetTask.AssignTo;
            ViewBag.NoOfRecords = GetTask.NoOfRecords;
            ViewBag.ExpDateofComp = GetTask.ExpDateofComp;
            var examiner = db.AspNetUsersInternal.Where(e => e.role == 2).ToList();
            var num = examiner.Count;
            ViewBag.people = examiner;
            return View();
        }

        [HttpPost("ReassignNamesearchTask")]
        public IActionResult ReassignNamesearchTask(mTasks model)
        {
           // if(model.Service)
            HttpClient client = new HttpClient();
            var response = client.PostAsJsonAsync($"{Globals.Globals.end_point_ReassignTask}/{model._id}", model).Result;
            if (response.IsSuccessStatusCode)
            {
                if(model.Service == "Name Search")
                {
                    return RedirectToAction("NameSearchTasks", "Home");
                }
                return RedirectToAction("AssignedCompanyApplications", "Home");
            }
            return BadRequest();
        }
        [Route("NameSearchTasks")]
        public IActionResult NameSearchTasks()
        {
            HttpClient client = new HttpClient();
            //Get all the Tasks Including Company Applications
            var response = client.GetAsync($"{Globals.Globals.end_point_getalltasks}").Result.Content.ReadAsStringAsync().Result;
            dynamic task = JsonConvert.DeserializeObject(response);
            var tasks = task.data.value;
            List<mTasks> AllTask = JsonConvert.DeserializeObject<List<mTasks>>(tasks.ToString());


            //Filter NameSearch Task and Select Pending Task 
            var SearchNameTasks = AllTask.Where(e => e.Service == "Name Search" && (e.Status == "Pending"|| e.Status=="Assigned")).ToList();
            //Return a list of Pending NameSeraches
            return View(SearchNameTasks);
        }

        [HttpGet("RegistrarDashboard")]
        public IActionResult RegistrarDashboard()
        {
            var client = new HttpClient();

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var resp = client.GetAsync($"{Globals.Globals.end_point_get_company_application}").Result.Content.ReadAsStringAsync().Result;
            dynamic dataa_j = JsonConvert.DeserializeObject(resp);
            var companies = dataa_j.data.value;
            List<mCompanyResponse> companyApplications = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            companyApplications = companyApplications.Where(z => z.companyInfo.Status == "Recommended").ToList();

            // counts
            //recommended
            List<mCompanyResponse> recommended = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            recommended = recommended.Where(z => z.companyInfo.Status == "Recommended"  ).ToList();
            var recommendedApplications = recommended.Count();

            //approved
            List<mCompanyResponse> approved = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            approved = approved.Where(z => z.companyInfo.Status == "Approved").ToList();
            var approvedApplications = approved.Count();
            //ALLapproved
            string username = "registrar@registrar.com";
            List<mCompanyResponse> allApproved = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            allApproved = allApproved.Where(z => z.companyInfo.Approved_By == username).ToList();
            var allApprovedApplications = allApproved.Count();

            //assigned
            List<mCompanyResponse> assigned = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            assigned = assigned.Where(z => z.companyInfo.Status == "Assigned").ToList();
            var assignedApplications = assigned.Count();

            //pending
            List<mCompanyResponse> pending = JsonConvert.DeserializeObject<List<mCompanyResponse>>(companies.ToString());
            pending = pending.Where(z => z.companyInfo.Status == "Pending").ToList();
            var pendingApplications = pending.Count();


            ViewBag.companyApplications = companyApplications;
            ViewBag.recommendedCount = recommendedApplications;
            ViewBag.approvedApplications = approvedApplications;
            ViewBag.assignedApplications = assignedApplications;
            ViewBag.pendingApplications = pendingApplications;
            ViewBag.allApprovedApplications = allApprovedApplications;

            ViewBag.title = "Registrar Dashboard";
            return View();

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
