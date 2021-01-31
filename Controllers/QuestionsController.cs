using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillerClientConsole.Models;
using Microsoft.AspNetCore.Mvc;
using BillerClientConsole.Globals;
using Newtonsoft.Json;
using Webdev.Payments;
using Syncfusion.EJ2.Base;
using System.Collections;
using BillerClientConsole.Dtos;
using PdfSharpCore.Fonts;
using BillerClientConsole.Data;
using System.Net.Mail;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillerClientConsole.Controllers
{                           
    [Route("Company")]
    public class QuestionsController : Controller
    {
        public QuestionsController(QueryDbContext queryDb)
        {
            this.queryDb = queryDb;
        }
        private mCompanyInfo companyInfo = new mCompanyInfo();
        private dbContext db = new dbContext();
        public static List<mmainClause> objects = new List<mmainClause>();
        public static List<MemberDto> member = new List<MemberDto>();
        public static List<MemberDto> entityMember = new List<MemberDto>();
        private readonly QueryDbContext queryDb;

        //public static mCompanyInfo pvt = new mCompanyInfo();
        //public static mMemorandum memo = new mMemorandum();
        //public static List<mmainClause> Objectives = new List<mmainClause>();
        //public static AuthorisedCapitalIn capitals { get; set; }
        //List<Member> HumanMembers = new List<Member>();
        //List<CompanyMember> entityMembers = new List<CompanyMember>();
        //List<Director> AllCompanyDirectors = new List<Director>();

        [HttpGet("CompanyApplication")]
        public async Task<IActionResult> CompanyApplication(mCompanyInfo companyinfo)
        {
            member = new List<MemberDto>();
            entityMember = new List<MemberDto>();
            objects = new List<mmainClause>();
            ViewBag.title = "New Company Registration";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var resp = await client.GetAsync($"{Globals.Globals.service_end_point}/Names/{ user.Email}/GetUnusedNames").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(resp);
           
            var data = json_data.data.value;
            List<mSearch> nameSear = JsonConvert.DeserializeObject<List<mSearch>>(data.ToString());
            List<mSearchInfo> nameSearchSummary = new List<mSearchInfo>();

            List<mSearch> nameSearches = nameSear.Where(z => z.searchInfo.Satus == "Approved").OrderByDescending(z => z.searchInfo.SearchDate).ToList();

            foreach (mSearch search in nameSearches)
            {
                var adata = search.searchInfo.ApprovedDate.ToString();
               DateTime regDate = Convert.ToDateTime(adata);
               DateTime expDate = regDate.AddDays(30);
                int daysLeft = (expDate - regDate).Days;

                //search.searchInfo.ExpiryDate = expDate.ToString();
                //search.searchInfo.DaysLeft = daysLeft.ToString();
            }
            //foreach (mSearch search in nameSearches)
            //{
            //    nameSearchSummary.Add(search.searchInfo);
            //}

            //nameSearchSummary = nameSearchSummary.Where(z => z.Satus == "Reserved").ToList();
            //int counFPostt = nameSearchSummary.Count;
            string[] sex = { "Male","Female" };
            ViewBag.nameSearches = nameSearches;
            ViewBag.sex = sex;
            ViewBag.Count = nameSearches.Count;

            return View();
        }

        public IActionResult Queries()
        {
            return View();
        }

        [HttpGet("ApprovedNameSearches")]
        public async Task<IActionResult> NameSearches()
        {

            ViewBag.title = "New Company Registration";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var client = new HttpClient();
            var resp = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_user_v1}?UserID={user.Email}").Result.Content.ReadAsStringAsync();
            dynamic json_data = JsonConvert.DeserializeObject(resp);

            var data = json_data.data.value;
            List<mSearch> nameSearches = JsonConvert.DeserializeObject<List<mSearch>>(data.ToString());
            List<mSearchInfo> nameSearchSummary = new List<mSearchInfo>();

            nameSearches = nameSearches.Where(z => z.searchInfo.Satus == "Approved").ToList();

            foreach (mSearch search in nameSearches)
            {
                DateTime regDate = Convert.ToDateTime(search.searchInfo.ApprovedDate);
                DateTime expDate = regDate.AddDays(30);
                int daysLeft = (expDate - regDate).Days;

                //search.searchInfo.ExpiryDate = expDate.ToString();
                //search.searchInfo.DaysLeft = daysLeft.ToString();
            }
            //foreach (mSearch search in nameSearches)
            //{
            //    nameSearchSummary.Add(search.searchInfo);
            //}

            //nameSearchSummary = nameSearchSummary.Where(z => z.Satus == "Reserved").ToList();
            //int count = nameSearchSummary.Count;
            ViewBag.nameSearches = nameSearches;
            ViewBag.Count = nameSearches.Count;

            return View();
        }


        [HttpGet("AddNewCompany")]
        public IActionResult AddNewCompany(string searchId)
        {
            var db = new dbContext();
            member = new List<MemberDto>();
            entityMember = new List<MemberDto>();
            objects = new List<mmainClause>();
            ViewBag.title = "New Company Application";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            //ViewBag.billercode = user.BillerCode;

            var client = new HttpClient();

            var res = client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_search_id}?ID={searchId}").Result.Content.ReadAsStringAsync().Result;
            dynamic data_j = JsonConvert.DeserializeObject(res);
            var searchNames = data_j.data.value[0].searchInfo;
            var nameSearch = data_j.data.value[0];

            mSearch search = JsonConvert.DeserializeObject<mSearch>(nameSearch.ToString());

            mSearchInfo searchInfo = JsonConvert.DeserializeObject<mSearchInfo>(searchNames.ToString());
            ViewBag.SearchFor = searchInfo.Search_For;
            ViewBag.Objective = searchInfo.Purpose;

            var searchedName = data_j.data.value[0].searchNames;
            List<mSearchNames> names = JsonConvert.DeserializeObject<List<mSearchNames>>(searchedName.ToString());

            var name = names.Where(t => t.Status == "Reserved").FirstOrDefault();
            ViewBag.Name = name.Name;


            ViewBag.SearchRef = searchInfo.SearchRef;
            //pvt.Search_Ref = searchInfo.SearchRef;
            //pvt.Search_Name_ID = name.Name_ID;
            //pvt.Name = name.Name;
            // get reserved name details

            //pvt.authorisedCapital.authorisedCapital = 100;
            ViewBag.billercode = user.BillerCode;
            return View();
        }

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("AddNewCompany")]
        public async Task<IActionResult> AddNewCompany(mProductOffline product)
        {
            ViewBag.title = "New Company Application";
            //var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
            member = new List<MemberDto>();
            entityMember = new List<MemberDto>();
            objects = new List<mmainClause>();
            return RedirectToAction("CompanyInformation");
        }

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        //[HttpPost("AddCompanyInformation")]
        //public async Task<IActionResult> AddCompanyInformation(CoInfoFieldsIn companyInfo)
        //{
        //    ViewBag.title = "New Company Application";

        //    pvt.IndustrySector = companyInfo.industrySector;
        //    pvt.Registered_Address = companyInfo.physicalAddress;
        //    pvt.City = companyInfo.city;
        //    pvt.Telephone = companyInfo.telephone;
        //    pvt.TelephoneExtension = companyInfo.telephoneExt;
        //    pvt.Mobile = companyInfo.mobileNumber;
        //    pvt.Email = companyInfo.emailAddress;
        //    //var client = new HttpClient();
        //    //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
        //    return RedirectToAction("CompanyMemo");
        //}

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("AddCompanyMemo")]
        public async Task<IActionResult> AddCompanyMemo(mProductOffline product)
        {
            ViewBag.title = "New Company Application";
            //var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
            return RedirectToAction("CompanyMembers");
        }

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("CompanyMembers")]
        public async Task<IActionResult> CompanyMembers(mProductOffline product)
        {
            ViewBag.title = "New Company Application";
            //var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
            return View();
        }

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        //[HttpPost("AddCompanyArticles")]
        //public async Task<IActionResult> CompanyArticles(MemoFieldsIn1 values)
        //{
        //    ViewBag.title = "New Company Application";


        //    string[] otherObjectives = values.otherObjectives.Split(',');

        //    mmainClause clause = new mmainClause();
        //    clause.objective = values.mainObject;
        //    clause.objType = "main";

        //    Objectives.Add(clause);

        //    foreach(string otherObjective in otherObjectives)
        //    {
        //        mmainClause otherClause = new mmainClause();
        //        otherClause.objective = otherObjective;
        //        otherClause.objType = "other";

        //        Objectives.Add(otherClause);
        //    }
        //    //var client = new HttpClient();
        //    //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
        //    return View();
        //}

        /// <summary>
        /// allows the user to create a new product and upload it to the server
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("AddCompanyMembers")]
        public async Task<IActionResult> AddCompanyMemebers(string members, string companyMembers)
        {
            ViewBag.title = "New Company Application";

            members.TrimEnd(',');
            string[] singleMembers = members.Split('|');
            List<string[]> memberDetails = new List<string[]>();

            foreach (string mEmber in singleMembers)
            {
                string md = mEmber.TrimEnd(',');
                md = md.TrimStart(',');
                if (mEmber == "")
                    continue;
                memberDetails.Add(md.Split(','));
            }

            string[] singleCompanyMember = companyMembers.Split('|');
            List<string[]> companyMemberDetails = new List<string[]>();

            foreach (string CmEmber in singleCompanyMember)
            {
                string cd = CmEmber.TrimEnd(','); ;
                cd = cd.TrimStart(',');
                if (CmEmber == "")
                    continue;
                companyMemberDetails.Add(cd.Split(','));
            }

            //foreach(string[] memberDit in memberDetails)
            //{
            //    Member member = new Member();

            //    member.nationalId = memberDit[0];
            //    member.nationality = memberDit[1];
            //    member.fullName = memberDit[2];
            //    member.gender = memberDit[3];
            //    member.physicalAddress = memberDit[4];
            //    member.ordinaryShares = memberDit[5];
            //    member.preferenceShres = memberDit[6];
            //    member.totalShares = memberDit[7];

            //    HumanMembers.Add(member);
            //}

            //foreach(string[] coMemberDit in companyMemberDetails)
            //{
            //    CompanyMember companyMember = new CompanyMember();

            //    companyMember.name = coMemberDit[0];
            //    companyMember.companyNumber = coMemberDit[1];
            //    companyMember.countryOfOrigin = coMemberDit[2];
            //    companyMember.ordinaryShares = coMemberDit[3];
            //    companyMember.preferenceShares = coMemberDit[4];
            //    companyMember.totalShares = coMemberDit[5];

            //    entityMembers.Add(companyMember);
            //}


            //var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_postBillerProduct}", product).Result.Content.ReadAsStringAsync();
            return RedirectToAction("CompanyDirectors");
        }

        /// <summary>
        /// fetch the product and display it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ViewDetails/{id}")]
        public IActionResult ViewDetails(string id)
        {
            ViewBag.title = "View Question Details";
            var client = new HttpClient();
            var response = client.GetAsync($"{Globals.Globals.end_point_fetchBillerProductById}?id={id}").Result.Content.ReadAsStringAsync().Result;
            ViewBag.product = JsonConvert.DeserializeObject(response);
            return View();
        }


        [HttpGet("ListBillerProducts")]
        [HttpGet("")]
        public IActionResult ListBillerProducts(int pageNumber, string date_from, string date_to)
        {
            ViewBag.title = "Products";
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/products/CompanyInformation";
            //TempData["pageNumber"] = 1;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            return View();
        }

        //CompanyMembers
        //[HttpGet("CompanyMembers")]
        //[HttpGet("")]
        //public IActionResult CompanyMembers(AuthorisedCapitalIn authCap)
        //{
        //    ViewBag.title = "Products";

        //    capitals = authCap;
        //    //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
        //    ////ajax reports is in partial controllers
        //    // TempData["loadUrl"] = $"/ajax/products/CompanyInformation";
        //    //TempData["pageNumber"] = 1;
        //    //TempData["billercode"] = "Telone";
        //    //TempData["date_to"] = "20/4/2020";
        //    //TempData["date_from"] = "20/4/2020";
        //    return View();
        //}

        [HttpGet("CompanyInformation")]
        [HttpGet("")]
        public IActionResult CompanyInformation()
        {
            ViewBag.title = "Products";
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/products/CompanyInformation";
            //TempData["pageNumber"] = 1;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            return View();
            //ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/question/CompanyInformation";

            // return View();
        }



        [HttpGet("CompanyDirectors")]
        [HttpGet("")]
        public IActionResult CompanyDirectors()
        {
            ViewBag.title = "New Company Application";
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/products/CompanyDirectors";
            //TempData["pageNumber"] = 1;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            return View();
            //ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/question/CompanyInformation";

            // return View();
        }

        [HttpGet("PreviewApplication")]
        [HttpGet("")]
        public async Task<IActionResult> Preview(string directorTrans)
        {
            ViewBag.title = "New Company Application";

            string[] directorDit = directorTrans.Split('|');
            List<string[]> directorSingular = new List<string[]>();
            foreach (string director in directorDit)
            {
                string cd = director.TrimEnd(','); ;
                cd = cd.TrimStart(',');
                if (director == "")
                    continue;
                directorSingular.Add(cd.Split(','));
            }

            //foreach (string[] director in directorSingular)
            //{
            //    Director companyDirector = new Director();

            // companyDirector.nationalID = director[0];
            //    companyDirector.nationality = director[1];
            //    companyDirector.name = director[2];
            //    companyDirector.gender = director[3];
            //    companyDirector.residentialAddress = director[4];

            //    AllCompanyDirectors.Add(companyDirector);
            //}

            //Co Company = new Co();

            //Company.pvt = pvt;
            //Company.memo = memo;
            //Company.Objectives = Objectives;
            //Company.capitals = capitals;
            //Company.HumanMembers = HumanMembers;
            //Company.entityMembers = entityMembers;
            //Company.AllCompanyDirectors = AllCompanyDirectors;


            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<Co>($"{Globals.Globals.end_point_post_company_application}", Company).Result.Content.ReadAsStringAsync();
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/products/CompanyDirectors";
            //TempData["pageNumber"] = 1;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            //ViewBag.Company = Company;
            return View();
            //ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/question/CompanyInformation";

            // return View();
        }

        [HttpGet("CompanyMemo")]
        [HttpGet("")]
        public IActionResult CompanyMemo()
        {
            ViewBag.title = "New Company Registration";
            //var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).First();
            ////ajax reports is in partial controllers
            TempData["loadUrl"] = $"/ajax/products/CompanyMemo";
            //TempData["pageNumber"] = 1;
            //TempData["billercode"] = "Telone";
            //TempData["date_to"] = "20/4/2020";
            //TempData["date_from"] = "20/4/2020";
            return View();
            //ajax reports is in partial controllers
            // TempData["loadUrl"] = $"/ajax/question/CompanyInformation";

            // return View();
        }

        /// <summary>
        /// update the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(mProductOffline product)
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync<mProductOffline>($"{Globals.Globals.end_point_updateBillerProduct}", product);
            return RedirectToAction("ListBillerQuestions");
        }


        [HttpGet("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)


        {

            var client = new HttpClient();
            var resp = await client.GetAsync($"{Globals.Globals.end_point_DeleteProductBillerProductById}?id={id}");
            return RedirectToAction("ListBillerQuestions");
        }

        //[HttpGet("CompanyAuthorisedShareCapital")]
        //public async Task<IActionResult> AuthorisedShareCapital(MemoFieldsIn2 memofilels)
        //{
        //    ViewBag.title = "New Company Registration";
        //    memo.liabilityClause = memofilels.LiabilityClause;
        //    memo.tableB = memofilels.tableB == "Yes"? true : false;
        //    memo.tableC = memofilels.tableC == "Yes" ? true : false; 
        //    memo.ammendedArticles = memofilels.ammendedArticles;
        //    return View();
        //}


        //[HttpPost("ApplyNamesearch")]
        //public async Task<JsonResult> ApplyNamesearch(string search)
        //{
        //    return Json(new
        //    {
        //        res = "ok",
        //        message = "in reciept"
        //    });
        //}

        [HttpGet("SubmitNameSearch")]
        public async Task<JsonResult> SubmitNameSearchAsync(mCompanyInfo companyInformation)
        {
            var client = new HttpClient();
            string searchReferenzi = "";

            switch (companyInformation.step)
            {
                case 1:
                    var response = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_id}?ID={companyInformation.Search_Ref}").Result.Content.ReadAsStringAsync();
                    var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={companyInformation.Reference}").Result.Content.ReadAsStringAsync();

                    mCompanyResponse stepInfo = new mCompanyResponse();

                    try
                    {
                        dynamic json_step_data = JsonConvert.DeserializeObject(rhisponzi);
                        var datadata = json_step_data.data.value;
                        stepInfo = JsonConvert.DeserializeObject<mCompanyResponse>(datadata.ToString());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    

                    dynamic json_data = JsonConvert.DeserializeObject(response);
                    var data = json_data.data.value[0];
                    mSearch nameSearch = JsonConvert.DeserializeObject<mSearch>(data.ToString());

                    Globals.Globals.toBePaid = nameSearch.searchInfo.SearchRef;

                    var search = JsonConvert.SerializeObject(nameSearch);
                    Globals.Globals.companyInfo.Search_Ref = nameSearch.searchInfo.search_ID;
                    foreach (mSearchNames name in nameSearch.SearchNames)
                    {
                        if (name.Status == "Reserved")
                        {
                            Globals.Globals.companyInfo.Search_Name_ID = name.Name_ID;
                            Globals.Globals.companyInfo.Name = name.Name;
                            break;
                        }
                    }
                    Globals.Globals.companyInfo.Type = nameSearch.searchInfo.Search_For;
                    Globals.Globals.companyInfo.Status = "Saved";
                    Globals.Globals.companyInfo.Search_Ref = nameSearch.searchInfo.SearchRef;
                    //companyInfo = companyInformation;
                    DateTime ikozvino = DateTime.Now;
                    Globals.Globals.companyInfo.Date_Of_Application = ikozvino.ToString();
                    Globals.Globals.companyInfo.Application_Ref = Guid.NewGuid().ToString();
                    Globals.Globals.companyInfo.Payment = "Paid";
                    var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                    Globals.Globals.companyInfo.AppliedBy = user.Email;

                    mCompany NewCompany = new mCompany();
                    NewCompany.CompanyInfo = Globals.Globals.companyInfo;
                    var res = await client.PostAsJsonAsync<mCompany>($"{Globals.Globals.end_point_post_company_application}", NewCompany).Result.Content.ReadAsStringAsync();
                    return Json(new
                    {
                        refrence = companyInformation.Search_Ref,
                        reservedName = Globals.Globals.companyInfo.Name,
                        type = Globals.Globals.companyInfo.Type,
                        objective = nameSearch.searchInfo.Purpose,
                        stepInfor = stepInfo
                    });
                case 2:
                    Globals.Globals.companyInfo.Email = companyInformation.Email;
                    Globals.Globals.companyInfo.Registered_Address = companyInformation.Registered_Address;
                    Globals.Globals.companyInfo.City = companyInformation.City;
                    Globals.Globals.companyInfo.MobileNumber = companyInformation.MobileNumber;
                    Globals.Globals.companyInfo.PostalAddress = companyInformation.PostalAddress;
                    Globals.Globals.companyInfo.Telephone = companyInformation.Telephone;
                    //Globals.Globals.companyInfo.Telephone = companyInformation.Telephone;
                    //Globals.Globals.companyInfo.TelephoneExt = companyInformation.TelephoneExt;
                    //Globals.Globals.companyInfo.MobileNumber = companyInformation.MobileNumber;
                    mCompany NewCompanyy = new mCompany();
                    NewCompanyy.CompanyInfo = Globals.Globals.companyInfo;
                    NewCompanyy.CompanyInfo.step = companyInformation.step;
                    var resp = await client.PostAsJsonAsync<mCompany>($"{Globals.Globals.end_point_post_company_application}", NewCompanyy).Result.Content.ReadAsStringAsync();
                    var b = 0;
                    break;
                case 3:
                    //mMemorandum memorandum = new mMemorandum();
                    //memorandum.memostep = companyInformation.step;
                    //memorandum._id = Guid.NewGuid().ToString();
                    //memorandum.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
                    //memorandum.liabilityClause = companyInformation.liabilityClause;
                    //memorandum.sharesClause = companyInformation.shareClause;


                    //var all = companyInformation.Objectives.TrimEnd(',');
                    //string[] singularObjectives = all.Split('|');

                    //List<mmainClause> objects = new List<mmainClause>();
                    //var objectiveCount = 0;
                    //foreach (string objetive in singularObjectives)
                    //{
                    //    if (objetive != "")
                    //    {
                    //        string m = objetive.TrimEnd(',');
                    //        m = m.TrimStart(',');

                    //        string[] items = objetive.Split(',');
                    //        mmainClause clause = new mmainClause();

                    //        if (items[0] == "")
                    //        {
                    //            clause.obj_num = items[1];
                    //            clause.objective = items[2];
                    //        }
                    //        else
                    //        {
                    //            clause.obj_num = items[0];
                    //            clause.objective = items[1];
                    //        }


                    //        if (objectiveCount == 0)
                    //        {
                    //            clause.objType = "main";
                    //            objectiveCount++;
                    //        }
                    //        else
                    //        {
                    //            clause.objType = "other";
                    //        }
                    //        clause.memo_id = memorandum._id;
                    //        clause._id = Guid.NewGuid().ToString();
                    //        objects.Add(clause);
                    //    }
                    //}
                    //Globals.Globals.objects = objects;
                    //memorandum.objects = objects;
                    //PostMemo postMemo = new PostMemo();
                    //postMemo.memo = memorandum;
                    //postMemo.step = companyInformation.step;
                    //var respo = await client.PostAsJsonAsync<PostMemo>($"{Globals.Globals.end_point_post_company_application_memo}", postMemo).Result.Content.ReadAsStringAsync();
                    break;
                case 4:
                    mArticles articles = new mArticles();
                    articles._id = Guid.NewGuid().ToString();
                    articles.Application_Ref = Globals.Globals.companyInfo.Application_Ref;

                    if (companyInformation.Article.Contains("Table B"))
                        articles.articles_type = "Table B";
                    else
                        articles.articles_type = "Table C";

                    if (!string.IsNullOrEmpty(companyInformation.Ammended))
                        articles.articles = companyInformation.Ammended;

                    postArticles postArt = new postArticles();
                    postArt.Articles = articles;
                    postArt.step = companyInformation.step;

                    var respopo = await client.PostAsJsonAsync<postArticles>($"{Globals.Globals.service_end_point}/api/v1/{companyInformation.Application_Ref}/postCompanyApplicationArticles", postArt).Result.Content.ReadAsStringAsync();
                    var g = 0;
                    //Globals.Globals.companyInfo.TableB = companyInformation.TableB;
                    //Globals.Globals.companyInfo.TableC = companyInformation.Ta
                    //Globals.Globals.companyInfo.Ammended = companyInformation.Ammended;
                    break;
                case 5:
                    string[] singleMembers = companyInformation.Members.Split('|');

                    List<string[]> memberDetails = new List<string[]>();
                    List<mMembersInfo> members = new List<mMembersInfo>();
                    List<mMembersPotifolio> potfolio = new List<mMembersPotifolio>();

                    foreach (string mEmber in singleMembers)
                    {
                        string md = mEmber.TrimEnd(',');
                        md = md.TrimStart(',');
                        if (mEmber == "")
                            continue;
                        memberDetails.Add(md.Split(','));
                    }
                    var c = 0;

                    foreach (string[] membr in memberDetails)
                    {
                        mMembersInfo memb = new mMembersInfo();
                        mMembersPotifolio pot = new mMembersPotifolio();
                        int roleIndex = 8;

                        memb.member_id = Guid.NewGuid().ToString();
                        memb.Names = membr[3];
                        memb.Surname = membr[2];
                        memb.City = membr[5];
                        memb.memberType = "Person";
                        memb.ID_No = membr[0];
                        memb.Nationality = membr[1];


                        pot.member_id = memb.member_id;
                        pot.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
                        if (membr[roleIndex] == "Member")
                        {
                            pot.IsMember = 1;
                            roleIndex++;
                        }

                        if (membr[roleIndex] == "Director")
                        {
                            pot.IsDirector = 1;
                            roleIndex++;
                        }

                        if (membr[roleIndex] == "Secretary")
                        {
                            pot.IsCoSec = 1;
                            roleIndex++;
                        }

                        pot.number_of_shares = int.Parse(membr[roleIndex]) + int.Parse(membr[roleIndex + 1]);

                        members.Add(memb);
                        potfolio.Add(pot);
                    }

                    string[] companyMembers = companyInformation.MemberEntities.Split('|');

                    List<string[]> memberEntities = new List<string[]>();

                    foreach (string me in companyMembers)
                    {
                        if (me == "")
                            continue;
                        string racho = me.TrimStart(',');
                        racho = racho.TrimEnd(',');
                        memberEntities.Add(racho.Split(','));
                    }

                    foreach(string[] companyMember in memberEntities)
                    {
                        mMembersInfo memberEntity = new mMembersInfo();
                        memberEntity.member_id = Guid.NewGuid().ToString();
                        memberEntity.Names = companyMember[0];
                        memberEntity.Nationality = companyMember[2];
                        memberEntity.memberType = "Entity";

                        mMembersPotifolio potifolio = new mMembersPotifolio();
                        potifolio.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
                        potifolio.member_id = memberEntity.member_id;
                        potifolio.number_of_shares = int.Parse(companyMember[3]) + int.Parse(companyMember[4]);
                        potifolio.IsMember = 1;

                        members.Add(memberEntity);
                        potfolio.Add(potifolio);
                    }

                    PostMembers postMembers = new PostMembers();
                    postMembers._id = Globals.Globals.companyInfo.Application_Ref;
                    postMembers.step = companyInformation.step.ToString();
                    postMembers.members = members;
                    postMembers.membersPotifolio = potfolio;

                    var respopopo = await client.PostAsJsonAsync<PostMembers>($"{Globals.Globals.end_point_post_company_application_members}", postMembers).Result.Content.ReadAsStringAsync();


                    string paymentLink = SetupPayment(searchReferenzi);

                    return Json(new
                    {
                        link = paymentLink
                    });
                    //foreach (string[] member in memberDetails)
                    //{
                    //    mMembersPotifolio mmber = new mMembersPotifolio();
                    //mmber.name = member[2];
                    //mmber.nationality = member[1];

                    //    int ordinaryShares = int.Parse(member[5]);
                    //    int preferenceShares = int.Parse(member[6]);
                    //    int totalShares = ordinaryShares + preferenceShares;
                    //    mmber.number_of_shares = totalShares;
                    //    Globals.Globals.members.Add(mmber);
                    //}

                    //string[] singleCompanyMember = companyInformation.MemberEntities.Split('|');
                    //List<string[]> companyMemberDetails = new List<string[]>();

                    //foreach (string CmEmber in singleCompanyMember)
                    //{
                    //    string cd = CmEmber.TrimEnd(','); ;
                    //    cd = cd.TrimStart(',');
                    //    if (CmEmber == "")
                    //        continue;
                    //    companyMemberDetails.Add(cd.Split(','));
                    //}

                    //foreach (string[] member in companyMemberDetails)
                    //{
                    //    mMembersPotifolio mmber = new mMembersPotifolio();
                    //    //mmber.name = member[0];
                    //    //mmber.nationality = member[2];

                    //    int ordinaryShares = int.Parse(member[3]);
                    //    int preferenceShares = int.Parse(member[4]);
                    //    int totalShares = ordinaryShares + preferenceShares;
                    //    mmber.number_of_shares = totalShares;
                    //    Globals.Globals.members.Add(mmber);
                    //}

                    break;
                case 6:
                    //string[] directorDetails = companyInformation.Directors.Split('|');
                    //List<string[]> directorSingular = new List<string[]>();
                    //foreach (string director in directorDetails)
                    //{
                    //    string cd = director.TrimEnd(','); ;
                    //    cd = cd.TrimStart(',');
                    //    if (director == "")
                    //        continue;
                    //    directorSingular.Add(cd.Split(','));
                    //}
                    var we = 0;
                    

                    break;
            }

            return Json(new
            {
                res = "ok"
            });
        }

        [HttpPost("/PvtReg/{applicationid}/RegisterName/{nameId}")]
        public async Task<IActionResult> SubmitNameForPvtReg(string applicationid, string nameId)
        {
            var client = new HttpClient();
            string searchReferenzi = "";
            var response = await client.GetAsync($"{Globals.Globals.end_point_get_name_searches_by_id}?ID={nameId}").Result.Content.ReadAsStringAsync();
            var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={applicationid}").Result.Content.ReadAsStringAsync();

            mCompanyResponse stepInfo = new mCompanyResponse();

            try
            {
                dynamic json_step_data = JsonConvert.DeserializeObject(rhisponzi);
                var datadata = json_step_data.data.value;
                stepInfo = JsonConvert.DeserializeObject<mCompanyResponse>(datadata.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            dynamic json_data = JsonConvert.DeserializeObject(response);
            var data = json_data.data.value;
            mSearch nameSearch = JsonConvert.DeserializeObject<mSearch>(data.ToString());

            Globals.Globals.toBePaid = nameSearch.searchInfo.SearchRef;

            var search = JsonConvert.SerializeObject(nameSearch);
            Globals.Globals.companyInfo.Search_Ref = nameSearch.searchInfo.search_ID;
            foreach (mSearchNames name in nameSearch.SearchNames)
            {
                if (name.Status == "Reserved")
                {
                    Globals.Globals.companyInfo.Search_Name_ID = name.Name_ID;
                    Globals.Globals.companyInfo.Name = name.Name;
                    break;
                }
            }
            Globals.Globals.companyInfo.Type = nameSearch.searchInfo.Search_For;
            Globals.Globals.companyInfo.Status = "Saved";
            Globals.Globals.companyInfo.Search_Ref = nameSearch.searchInfo.SearchRef;
            //companyInfo = companyInformation;
            DateTime ikozvino = DateTime.Now;
            Globals.Globals.companyInfo.Date_Of_Application = ikozvino.ToString();
            Globals.Globals.companyInfo.Application_Ref = Guid.NewGuid().ToString();
            Globals.Globals.companyInfo.Payment = "Paid";
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            Globals.Globals.companyInfo.AppliedBy = user.Email;

            mCompany NewCompany = new mCompany();
            NewCompany.CompanyInfo = Globals.Globals.companyInfo;
            var res = await client.PostAsJsonAsync<mCompany>($"{Globals.Globals.end_point_post_company_application}", NewCompany).Result.Content.ReadAsStringAsync();
            return Ok(NewCompany.CompanyInfo.Search_Ref);
        }


        private string SetupPayment(string searchRef)
        {
            string uri = "";
            //var paynow = new Paynow("9848", "9cb58118-b8e3-45bc-b5b5-66a29ce71309");
            var paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");

            paynow.ResultUrl = "http://example.com/gateways/paynow/update";
            paynow.ReturnUrl = $"http://localhost:2015/Company/CompanyPayment?searchRef={Globals.Globals.toBePaid}";
            
            var payment = paynow.CreatePayment(Guid.NewGuid().ToString(), "brightonkofu@outlook.com");

            payment.Add("Name search", 1);
            
            var response = paynow.Send(payment);

           
            if (response.Success())
            {
                uri = response.RedirectLink();

                var pollUrl = response.PollUrl();

                var status = paynow.PollTransaction(pollUrl);

                ViewBag.Paid = "false";
                Globals.Globals.payment = paynow;
                Globals.Globals.response = response;
            }

            return uri;
        }

        [HttpGet("CompanyPayment")]
        public async Task<IActionResult> CompanyApplicationPaid(string searchRef)
        {
            var payment = Globals.Globals.payment;
            var responsey = Globals.Globals.response;
            ViewBag.title = "Payment";
            var status = payment.PollTransaction(responsey.PollUrl());
            if (status.Paid())
            {
                var client = new HttpClient();

                var rhisponzi = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={searchRef}").Result.Content.ReadAsStringAsync();

                mCompanyResponse stepInfo = new mCompanyResponse();
                dynamic json_step_data = JsonConvert.DeserializeObject(rhisponzi);
                var datadata = json_step_data.data.value;
                stepInfo = JsonConvert.DeserializeObject<mCompanyResponse>(datadata.ToString());

                var response = await client.PostAsJsonAsync<mCompanyResponse>($"{Globals.Globals.end_point_paid_company_application}", stepInfo).Result.Content.ReadAsStringAsync();

                ViewBag.Payment = status.Amount;
                ViewBag.RefNumber = status.Reference;
            }
            else
            {
                ViewBag.Payment = 0;

            }
            return View();
        }


        [HttpPost("SubmitOffice")]
        public JsonResult SubmitOffice(
            string physicalAddress

            )
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}",null);
            return Json(new
            {
                res = "ok"
            });
        }


        [HttpPost("SubmitObjectives")]
        public JsonResult SubmitObjectives(string objectives)
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}",null);
            return Json(new
            {
                res = "ok"
            });
        }


        [HttpPost("SubmitEntityMembers")]
        public JsonResult SubmitEntityMembers(string objectives)
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}", null);
            return Json(new
            {
                res = "ok"
            });
        }


        [HttpPost("Articles")]
        public JsonResult Articles(string objectives)
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}", null);
            return Json(new
            {
                res = "ok"
            });
        }


        [HttpPost("SubmitMembers")]
        public JsonResult SubmitMembers(string objectives)
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}", null);
            return Json(new
            {
                res = "ok"
            });
        }


        [HttpPost("SubmitDirectors")]
        public JsonResult SubmitDirectors(string objectives)
        {
            var client = new HttpClient();
            //var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.end_point_updateBillerProduct}", null);
            return Json(new
            {
                res = "ok"
            });
        }

        [HttpPost("UrlDatasourceArt")]

        public IActionResult UrlDatasourceArt([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<mmainClause>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }



        [HttpPost("UrlDatasource")]

        public IActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<mmainClause>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        //public ActionResult CellEditUpdate([FromBody] CRUDModel<Orders> value)
        //{
        //    var ord = value.Value;
        //    Orders val = order.Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
        //    val.OrderID = ord.OrderID;
        //    val.EmployeeID = ord.EmployeeID;
        //    val.CustomerID = ord.CustomerID;
        //    val.Freight = ord.Freight;
        //    val.OrderDate = ord.OrderDate;
        //    val.ShipCity = ord.ShipCity;
        //    return Json(value.Value);
        //}
        
        public List<mmainClause> GetObjects()
        {
            if(objects==null)
             objects = new List<mmainClause>();
            int Count = objects.Count();
            return objects;
        }

        ////insert the record
        [HttpPost("CellEditInsert")]

        public ActionResult CellEditInsert([FromBody] CRUDModel<mmainClause> value)
        {
            if(objects==null)
                objects = new List<mmainClause>();
            objects.Insert(0, value.Value);
            return Json(value);
        }

        //public ActionResult CellEditInsert([FromBody] CRUDModel<mmainClause> value)
        //{
        //    if (objects == null)
        //        objects = new List<mmainClause>();
        //    objects.Insert(0, value.Value);
        //    return Json(value);
        //}
        ////Delete the record
        //public ActionResult CellEditDelete([FromBody] CRUDModel<Orders> value)
        //{
        //    order.Remove(order.Where(or => or.OrderID == int.Parse(value.Key.ToString())).FirstOrDefault());
        //    return Json(value);
        //}

        [HttpPost("AddCompanyMemorandum")]
        public async Task<ActionResult> AddCompanyMemorandumAsync(int step, string liab, string shareClo)
        {
            var client = new HttpClient();

            //mMeMorandum memorandum = new mMeMorandum();
            //memorandum.memostep = step;
            //memorandum._id = Guid.NewGuid().ToString();
            //memorandum.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
            //liabilityClause liabilityClause = new liabilityClause();
            //liabilityClause.description = liab;
            //memorandum.LiabilityClause.Add(liabilityClause);
            //memorandum.liabilityClause = liab;
            //sharesClause sharesClause = new sharesClause();
            //sharesClause.description = shareClo;
            //memorandum.SharesClause.Add(sharesClause);
            //memorandum.sharesClause = shareClo;

            var objectiveType = 0;


            //memorandum.objects = objects;




            PostmMemorandum memo = new PostmMemorandum();
            memo.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
            memo._id = Guid.NewGuid().ToString();

            liabilityClause liabilityClause = new liabilityClause
            {
                _id = Guid.NewGuid().ToString(),
                memo_id = memo._id,
                description = liab
            };
            memo.LiabilityClause = liabilityClause;

            sharesClause shareClause = new sharesClause
            {
                _id = Guid.NewGuid().ToString(),
                memo_id = memo._id,
                description = shareClo
            };
            memo.SharesClause = shareClause;

            objects.Reverse();
            foreach (mmainClause objecty in objects)
            {
                objecty.memo_id = memo._id;
                objecty._id = Guid.NewGuid().ToString();
                objecty.Application_Ref = Globals.Globals.companyInfo.Application_Ref;

                if (objectiveType == 0)
                {
                    objecty.objType = "Main";
                    objectiveType++;
                }
                else
                {
                    objecty.objType = "Other";
                }
            }

            memo.objects = objects;

            PostMemo postMemo = new PostMemo();
            postMemo.step = step;
            postMemo.memo = memo;

            var respo = await client.PostAsJsonAsync<PostMemo>($"{Globals.Globals.end_point_post_company_application_memo}", postMemo).Result.Content.ReadAsStringAsync();
            return Json(objects);
        }

        
        [HttpPost("MemberCellEditInsert")]
        public ActionResult MemberCellEditInsert([FromBody] CRUDModel<MemberDto> value)
        {
            if (member == null)
                member = new List<MemberDto>();
            if(value != null)
                member.Insert(0, value.Value);
            return Json(value);
        }

        [HttpPost("EntityCellEditInsert")]
        public ActionResult EntityCellEditInsert([FromBody] CRUDModel<MemberDto> value)
        {
            if (entityMember == null)
                entityMember = new List<MemberDto>();
            if (value != null)
                entityMember.Insert(0, value.Value);
            return Json(value);
        }

        [HttpPost("MemberUrlDataSource")]
        public IActionResult MemberUrlDataSource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetMemberObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                //DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<MemberDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }


        [HttpPost("CompanyUrlDataSource")]
        public IActionResult CompanyUrlDataSource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = GetMemberEintityObjects();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<MemberDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private IEnumerable GetMemberObjects()
        {    
            

            return member;
        }

        private IEnumerable GetMemberEintityObjects()
        {
            return entityMember;
        }


        [HttpGet("RegisterOffice")]
        public async Task<IActionResult> SubmitTheOffice(RegisteredOfficeDto dto)
        {
            var client = new HttpClient();
            var office = new RegisteredOffice
            {
                PhysicalAddress = dto.PhysicalAddress,
                PostalAddress = dto.PostalAddress,
                City = dto.City,
                TelephoneNumber = dto.Telephone,
                MobileNumber = dto.MobileNumber,
                EmailAddress = dto.EmailAddress
                
            };
            var respopopo = await client.PostAsJsonAsync<RegisteredOffice>($"{Globals.Globals.service_end_point}/PvtRegistration/{dto.AppicationId}/RegisterOffice", office).Result.Content.ReadAsStringAsync();
            return Ok();
        }

        [HttpPost("AddCompanyMemberss")]
        public async Task<ActionResult> AddCompanyMemberss()
        {
            mMembersInfo memb;
            mMembersPotifolio folio;

            List<mMembersInfo> members = new List<mMembersInfo>();
            List<mMembersPotifolio> potifolios = new List<mMembersPotifolio>();

            foreach (MemberDto memberrr in member)
            {
                memb = new mMembersInfo();

                memb.member_id = memberrr.NationalId;
                memb.Names = memberrr.FirstName;
                memb.Surname = memberrr.Surname;
                memb.City = memberrr.PhyicalAddress;
                memb.memberType = "Person";
                memb.ID_No = memberrr.NationalId;
                memb.Nationality = memberrr.Nationality;

                folio = new mMembersPotifolio();
                folio.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
                folio.member_id = memb.member_id;

                //string[] roles = memberrr.Role.Split(',');
                //foreach(string role in roles)
                //{
                    if (memberrr.IsAMember)
                        folio.IsMember = 1;
                    if (memberrr.IsADirector)
                        folio.IsDirector = 1;
                    if (memberrr.IsCompanySecretary)
                        folio.IsCoSec = 1;
                //}

                try
                {
                    folio.OrdinaryShares = int.Parse(memberrr.OrdinaryShares);
                    folio.PreferenceShares = int.Parse(memberrr.PreferenceShares);
                    folio.number_of_shares = int.Parse(memberrr.OrdinaryShares) + int.Parse(memberrr.PreferenceShares);
                }catch(Exception ex)
                {
                    return BadRequest();
                }

                members.Add(memb);
                potifolios.Add(folio);
            }


            foreach(MemberDto entity in entityMember)
            {
                memb = new mMembersInfo();

                memb.member_id = entity.EntityNumber;
                memb.ID_No = entity.EntityNumber;
                memb.Names = entity.FirstName;
                memb.Nationality = entity.Nationality;
                memb.memberType = "Entity";

                folio = new mMembersPotifolio();
                folio.Application_Ref = Globals.Globals.companyInfo.Application_Ref;
                folio.member_id = memb.member_id;
                folio.company_member_id = memb.member_id;
                folio.IsMember = 1;

                folio.OrdinaryShares = int.Parse(entity.OrdinaryShares);
                folio.PreferenceShares = int.Parse(entity.PreferenceShares);
                folio.number_of_shares = int.Parse(entity.OrdinaryShares) + int.Parse(entity.PreferenceShares);

                members.Add(memb);
                potifolios.Add(folio);
            }

            PostMembers postMembers = new PostMembers();
            postMembers._id = Globals.Globals.companyInfo.Application_Ref;
            int stepi = 5;
            postMembers.step = stepi.ToString();
            postMembers.members = members;
            postMembers.membersPotifolio = potifolios;

            var client = new HttpClient();
            var respopopo = await client.PostAsJsonAsync<PostMembers>($"{Globals.Globals.end_point_post_company_application_members}", postMembers).Result.Content.ReadAsStringAsync();

            string paymentLink = SetupPayment("");

            return Json(new
            {
                link = paymentLink
            });
        }


        [HttpGet("PayForCompany")]
        public async Task<ActionResult> PayForCompany(int step)
        {
            ViewBag.title = "Application payment";
            if (step == 0)
            {
                string paymentLink = SetupPaymentForApplication("");
                ViewBag.link = paymentLink;
                ViewBag.paid = 0;
                return View();
            }
            ViewBag.paid = 1;
            return View();
        }


        [HttpPost("/{applicationId}/Approve")]
        public async Task<IActionResult> ApprovePvtEntityApplication(string applicationId)
        {
            //var db = new db();
            
            var client = new HttpClient();
            var user = db.AspNetUsers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var companyApplication = await client.GetAsync($"{Globals.Globals.end_point_get_company_application_by_application_ref}?ApplicationRef="+applicationId).Result.Content.ReadAsStringAsync();
            var res1 = JsonConvert.DeserializeObject(companyApplication);
            ///var CompInfo = res1.data.value[0].companyInfo;

           //// var DeCompInfo = JsonConvert.DeserializeObject<mCompanyInfo>(CompInfo.ToString());
           // var email2 = DeCompInfo.AppliedBy;

            
            //Code to See if there are pending Queries on the Application 
            var ApplicationQueries = queryDb.Queries.Where(q => q.applicationRef == applicationId && q.status == "Pending").ToList();
            if (ApplicationQueries.Count > 0)
            {
                //Code to Send email to user
                //Code to update the Company info
                var update = await client.GetAsync($"{Globals.Globals.end_point_post_update_companyinfo}/" + applicationId);
                if (update.IsSuccessStatusCode)
                {
                    //Code to Send email to user
                    var email1 = "email2";// user.Email;
                    SmtpClient emailclient1 = new SmtpClient("mail.ttcsglobal.com");
                    emailclient1.UseDefaultCredentials = false;
                    emailclient1.Credentials = new NetworkCredential("companiesonlinezw", "N3wPr0ducts@1");
                    // client.Credentials = new NetworkCredential("username", "password");
                    MailMessage mailMessage1 = new MailMessage();
                    mailMessage1.From = new MailAddress("companiesonlinezw@ttcsglobal.com");
                    mailMessage1.To.Add(email1);
                    mailMessage1.IsBodyHtml = true;
                    mailMessage1.Body = ("<!DOCTYPE html> " +
                        "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                        "<head>" +
                            "<title>Email</title>" +
                        "</head>" +

                        "<body style=\"font-family:'Century Gothic'\">" +
                        "<p><b>Hi Dear valued Customer</b></p>" +

                        "<p>You have a query in you company application, please go and review the process.</p>" +

                        "<a>https://deedsapp.ttcsglobal.com:6868/Auth/Login </a>" +
                        "<p>Please login and correct issues/p>" +
                       "<p> Enjoy our services.</p> " +

                        "<p>Regards</p>" +

                        "<p>DCIP</p>" +

                        "</body>" +
                        "</html>");//GetFormattedMessageHTML();
                    mailMessage1.Subject = "Company Application has Queries";
                    emailclient1.Send(mailMessage1);
                    // return RedirectToAction("Queries");
                    // return Json(new { });
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                //return View("WithQueries");
            }
            else
            {
                var resolve = await client.GetAsync($"{Globals.Globals.end_point_resolveQuery_companyinfo}/{applicationId}");
                if (resolve.IsSuccessStatusCode)
                {
                    var email ="email2";// user.Email;
                    SmtpClient emailclient = new SmtpClient("mail.ttcsglobal.com");
                    emailclient.UseDefaultCredentials = false;
                    emailclient.Credentials = new NetworkCredential("companiesonlinezw", "N3wPr0ducts@1");
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

                        "<p>Your Company Application has been Approved, use the link to login and Check your Company Application.</p>" +

                        "<a>https://deedsapp.ttcsglobal.com:6868/Auth/Login </a>" +
                        "<p>Please login/p>" +
                       "<p> Enjoy our services.</p> " +

                        "<p>Regards</p>" +

                        "<p>DCIP</p>" +

                        "</body>" +
                        "</html>");//GetFormattedMessageHTML();
                    mailMessage.Subject = "Company Application has been Approved";
                    emailclient.Send(mailMessage);
                }



                //Code to do Query Verification on the incoming ApplicationId
                //var result = await client.GetAsync($"{Globals.Globals.end_point_get_queries}").Result.Content.ReadAsStringAsync();
                //// var res= result.Content.ReadAsStringAsync();
                //var queries = JsonConvert.DeserializeObject<IEnumerable<mQuery>>(result).ToList();
                var response = await client.PostAsJsonAsync<string>($"{Globals.Globals.service_end_point}/PvtRegistration/{applicationId}/Approve", user.Email).Result.Content.ReadAsStringAsync();
                return Ok();
            }
           

        }


        private string SetupPaymentForApplication(string searchRef)
        {
            string uri = "";
            //var paynow = new Paynow("9848", "9cb58118-b8e3-45bc-b5b5-66a29ce71309");
            var paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");

            paynow.ResultUrl = "http://example.com/gateways/paynow/update";
            paynow.ReturnUrl = $"http://localhost:2015/Company/PayForCompany?step=1";

            var payment = paynow.CreatePayment(Guid.NewGuid().ToString(), "brightonkofu@outlook.com");

            payment.Add("Name search", 1);

            var response = paynow.Send(payment);


            if (response.Success())
            {
                uri = response.RedirectLink();

                var pollUrl = response.PollUrl();

                var status = paynow.PollTransaction(pollUrl);

                ViewBag.Paid = "false";
                Globals.Globals.payment = paynow;
                Globals.Globals.response = response;
            }

            return uri;
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
