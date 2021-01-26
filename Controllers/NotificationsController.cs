using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using BillerClientConsole.Models;
using Newtonsoft.Json;

namespace BillerClientConsole.Controllers
{
    [Route("Notifications")]
    [Authorize]
    public class NotificationsController : Controller
    {

        private dbContext db = new dbContext();



        [HttpGet("AllNotifications")]
        public async Task<IActionResult> AllNotifications(string date_from,string date_to)
        {
            ViewBag.title = "Notifications / All Notifications";

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();

            var client = new HttpClient();
            var response = await client.GetAsync($"{Globals.Globals.end_point_fetchBillerNotifications}?billercode={user.BillerCode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            dynamic jdata = JsonConvert.DeserializeObject(response);
            List<mNotification> notifications = null;
            if (jdata.res == "ok")
            {
                notifications = JsonConvert.DeserializeObject<List<mNotification>>(jdata.msg.ToString());
            }
            ViewBag.notifications = notifications ?? new List<mNotification>();
            return View();
        }



        /// <summary>
        /// show the notifications that have been read
        /// </summary>
        /// <returns></returns>
        [HttpGet("Read")]
        public async Task<IActionResult> Read(string date_from, string date_to)
        {
            ViewBag.title = "Notifications / Read";

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();

            var client = new HttpClient();
            var response = await client.GetAsync($"{Globals.Globals.end_point_fetchBillerNotifications}?billercode={user.BillerCode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            dynamic jdata = JsonConvert.DeserializeObject(response);
            List<mNotification> notifications = null;
            if (jdata.res == "ok")
            {
                notifications = JsonConvert.DeserializeObject<List<mNotification>>(jdata.msg.ToString());
                notifications = notifications.Where(i => i.isRead).ToList();
            }
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            ViewBag.notifications = notifications ?? new List<mNotification>();
            return View();
        }


        /// <summary>
        /// show the notifications that have not been read
        /// </summary>
        /// <returns></returns>
        [HttpGet("UnRead")]
        public async Task<IActionResult> UnRead(string date_from, string date_to)
        {
            ViewBag.title = "Notifications / UnRead";

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();

            var client = new HttpClient();
            var response = await client.GetAsync($"{Globals.Globals.end_point_fetchBillerNotifications}?billercode={user.BillerCode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            dynamic jdata = JsonConvert.DeserializeObject(response);
            List<mNotification> notifications = null;
            if (jdata.res == "ok")
            {
                notifications = JsonConvert.DeserializeObject<List<mNotification>>(jdata.msg.ToString());
                notifications = notifications.Where(i => !i.isRead).ToList();
            }
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            ViewBag.notifications = notifications ?? new List<mNotification>();
            return View();
        }


        /// <summary>
        /// view a single notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("ViewDetails/{id}")]
        public async Task<IActionResult> ViewDetails(int id)
        {
            ViewBag.title = "Notification";

            var client = new HttpClient();
            var response = await client.GetAsync($"{Globals.Globals.end_point_fetchBillerNotificationById}/?id={id}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(response);
            if (json_data.res == "ok")
            {
                ViewBag.notification = json_data.msg;
            }
            else
            {
                TempData["tmsg"] = "Error fetching notification";
                TempData["type"] = "error";
                ViewBag.notification = new mNotification();
            }
            return View();
        }

        [Route("MarkAsRead/{id}/{date_from?}/{date_to?}/{view}")]
        public async Task<IActionResult> MarkAsRead(int id, int pageNumber = 1, string date_from = null, string date_to = null,string view= "AllNotifications")
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;

            var client = new HttpClient();
            await client.GetAsync($"{Globals.Globals.end_point_markNotificationAsRead}?id={id}");
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return RedirectToAction(view, "Notifications", new { date_from, date_to });
        }

        [Route("MarkAsUnRead/{id}/{date_from?}/{date_to?}/{view}")]
        public async Task<IActionResult> MarkAsUnRead(int id, int pageNumber = 1, string date_from = null, string date_to = null,string view= "AllNotifications")
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;
            var client = new HttpClient();
            await client.GetAsync($"{Globals.Globals.end_point_markNotificationAsUnRead}?id={id}");
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return RedirectToAction(view, "Notifications", new { date_from, date_to });
        }



        [Route("Delete/{id}/{date_from?}/{date_to?}/{view}")]
        public async Task<IActionResult> Delete(int id, string date_from = null, string date_to = null,string view= "AllNotifications")
        {
            if (date_from == "-1") date_from = null;
            if (date_to == "-1") date_to = null;
            var client = new HttpClient();
            var response = await client.PostAsync($"{Globals.Globals.end_point_deleteNotificationById}?id={id}",null).Result.Content.ReadAsStringAsync();
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return RedirectToAction(view, "Notifications", new { date_from, date_to });
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
