using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BillerClientConsole.Controllers
{
    [Route("Account")]
    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        private dbContext db = new dbContext();



        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            ViewBag.title = "Account / Change Password";
            return View();
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string new_password, string current_password)
        {
            ViewBag.title = "Account / Change Password";
            var user_id = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(user_id);
            var result = await userManager.ChangePasswordAsync(user, current_password, new_password);

            if (result.Succeeded)
            {
                TempData["tmsg"] = "Password Changed";
                TempData["type"] = "success";
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                TempData["tmsg"] = "Password Changed Failed";
                TempData["type"] = "error";
            }
            return View();
        }


        /// <summary>
        /// view biller details
        /// </summary>
        /// <returns></returns>
        [HttpGet("BillerDetails")]
        public async Task<IActionResult> BillerDetails()
        {
            ViewBag.title = "Account / Biller Details";
            ///fetch the biller info from the server
            var client = new HttpClient();

            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var response = await client.GetAsync($"{Globals.Globals.end_point_GetBillerInfoByBillerCode}?billercode={user.BillerCode}").Result.Content.ReadAsStringAsync(); ;
            dynamic json_data = JsonConvert.DeserializeObject(response.ToString());

            if (json_data.res == "ok")
            {
                ViewBag.bank = JsonConvert.DeserializeObject<mBankAccount>(json_data.bank.ToString());
                ViewBag.address = JsonConvert.DeserializeObject<mAddress>(json_data.address.ToString());
                ViewBag.billerInfo = JsonConvert.DeserializeObject<mBillerInfo>(json_data.biller.ToString());

                if (ViewBag.bank == null) ViewBag.bank = new mBankAccount();
                if (ViewBag.address == null) ViewBag.address = new mAddress();
                if (ViewBag.billerInfo == null) ViewBag.billerInfo = new mBillerInfo();
            }

            else
            {
                ViewBag.bank = new mBankAccount();
                ViewBag.address = new mAddress();
                ViewBag.billerInfo = new mBillerInfo();
                TempData["tmsg"] = "Error retrieving details";
                TempData["type"] = "error";
            }
            return View();
        }


        [HttpPost("UpdateBillerDetails")]
        public async Task<IActionResult> UpdateBillerDetails(mBillerInfo biller, mBankAccount bankAccount, mAddress address)
        {
            try
            {
                var data = Json(new
                {
                    biller = JsonConvert.SerializeObject(biller),
                    bankAccount = JsonConvert.SerializeObject(bankAccount),
                    address = JsonConvert.SerializeObject(address)
                }).Value.ToString();
                data = data.Replace("=", ":");

                var client = new HttpClient();
                var response = await client.PostAsync($"{Globals.Globals.end_point_UpdateBillerInfoByBillerCode}?data={data}", null).Result.Content.ReadAsStringAsync();
                dynamic json_data = JsonConvert.SerializeObject(response);
                if (json_data.res == "ok")
                {
                    TempData["tmsg"] = "Details Saved";
                    TempData["type"] = "success";
                }
                else
                {
                    TempData["tmsg"] = "Error";
                    TempData["type"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["tmsg"] = "Error occurred";
                TempData["type"] = "error";
            }
            return RedirectToAction("BillerDetails");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
