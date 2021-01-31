using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using PasswordGenerator;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    [Authorize]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        private dbContext db = new dbContext();

        private readonly IHttpContextAccessor _http;


        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor http)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _http = http;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);


        private void seed()
        {
            var user = db.AspNetUsers.Where(i => i.Id != null).FirstOrDefault();
            if (user == null)
            {
                var nuser = new IdentityUser { Email = "client@client.com", UserName = "client@client.com", PasswordHash = "Develop#99" };
                userManager.CreateAsync(nuser, nuser.PasswordHash).Wait();
                var nuser1 = new IdentityUser { Email = "principal@principal.com", UserName = "principal@principal.com", PasswordHash = "Develop#99" };
                userManager.CreateAsync(nuser1, nuser1.PasswordHash).Wait();
                var nuser2 = new IdentityUser { Email = "examiner@examiner.com", UserName = "examiner@examiner.com", PasswordHash = "Develop#99" };
                userManager.CreateAsync(nuser2, nuser2.PasswordHash).Wait();
            }

        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public IActionResult Forgotpassword()
        {
            ViewBag.title = "Forgot Password";
            return View();

        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> Forgotpassword(string email)
        {
            //generating new password
            var pwdb = new Password();
            var new_password = pwdb.Next();

            // Calling identity methods or functions for change password
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                TempData["flash"] = "1";
                TempData["error"] = "User is unavailable in system";
                return View();
            }
            else
            {
                string code = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, code, new_password);

                if (result.Succeeded)
                {

                    SmtpClient client = new SmtpClient("mail.ttcsglobal.com");
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("companiesonlinezw", "N3wPr0ducts@1");
                    // client.Credentials = new NetworkCredential("username", "password");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("companiesonlinezw@ttcsglobal.com");
                    mailMessage.To.Add(email);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = ("<!DOCTYPE html> " +
                        "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                        "<head>" +
                            "<title>Email</title>" +
                        "</head>" +

                        "<body style=\"font-family:'Century Gothic'\">" +
                        "<p><b>Hi Dear valued Customer</b></p>" +
                            "<p>Your new password is " + new_password + ".</p>" +
                        "<p>Kindly use the link below to access your account.</p>" +

                        "<a>https://deedsapp.ttcsglobal.com:6868/Auth/Login </a>" +
                        "<p>as a security measure we recomend a that you change your password afterlogin</p>" +
                       "<p> Enjoy our services.</p> " +

                        "<p>Regards</p>" +

                        "<p>DCIP</p>" +

                        "</body>" +
                        "</html>");//GetFormattedMessageHTML();
                    mailMessage.Subject = "Password successfully changed";
                    client.Send(mailMessage);

                    TempData["error"] = "Password has been changed, please check email..=";
                    TempData["flash"] = "2";
                    return View();
                }
                else
                {
                    TempData["error"] = "Password Changed Failed";
                    TempData["flash"] = "1";
                    return View();
                }
            }


            ViewBag.title = "Forgot Password";
            return RedirectToAction("Login", "Auth");

        }



            [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Login(string email, string password, string natid, string passwordb, string firstname, string lastname, int role, string address, string city, string country)
        {
            var user = db.AspNetUsers.Where(i => i.Email == email).FirstOrDefault();
            //var user = db.AspNetUsers.Where(i => i.Id == email).FirstOrDefault();
            var client = new HttpClient();


            if (user == null)
            {
                // checking password
                if (password == null)
                {
                    TempData["flash"] = "1";
                    TempData["error"] = "Please fill in password";
                    return View();

                }

                var nuser = new IdentityUser { Email = email, UserName = email, PasswordHash = password };

                var userid = nuser.Id;
                IdentityResult result = await userManager.CreateAsync(nuser, nuser.PasswordHash);
                if (result.Succeeded)
                {


                    // Change Role in created ID


                    //"UPDATE Inventory SET Inventorynumber='"+ num +"',Inventory_Name='"+name+"', Quantity ='"+ quant+"',Location ='"+ location+"' Category ='"+ category+"' WHERE Inventorynumber ='"+ numquery +"';";
                    // string query = "UPDATE Inventory SET Inventorynumber=@Inventorynumber,Inventory_Name=@Inventory_Name, Quantity =@Quantity ,Location =@Location,Category =@Category WHERE Inventorynumber =@Inventorynumber";
                    try
                    {
                        //This is my connection string i have assigned the database file address path  
                        string MyConnection2 = "Server=DESKTOP-Q6TP3P1\\SQLEXPRESS;Database=stanchart_simba_biller_console;User Id=sa;Password=Password123;";
                        //This is my insert query in which i am taking input from the user through windows forms  
                        //string Query = "insert into student.studentinfo(idStudentInfo,Name,Father_Name,Age,Semester) values('" + this.IdTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FnameTextBox.Text + "','" + this.AgeTextBox.Text + "','" + this.SemesterTextBox.Text + "');";
                        string Query = "UPDATE AspNetUsers SET Role ='"+ role +"' WHERE Id = '" + userid +"'";
                                                //This is  MySqlConnection here i have created the object and pass my connection string.  
                        SqlConnection MyConn2 = new SqlConnection(MyConnection2);
                        //This is command class which will handle the query and connection object.  
                        SqlCommand MyCommand2 = new SqlCommand(Query, MyConn2);
                      // MySqlDataReader MyReader2;

                        MyConn2.Open();

                        using (SqlDataReader reader = MyCommand2.ExecuteReader())
                        {
                            ViewBag.error = "Save data";
                            while (reader.Read())
                            {
                                // listBox2.Items.Add(reader.GetString(0) + " " + reader.GetString(1) + "  (" + reader.GetInt16(2) + ")");
                            }
                        }
                        MyConn2.Close();


                        string roleName;
                        if (role == 3)
                        {
                            try 
                            { 
                                    roleName = "Client";
                                    var detail = new AspNetUsersDetails { UserId = userid, email = email, natid = natid, firstname = firstname, lastname = lastname, address = address, city = city, country = country, role = role, roleName = roleName };

                                    // IdentityResult result = await userManager.CreateAsync(detail);
                                    db.AspNetUsersDetails.Add(detail);
                                    var resultb = db.SaveChanges();

                            } 
                            catch
                            {
                                return View();
                            }
                        }

                        return (RedirectToAction("CompleteRegistration", new { email = email, firstname }));


                    }
                    catch (Exception ex)
                    {
                        //  MessageBox.Show(ex.Message);
                        ViewBag.error = ex.Message;
                    }



                    return (RedirectToAction("CompleteRegistration", new { email = email ,firstname }));
                   // ViewBag.echo = "User has already been registered";
                }
                else
                {
                    TempData["flash"] = "1";
                    TempData["error"] = "Failed to create User";
                    return View();
                    ViewBag.echo = "User has already been registered";
                }

            }
           
                
            ViewBag.title = "Login";
            TempData["flash"] = "1";
            TempData["error"] = "User already registered";
            // return View();

            return View();
        }

        [AllowAnonymous]
        [HttpGet("CompleteRegistration")]
        public IActionResult CompleteRegistration(string email,string firstname)

        {
            SmtpClient client = new SmtpClient("mail.ttcsglobal.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("companiesonlinezw", "N3wPr0ducts@1");
            // client.Credentials = new NetworkCredential("username", "password");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("companiesonlinezw@ttcsglobal.com");
            mailMessage.To.Add(email);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = ("<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Email</title>" +
                "</head>" +

                "<body style=\"font-family:'Century Gothic'\">" +
                "<p><b>Dear " + firstname + "</b></p>" +
                    "<p>Congratulations, for being successfully registered on DCIP services portal.</p>" +
                "<p>Kindly use the link below to access your account.Enjoy our services.</p>" +

                "<a>https://deedsapp.ttcsglobal.com:6868/Auth/Login </a>" +

                "<p>Regards</p>" +

                "<p>DCIP</p>" +

                "</body>" +
                "</html>");//GetFormattedMessageHTML();
            mailMessage.Subject = "Successfull user";
            client.Send(mailMessage);


            string ddd;
            ddd = email;

            var user = db.AspNetUsers.Where(i => i.Email == email).FirstOrDefault();
            TempData["flash"] = "2";
            TempData["error"] = "User has been successfully registered, please check email";

            ViewBag.title = "Login";

            return View();
        }


        private String GetFormattedMessageHTML()
        {
            return "";
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {

            TempData["flash"] = "2";
            TempData["error"] = "Welcome";
            //seed the temp account

            ViewBag.title = "Login";
            seed();
            return View();
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password, string billercode, string returnUrl)
        {
            // checking password
            if (password == null)
            {
                TempData["flash"] = "1";
                TempData["error"] = "Please fill in password";
                return View();


            }

            ViewBag.title = "Login";
            //sign in
            var iduser = new IdentityUser { Email = email, PasswordHash = password, UserName = email };
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            //if signedin check the correct biller code is etered
            if (result.Succeeded)
            {
                /*var user = db.AspNetUsers.Where(i => i.Email == email && i.BillerCode == billercode).FirstOrDefault();
                if (user == null)
                {
                    //log out if billercode is different
                    await signInManager.SignOutAsync();
                    return RedirectToAction("Login");
                }
                else*/
                {
                    //go to the return url
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        TempData["flash"] = "2";
                        TempData["error"] = "Successful";
                        return RedirectPermanent(returnUrl);
                    }
                    else
                    {
                        TempData["flash"] = "2";
                        TempData["error"] = "Successful";
                        //go to admin dashboard
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
            }
            else
            {
                TempData["flash"] = "1";
                TempData["error"] = "Invalid Login Credentials";
                return View();

            }
            ViewBag.email = email;
            ViewBag.billercode = billercode;
            TempData["error"] = "Invalid Login Credentials";
            TempData["flash"] = "error";
            return View();
        }


        [HttpGet("Logout")]
        public IActionResult Logout()
        {


            signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
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
