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
    [Route("ajax/question")]
    public class QuestionsPartialController : Controller
    {
        /// <summary>
        /// invoked from ajax
        /// fetches the biller products async
        /// and returns a partial view with the products are rendered
        /// </summary>
        /// <param name="billercode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("CompanyInformation")]
        public async Task<IActionResult> CompanyInformation()
        {
            //now using datatables , paging and pagenumber now obsolete
            //if (pageNumber < 1) pageNumber = 1;//ensure page never goes below one
            //var client = new HttpClient();
            //var result = await client.GetAsync($"{Globals.Globals.end_point_GetAllBillerProducts}?BillerCode={billercode}&date_from={date_from}&date_to={date_to}").Result.Content.ReadAsStringAsync();
            //dynamic json_data = JsonConvert.DeserializeObject(result);
            //if (json_data.res == "err") return PartialView("_ListBillerProducts", null); ;
            ////var data = json_data.data;
            ////List<mBillerProduct> products = JsonConvert.DeserializeObject<List<mBillerProduct>>(data.ToString());
            //ViewBag.products = json_data.data;//.AsQueryable().ToPagedList(pageNumber, 5);
            //ViewBag.pageNumber = pageNumber;
            //if(!string.IsNullOrEmpty(date_from))
            //{
            //    ViewBag.date_from = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //}
            //if (!string.IsNullOrEmpty(date_to))
            //{
            //    ViewBag.date_to = DateTime.ParseExact(date_to, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //}
            return PartialView("_ListBillerProducts");
        }
    }



     


    }

