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
    [Route("AdvancedSearch")]
   
    public class AdvancedSearchController : Controller
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
    }
    }

    