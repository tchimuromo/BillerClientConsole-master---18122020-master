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
    [Route("NamesearchReservation")]

    public class NamesearchReservationController : Controller
    {

        private dbContext db = new dbContext();
        private readonly QueryDbContext context;


        [HttpGet("Contains")]

        public async Task<IActionResult> Dashboard(string name)
        {
            //name = "TA";
            var client = new HttpClient();
            var resp = await client.GetAsync($"{Globals.Globals.end_point_FilterNames }?name={name}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(resp);
            var dattta = json_data.data.value;
            List<mSearchNames> search = JsonConvert.DeserializeObject<List<mSearchNames>>(dattta.ToString());

            ViewBag.filters = search;
            //ViewBag.title = "Dashboard";
            // return View();
            return Json(new
            {
                //res = search,
                data = search

            });

        }



        [HttpGet("Namesearchview")]

        public async Task<IActionResult> Viewname(string searchRef, string searchid)
        {
            //name = "TA";
            var client = new HttpClient();

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var resp = await client.GetAsync($"{Globals.Globals.end_point_namesearch_by_searchref }?searchRef={searchRef}").Result.Content.ReadAsStringAsync();
            IEnumerable<mSearchNames> json_data = JsonConvert.DeserializeObject<IEnumerable<mSearchNames>>(resp);
            var reserved = json_data.FirstOrDefault(e => e.Status == "Reserved");
            // mCompanyInfo companyInfo = JsonConvert.DeserializeObject<mCompanyInfo>(dattta.ToString());
            // var dattta = json_data.value;
            // List<mSearchNames> search = JsonConvert.DeserializeObject<mSearchNames>(dattta.ToString());
            var bysearchid = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_id}?ID={searchid}").Result.Content.ReadAsStringAsync();
            dynamic json_datab = JsonConvert.DeserializeObject(bysearchid);
            var data = json_datab.data.value.searchInfo;
            mSearchInfo nameinfo = JsonConvert.DeserializeObject<mSearchInfo>(data.ToString());
           // var nameinfo = json_datab;


            var paymentsHistory = await client.GetAsync($"{Globals.Globals.end_point_payment}{user.UserName}").Result.Content.ReadAsStringAsync();
            var creditsHistory = await client.GetAsync($"{Globals.Globals.end_point_payments_credits}{user.UserName}").Result.Content.ReadAsStringAsync();

            dynamic hist_data = JsonConvert.DeserializeObject(paymentsHistory);
            PaymentsResponse response = JsonConvert.DeserializeObject<PaymentsResponse>(hist_data.ToString());

            dynamic json_data_credits = JsonConvert.DeserializeObject(creditsHistory);
            CreditCounts credits = JsonConvert.DeserializeObject<CreditCounts>(json_data_credits.ToString());

            int paymentsCount = response.Payments.Count();
            if (paymentsCount > 3)
            {
                List<PaymentDto> newPayments = new List<PaymentDto>();
                for (int i = 0; i < 3; i++)
                {
                    newPayments.Add(response.Payments[response.Payments.Count - (i + 1)]);
                }
                response.Payments = newPayments;
            }
            ViewBag.Balance = response.AccountBalance;
            ViewBag.Payments = response.Payments;
            ViewBag.credits = credits;
            ViewBag.reserved = reserved;
            ViewBag.nameinfo = nameinfo;
            ViewBag.title = "view name";
            return View();


        }

        [HttpGet("CreditStatus")]
        public async Task<IActionResult> CreditStatus(string searchID)
        {
            if (!string.IsNullOrEmpty(searchID))
            {
                var client = new HttpClient();
                var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                var credits = await client.GetAsync($"{Globals.Globals.service_end_point}/Payments/Credits/{user.UserName}").Result.Content.ReadAsStringAsync();
                dynamic json_data_credits = JsonConvert.DeserializeObject(credits);
                CreditCounts creditsFromDb = JsonConvert.DeserializeObject<CreditCounts>(json_data_credits.ToString());




                if (creditsFromDb.NameSearch > 0)
                {
                    var resp = await client.GetAsync($"{Globals.Globals.end_point_reserve_bySearchRef}?searchid={searchID}&useremail={user.UserName}").Result.Content.ReadAsStringAsync();



                    return Redirect("/Home/Dashboard");
                }else
                {

                    return RedirectToAction("CreditStatus", "Payments", new { service = "Name search" }) ;


                }

            }

            return View();
        }

        [HttpGet("FurtherReserveName")]
        public async Task<IActionResult> CreditStatu(string searchID)
        {
            var client = new HttpClient();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            //curl -X GET "https://localhost:44380/api/v1/reserve/bySearchRef?searchid=c9f2cc67-f648-4e37-998e-c108fe41b51e&useremail=Lmuzenda%40gmail.com" -H  "accept: */*"

            var resp = await client.GetAsync($"{Globals.Globals.end_point_reserve_bySearchRef}?searchid={searchID}&useremail={user}").Result.Content.ReadAsStringAsync();

            

            return Redirect("/Home/Dashboard");
        }



    }
}

