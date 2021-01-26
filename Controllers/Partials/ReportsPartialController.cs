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
    /// will return partial report views called by ajax
    /// </summary>
    [Route("ajax/reports")]
    public class ReportsPartialController : Controller
    {
        /// <summary>
        /// invoked from ajax
        /// fetches the feedback async
        /// and returns a partial view with the feedback rendered
        /// </summary>
        /// <param name="billercode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("Feedback")]
        public async Task<IActionResult> Feedback(int pageNumber, string billercode,string date_from,string date_to)
        {
            //now using datatables , paging and pagenumber now obsolete
            if (pageNumber < 1) pageNumber = 1;//ensure page never goes below one
            var client = new HttpClient();
            //var result = await client.GetAsync($"{Globals.Globals.end_point_biller_feedback}?BillerCode={billercode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            //dynamic json_data = JsonConvert.DeserializeObject(result);
            //var data = json_data.data;
            //List<mFeedback> feedback = JsonConvert.DeserializeObject<List<mFeedback>>(data.ToString());
            List<mFeedback> feedback = new  List<mFeedback>();
            //ViewBag.pfeedback = feedback;//.AsQueryable().ToPagedList(pageNumber, 5);
            //ViewBag.pageNumber = pageNumber;
            //if(!string.IsNullOrEmpty(date_from))
            //{
            //    ViewBag.date_from = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //}
            //if (!string.IsNullOrEmpty(date_to))
            //{
            //    ViewBag.date_to = DateTime.ParseExact(date_to, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //}
            return PartialView("_ListFeedback",feedback);
        }



        [Route("PaymentHistory")]
        public async Task<IActionResult> PaymentHistory(int pageNumber, string billercode, string date_from, string date_to)
        {
            //now using datatables , paging and pagenumber now obsolete
            if (pageNumber < 1) pageNumber = 1;//ensure page never goes below one
            var client = new HttpClient();
            var result = await client.GetAsync($"{Globals.Globals.end_point_biller_paymenthistory}?BillerCode={billercode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(result);
            var data = json_data.data;
            List<mPayments> payments = JsonConvert.DeserializeObject<List<mPayments>>(data.ToString());
            ViewBag.payments = payments;//.AsQueryable().ToPagedList(pageNumber, 5);
            ViewBag.pageNumber = pageNumber;
            if (!string.IsNullOrEmpty(date_from))
            {
                ViewBag.date_from = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(date_to))
            {
                ViewBag.date_to = DateTime.ParseExact(date_to, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            return PartialView("_ListPaymentHistory", payments);
        }


        [Route("TransactionHistory")]
        public async Task<IActionResult> TransactionHistory(int pageNumber, string billercode, string date_from, string date_to)
        {
            //now using datatables , paging and pagenumber now obsolete
            if (pageNumber < 1) pageNumber = 1;//ensure page never goes below one
            var client = new HttpClient();
            var result = await client.GetAsync($"{Globals.Globals.end_point_biller_transactionhistory}?BillerCode={billercode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(result);
            var data = json_data.data;
            List<mReceipts> transactions = JsonConvert.DeserializeObject<List<mReceipts>>(data.ToString());
            ViewBag.transactions = transactions;//.AsQueryable().ToPagedList(pageNumber, 5);
            ViewBag.pageNumber = pageNumber;
            if (!string.IsNullOrEmpty(date_from))
            {
                ViewBag.date_from = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(date_to))
            {
                ViewBag.date_to = DateTime.ParseExact(date_to, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            return PartialView("_ListTransactionHistory", transactions);
        }


    }
}
