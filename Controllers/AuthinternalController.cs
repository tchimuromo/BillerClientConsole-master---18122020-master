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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{
    [Authorize]
    [Route("Authinternal")]
    public class AuthinternalController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        private dbContext db = new dbContext();

        private readonly IHttpContextAccessor _http;


        public AuthinternalController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor http)
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
        [HttpGet("Role")]
        public IActionResult Login()

        { 

            // "UPDATE Inventory SET Inventorynumber='"+ num +"',Inventory_Name='"+name+"', Quantity ='"+ quant+"',Location ='"+ location+"' Category ='"+ category+"' WHERE Inventorynumber ='"+ numquery +"';";
        //string query = "UPDATE Inventory SET Inventorynumber=@Inventorynumber,Inventory_Name=@Inventory_Name, Quantity =@Quantity ,Location =@Location,Category =@Category WHERE Inventorynumber =@Inventorynumber";
            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = "Server=DESKTOP-Q6TP3P1\\SQLEXPRESS;Database=stanchart_simba_biller_console;User Id=sa;Password=password123;";
        //This is my insert query in which i am taking input from the user through windows forms  
        //string Query = "insert into student.studentinfo(idStudentInfo,Name,Father_Name,Age,Semester) values('" + this.IdTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FnameTextBox.Text + "','" + this.AgeTextBox.Text + "','" + this.SemesterTextBox.Text + "');";
                string Query = "UPDATE AspNetUsers SET Role= 3 WHERE Id = @userid";

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


            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                ViewBag.error = ex.Message;
            }

            ViewBag.title = "Login";
           
            return View();
        }


        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register()
        {


            ViewBag.title = "Register";
            return View();
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string password, string natid, string passwordb, string firstname, string lastname, int role, string address, string city, string country)
        {
            var user = db.AspNetUsers.Where(i => i.Email == email).FirstOrDefault();
            //var user = db.AspNetUsers.Where(i => i.Id == email).FirstOrDefault();
            var client = new HttpClient();

            if (user == null)
            {
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
                       // string MyConnection2 = "Server=O-Chimuka\\COMPANIES;Database=stanchart_simba_biller_console;User Id=sa;Password=password123;";
                        string MyConnection2 = "Server=DEEDSAPP\\SQLEXPRESS2;;Database=stanchart_simba_biller_console;User Id=sa;Password=Password123;";
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
                        if (role == 3)
                        {
                            try
                            {
                                string roleName;
                                roleName = "Client";
                                var detail = new AspNetUsersDetails { UserId = userid, email = email, natid = natid, firstname = firstname, lastname = lastname, address = address, city = city, country = country, role = role, roleName = roleName };

                                // IdentityResult result = await userManager.CreateAsync(detail);
                                db.AspNetUsersDetails.Add(detail);
                                var resultb = db.SaveChanges();

                            }
                            catch
                            {
                                ViewBag.title = "Login";
                                return View();
                            }

                        }
                        else if (role == 2)
                        {
                            try
                            {
                                string roleName;
                                roleName = "Examiner";
                                var detail = new AspNetUsersInternal {  email = email, natid = natid, firstname = firstname, lastname = lastname, address = address, city = city, country = country, role = role, roleName = roleName };

                                // IdentityResult result = await userManager.CreateAsync(detail);
                                db.AspNetUsersInternal.Add(detail);
                                var resultb = db.SaveChanges();

                            }
                            catch(Exception e)
                            {
                               var ex = e;
                                ViewBag.title = "Login";
                                return View();
                            }

                        }

                        else if (role == 1)
                        {
                            try
                            {
                                string roleName;
                                roleName = "Principal";
                                var detail = new AspNetUsersInternal {  email = email, natid = natid, firstname = firstname, lastname = lastname, address = address, city = city, country = country, role = role, roleName = roleName };

                                // IdentityResult result = await userManager.CreateAsync(detail);
                                db.AspNetUsersInternal.Add(detail);
                                var resultb = db.SaveChanges();

                            }
                            catch
                            {
                                ViewBag.title = "Login";
                                return View();
                        
                            }
                        }
                        else
                        {
                            ViewBag.title = "Login";
                            return View();

                        }

                    }
                    catch (Exception ex)
                    {
                        //  MessageBox.Show(ex.Message);
                        ViewBag.error = ex.Message;
                    }



                    return (RedirectToAction("CompleteRegistration", new { email = email }));
                   // ViewBag.echo = "User has already been registered";
                }
                else
                {
                    ViewBag.echo = "User has already been registered";
                }

            }
            ViewBag.title = "Login";

            return View();
        }

       
        [HttpGet("CompleteRegistration")]
        public IActionResult CompleteRegistration(string email, string firstname)

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
                "<p><b>Hi " + firstname + "/Dear valued Customer</b></p>" +
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

            //var user = db.AspNetUsers.Where(i => i.Email == email).FirstOrDefault();

            ViewBag.title = "Login";

            return View();
        }




        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Logink()
        {
            //seed the temp account

            ViewBag.title = "Login";
            seed();
            return View();
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password, string billercode, string returnUrl)
        {
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
                        return RedirectPermanent(returnUrl);
                    }
                    else
                    {
                        //go to admin dashboard
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
            }
            ViewBag.email = email;
            ViewBag.billercode = billercode;
            TempData["tmsg"] = "Invalid Login Credentials";
            TempData["type"] = "error";
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



////Send SMS COde

//string username = "obiematan";

//// Webservices token for above Webservice username
//string token = "72cceb3c5553eb552d0314b227395c61";

//// BulkSMS Webservices URL
//string bulksms_ws = "http://portal.bulksmsweb.com/index.php?app=ws";

//// destination numbers, comma seperated or use #groupcode for sending to group
//// $destinations = '#devteam,263071077072,26370229338';
//// $destinations = '26300123123123,26300456456456';  for multiple recipients

//string destinations = "263772887738,263779390351,263773030827";

//// SMS Message to send
//string message = "Testing C# to BulkSMS Webservice";

//// send via BulkSMS HTTP API

//string ws_str = bulksms_ws + "&u=" + username + "&h=" + token + "&op=pv";
//ws_str += "&to=" + Uri.EscapeDataString(destinations) + "&msg=" + Uri.EscapeDataString(message);

//                    HttpResponseMessage response = await client.GetAsync(ws_str);

//response.EnsureSuccessStatusCode();

//                    using (HttpContent content = response.Content)
//                    {
//                        string responseBody = await response.Content.ReadAsStringAsync();
//Console.WriteLine(responseBody + "........");
//                    }