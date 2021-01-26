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

            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            //var client = new HttpClient();
            //var res = await client.GetAsync($"{Globals.Globals.end_point_get_company_applications}").Result.Content.ReadAsStringAsync();
            //db.Dispose();
            //dynamic json_data = JsonConvert.DeserializeObject(res);

            //var data = json_data.data.value;
            //List<mCompany> enqs = JsonConvert.DeserializeObject<List<mCompany>>(data.ToString());


            //List<mCompanyInfor> Summary = new List<mCompanyInfor>();
            //int p = 1;

            //foreach (mCompany item in enqs)
            //{
            //    mCompanyInfor items = new mCompanyInfor();

            //    items.Application_Ref = item.CompanyInfo.Application_Ref;
            //    items.RegNumber = item.CompanyInfo.RegNumber;
            //    items.AppliedBy = item.CompanyInfo.AppliedBy;
            //    items.Name = item.CompanyInfo.Name;
            //    items.No_Of_Directors = item.CompanyInfo.No_Of_Directors;
            //    //items.Objective = item.CompanyInfo.Objective;
            //    items.Date_Of_Application = item.CompanyInfo.Date_Of_Application;
            //    items.Type = item.CompanyInfo.Type;
            //    items.Status = item.CompanyInfo.Status;

            //    Summary.Add(items);
            //    if (p ==3 )
            //    {
            //        //break;
            //    }
            //    p++;
            //}

            //List<mCompanyInfor> rejesteredCompanies = Summary.Where(q => q.AppliedBy == user.UserName).ToList();
            //List<mCompanyInfor> pendingCompanies = Summary.Where(q => q.Status == "Pending").ToList();
            //ViewBag.title = "Summary of Companies";
            //ViewBag.datasource = rejesteredCompanies;
            //ViewBag.companyApplication = pendingCompanies;

            //var resp = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_user_v1}?UserID={user.Email}").Result.Content.ReadAsStringAsync();
            //json_data = JsonConvert.DeserializeObject(resp);

            //data = json_data.data.value;
            //List<mSearch> nameSearches = JsonConvert.DeserializeObject<List<mSearch>>(data.ToString());
            //List<mSearchInfo> nameSearchSummary = new List<mSearchInfo>();

            //foreach(mSearch search in nameSearches)
            //{
            //    nameSearchSummary.Add(search.searchInfo);
            //}

            //ViewBag.nameSearches = nameSearchSummary;
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
            List<Queries> query = new List<Queries>();
            foreach (var item in companyApplications)
            {
                var query1 = context.Queries
                    .Where(q => q.applicationRef == item.companyInfo.Application_Ref && q.status == "Pending")
                    .ToList();
                foreach (var query1item in query1)
                {
                    query.Add(query1item);
                }
                        
            }
            ViewBag.Queries = query;
           
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
            ViewBag.Assigned = namesb.Count;

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

            //var paidApplications = companyApplications.Where(z => z.companyInfo.Payment.Equals("Paid")).ToList();
            ViewBag.CompanyApps = companyApplications.Count;
            ViewBag.Unassigned = names.Count + unassignedApplications.Count;

            //Cpunt for total number of namesearches
            List<mSearch> namesd = JsonConvert.DeserializeObject<List<mSearch>>(searchNames.ToString());
            ViewBag.TotalNameSearches = namesd.Count;

            ViewBag.title = "Principal Dashboard";
            return View();
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
