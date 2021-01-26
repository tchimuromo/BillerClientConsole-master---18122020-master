using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using BillerClientConsole.Models;
using Newtonsoft.Json;
using X.PagedList;
using System.Globalization;

namespace BillerClientConsole.Controllers.Partials
{

    /// <summary>
    /// will return partial enquiries views called by ajax
    /// </summary>
    [Route("ajax/enquiries")]
    public class EnquiriesPartialController : Controller
    {
        /// <summary>
        /// invoked from ajax
        /// fetches the biller products async
        /// and returns a partial view with the products are rendered
        /// </summary>
        /// <param name="billercode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("ListAllEnquiries")]
        public async Task<IActionResult> ListAllEnquiries(bool open,bool closed)
        {
            //now using datatables , paging and pagenumber now obsolete
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_GetAllBillerEnquiries}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            db.Dispose();
            dynamic json = JsonConvert.DeserializeObject(res);
            List<mServiceEnquiry> enqs = new List<mServiceEnquiry>();
            if (json.res == "ok")
            {
                if (json.msg.ToString().Length > 5)
                {
                    enqs = JsonConvert.DeserializeObject<mServiceEnquiry>(json.msg.ToString());
                    if (open)
                    {
                        enqs = enqs.Where(i => !i.isRead).ToList();
                    }
                    if (closed)
                    {
                        enqs = enqs.Where(i => i.isRead).ToList();
                    }
                }
            }
            else
            {
                TempData["tmsg"] = "Error fetching the Enquirires";
                TempData["type"] = "error";
            }
            ViewBag.enqs = enqs;
            return PartialView("_ListAllEnquiries");
        }



        [Route("ListAllEnquiryQuestions")]
        public async Task<IActionResult> ListAllEnquiryQuestions(bool open, bool closed)
        {
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_GetAllBillerEnquiries}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync();
            db.Dispose();
            dynamic json = JsonConvert.DeserializeObject(res);
            List<mServiceEnquiry> enqs = new List<mServiceEnquiry>();
            if (json.res == "ok")
            {
                if (json.msg.ToString().Length > 5)
                {
                    enqs = JsonConvert.DeserializeObject<mServiceEnquiry>(json.msg.ToString());
                    if (open)
                    {
                        enqs = enqs.Where(i => !i.isRead).ToList();
                    }
                    if (closed)
                    {
                        enqs = enqs.Where(i => i.isRead).ToList();
                    }
                }
            }
            else
            {
                TempData["tmsg"] = "Error fetching the Enquirires";
                TempData["type"] = "error";
            }
            ViewBag.enqs = enqs;
            return PartialView("_ListAllEnquiries");
        }


    }
}
