using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using BillerClientConsole.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    [Route("Reports")]
    [Authorize]
    public class ReportsController : Controller
    {
        private dbContext db = new dbContext();

        /***** reports ******/
        [HttpGet("ListEnquiries")]
        public IActionResult ListEnquiries(int page=0)
        {
            ViewBag.title = "Reports / Enquiries";
            ViewBag.page = page;
            return View();
        }

        
        [HttpGet("ListTransactionHistory")]
        [HttpGet("")]
        public IActionResult ListTransactionHistory(int pageNumber, string date_from, string date_to)
        {
            ViewBag.title = "Reports / Transaction History";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            //ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/reports/TransactionHistory";
            TempData["pageNumber"] = pageNumber;
            TempData["billercode"] = user.BillerCode;
            TempData["date_to"] = date_to;
            TempData["date_from"] = date_from;
            return View();
        }


        [HttpGet("ListPaymentHistory")]
        public IActionResult ListPaymentHistory(int pageNumber, string date_from, string date_to)
        {
            ViewBag.title = "Reports / Payment History";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            //ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/reports/PaymentHistory";
            TempData["pageNumber"] = pageNumber;
            TempData["billercode"] = user.BillerCode;
            TempData["date_to"] = date_to;
            TempData["date_from"] = date_from;
            return View();
        }


        [HttpGet("ListFeedback")]
        public IActionResult ListFeedback(int pageNumber,string date_from,string date_to)
        {
            ViewBag.title = "Reports / Feedback";
            var user = db.AspNetUsers.Where(i=>i.Email==User.Identity.Name).First();

            TempData["loadUrl"] = $"/ajax/reports/Feedback";
            TempData["pageNumber"] = pageNumber;
            TempData["billercode"] = user.BillerCode;
            TempData["date_to"] = date_to;
            TempData["date_from"] =date_from;
            
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
