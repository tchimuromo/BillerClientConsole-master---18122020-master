using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BillerClientConsole.Controllers
{
    [Route("Application")]
    public class ApplicationsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
