using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    [Route("PaymentHistory")]
    [Authorize]
    public class PaymentController : Controller
    {
        [Route("ViewDetails/{id}")]
        public async Task<IActionResult> ViewDetails(int id)
        {
            ViewBag.title = "Report";
            //fetch the feedback from the api
            var client    = new HttpClient();
            var response  = await client.GetAsync($"{Globals.Globals.end_point_get_paymenthistory_by_id}/?id={id}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(response);
            ViewBag.PaymentDate = json_data.PaymentDate;
            ViewBag.PaidAmount = json_data.PaidAmount;
            ViewBag.TrackingID = json_data.TrackingID;
            ViewBag.BatchNo = json_data.BatchNo;
            return View();
        }


    }
}
