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
    [Route("ajax/products")]
    public class ProductsPartialController : Controller
    {
        /// <summary>
        /// invoked from ajax
        /// fetches the biller products async
        /// and returns a partial view with the products are rendered
        /// </summary>
        /// <param name="billercode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("BillerProducts")]
        public async Task<IActionResult> BillerProducts(string id)
        {
            //string id = "7ea8add6-8340-405b-8d34-d4f7298eade2";
            var db = new dbContext();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var res = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_id}?ID={id}").Result.Content.ReadAsStringAsync();
            db.Dispose();
            dynamic json_data = JsonConvert.DeserializeObject(res);

            var data = json_data.data.value;
            mSearch enqs = JsonConvert.DeserializeObject<mSearch>(data.ToString());


            ViewBag.data = enqs.searchInfo;
            ViewBag.names = enqs.SearchNames;

            return PartialView("_ListBillerProducts");
        }
        [Route("CompanyInformation")]
        public async Task<IActionResult> CompanyInformation()
        {

            return PartialView("_CompanyInformation");
        }

        [Route("CompanyMemo")]
        public async Task<IActionResult> CompanyMemo()
        {
           
            return PartialView("_CompanyMemo");
        }
        [Route("CompanyMembers")]
        public async Task<IActionResult> CompanyMembers()
        {
            //List<mSearchNames> sn = new List<mSearchNames>();
            //sn.Add(new mSearchNames { Name = "test" });
            //var data = sn;
            //ViewBag.dataSource = data;



            return PartialView("_CompanyMembers");
        }
        [Route("CompanyDirectors")]
        public async Task<IActionResult> CompanyDirectors()
        {
            return PartialView("_CompanyDirctors");
        }
    }
}
