using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Dtos;
using System.Net.Http;
using BillerClientConsole.Globals;
using BillerClientConsole.Models;
using Newtonsoft.Json;
using BillerClientConsole.Data;
using BillerClientConsole.Models.QueryModel;

namespace BillerClientConsole.Controllers
{
    [Route("Examination")]
    public class ExaminationController : Controller
    {
        private readonly QueryDbContext context;

        public ExaminationController(QueryDbContext context)
        {
            this.context = context;
        }
        [HttpPost("AddressHasQuery")]
        public async Task<IActionResult> ExamineAddressAsync(Query query)
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync<Query>($"{Globals.Globals.end_point_post_address_has_query}", query);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }
       
       
/// <summary>
/// /Controller to get application Details
/// </summary>
/// <param name="applicationId"></param>
/// <returns></returns>
        [HttpGet("{applicationId}/detail")]
        public async Task<IActionResult> ApplicationFromExaminer(string applicationId)
        {
            ViewBag.Title = "Customer";

            var client = new HttpClient();

            var rhisponzi = await client.GetAsync($"{Globals.Globals.service_end_point}/{applicationId}/Details").Result.Content.ReadAsStringAsync();
            dynamic json_dataa = JsonConvert.DeserializeObject(rhisponzi);
            

            CompanyApplicationForRewiew companyApplication = JsonConvert.DeserializeObject<CompanyApplicationForRewiew>(json_dataa.ToString());
            ViewBag.CompanyApplication = companyApplication;
            ViewBag.email = companyApplication.office.EmailAddress;

            //displaying Queries for the client user
            List<Queries> query = new List<Queries>();
            
                var query1 = context.Queries
                    .Where(q => q.applicationRef == applicationId)
                    .ToList();
                foreach (var query1item in query1)
                {
                    query.Add(query1item);
                }

            ViewBag.Queries = query;

            return View();
        }
    }
}
