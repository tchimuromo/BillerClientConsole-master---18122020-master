using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    public class PrincipalExaminerController : Controller
    {
        [Route("PrincipalExaminer")]
        [Route("Home")]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
