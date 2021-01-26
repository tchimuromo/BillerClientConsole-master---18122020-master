using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;

namespace BillerClientConsole.Controllers
{

    [Route("Enquiries")]
    public class EnquiriesController : Controller
    {

        private dbContext db = new dbContext();
        
        [HttpGet("ListAllEnquiries")]
        public IActionResult ListAllEnquiries(string date_from,string date_to)
        {
            ViewBag.title = "Enquiries";
            TempData["loadUrl"] = "/ajax/enquiries/ListAllEnquiries";
            TempData["date_from"] = date_from;
            TempData["date_to"] = date_to;
            TempData["open"] = false;
            TempData["closed"] = false;
            return View();
        }

        [HttpGet("ListAllOpenEnquiries")]
        public IActionResult ListAllOpenEnquiries(string date_from, string date_to)
        {
            ViewBag.title = "Open Enquiries";
            TempData["loadUrl"] = "/ajax/enquiries/ListAllEnquiries";
            TempData["date_from"] = date_from;
            TempData["date_to"] = date_to;
            TempData["open"] = true;
            TempData["closed"] = false;
            return View();
        }


        [HttpGet("ListAllClosedEnquiries")]
        public IActionResult ListAllClosedEnquiries(string date_from, string date_to)
        {
            ViewBag.title = "Closed Enquiries";
            TempData["loadUrl"] = "/ajax/enquiries/ListAllEnquiries";
            TempData["date_from"] = date_from;
            TempData["date_to"] = date_to;
            TempData["open"] = false;
            TempData["closed"] = true;
            return View();
        }


        [HttpGet("ListAllEnquirQuestions")]
        public IActionResult ListAllEnquirQuestions()
        {
            ViewBag.title = "Enquiry Questions";


            return View();
        }  
        
        
        

        /// <summary>
        /// show the update form
        /// fetch the enquiry by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("EditEnquiry")]
        public async Task<IActionResult> EditEnquiry(int id)
        {
            ViewBag.title = "Edit Enquiry";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var response = await client.GetStringAsync($"{Globals.Globals.end_point_fetchEnquiryQuestionByID}?id={id}");
            dynamic json = JsonConvert.DeserializeObject(response);
            if(json.res=="ok")
            {
                ViewBag.enq = json.msg;
            }
            ViewBag.billercode = user.BillerCode;
            return View();
        }


        //posted from the list of enquiries to create a new enquiry question
        [HttpPost("CreateEnquiry")]
        public async Task<IActionResult> CreateEnquiry(mEnquiryQuestions _question)
        {
            ViewBag.title = "Create Enquiry";
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync($"{Globals.Globals.end_point_postBillerInquiryQuestions}", _question).Result.Content.ReadAsStringAsync();
            return RedirectToAction("CreateEnquiry");
        }


        //update the question
        [HttpPost("UpdateEnquiry")]
        public async Task<IActionResult> UpdateEnquiry(mEnquiryQuestions _question)
        {
            ViewBag.title = "Create Enquiry";
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync($"{Globals.Globals.end_point_UpdateBillerEnquiryByID}", _question).Result.Content.ReadAsStringAsync();
            return RedirectToAction("ListAllEnquiries");
        }

        /// <summary>
        /// delete an enquiry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeleteEnquiryQuestion")]
        public async Task<IActionResult> DeleteEnquiryQuestion(int id)
        {
            var client = new HttpClient();
            var res = await client.PostAsync($"{Globals.Globals.end_point_delete_biller_enquiry_by_id}?id={id}",null);
            return RedirectToAction("ListAllEnquiries");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
