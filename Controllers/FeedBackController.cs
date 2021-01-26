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
    [Route("FeedBack")]
    [Authorize]
    public class FeedBackController : Controller
    {
        [Route("ViewDetails/{id}")]
        public async Task<IActionResult> ViewDetails(int id)
        {
            ViewBag.title = "Report";
            //fetch the feedback from the api
            //var client    = new HttpClient();
            //var response  = await client.GetAsync($"{Globals.Globals.end_point_get_feedback_by_id}/?id={id}").Result.Content.ReadAsStringAsync();
            //dynamic json_data = JsonConvert.DeserializeObject(response);
            //ViewBag.id = json_data.id;
            //ViewBag.feedback = json_data.feedback;
            //ViewBag.customer = json_data.customername;
            //ViewBag.phone = json_data.customerphone;
            //ViewBag.date = DateTime.Parse( json_data.date.ToString() ).ToString("yyyy-MM-dd");
            return View();
        }

        [Route("MarkAsRead/{id}/{pageNumber?}/{date_from?}/{date_to?}")]
        public async Task<IActionResult> MarkAsRead(int id, int pageNumber = 1, string date_from = null, string date_to = null)
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;

            var client = new HttpClient();
            //await client.GetAsync($"{Globals.Globals.end_point_mark_as_read_feedback_by_id}?id={id}");
            return RedirectToAction("ListFeedBack","Reports", new { pageNumber, date_from, date_to });
        }

          [Route("MarkAsUnRead/{id}/{pageNumber?}/{date_from?}/{date_to?}")]
        public async Task<IActionResult> MarkAsUnRead(int id, int pageNumber = 1, string date_from = null, string date_to = null)
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;

            var client = new HttpClient();
            await client.GetAsync($"{Globals.Globals.end_point_mark_as_un_read_feedback_by_id}?id={id}");
            return RedirectToAction("ListFeedBack","Reports", new { pageNumber, date_from, date_to });
        }



        [Route("Delete/{id}/{pageNumber?}/{date_from?}/{date_to?}")]
        public async Task<IActionResult> Delete(int id, int pageNumber=1, string date_from=null, string date_to=null)
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;

            var client = new HttpClient();
          //  var response = await client.GetAsync($"{Globals.Globals.end_point_delete_feedback_by_id}?id={id}").Result.Content.ReadAsStringAsync();
            return RedirectToAction("ListFeedBack", "Reports", new { pageNumber, date_from, date_to });
        }


    }
}
