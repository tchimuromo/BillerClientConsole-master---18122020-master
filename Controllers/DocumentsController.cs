using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IronPdf;
using IronBarCode;
using System.Drawing;
using System.Net.Http;
using BillerClientConsole.Globals;
using Newtonsoft.Json;
using BillerClientConsole.Models;
using System.Globalization;

namespace BillerClientConsole.Controllers
{
    [Route("Docs")]
    public class DocumentsController : Controller
    {  
        [Route("Test")]
        public IActionResult Test(string searchref)
        {

            var clientw = new HttpClient();
            var res = clientw.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={searchref}").Result.Content.ReadAsStringAsync().Result;
            var i = 0;

            dynamic json_data = JsonConvert.DeserializeObject(res);
            //var data = json_data.data.value[0].companyInfo;
            //List<mCompanyInfo> names = JsonConvert.DeserializeObject<List<mCompanyInfo>>(data.ToString());
            //var names =    JsonConvert.DeserializeObject<List<mCompanyInfo>>(data.ToString());

            //companyInfo 
            var dattta = json_data.data.value.companyInfo;
            mCompanyInfo companyInfo = JsonConvert.DeserializeObject<mCompanyInfo>(dattta.ToString());

            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            //var pdf = Renderer.RenderUrlAsPdf("https://www.nuget.org/packages/IronPdf");
            var pdf = PdfDocument.FromFile("C:\\My\\COICODE.pdf");
            //var ForegroundStamp = new HtmlStamp() { Html = "<h2 style='color:black'>copyright 2018 ironpdf.com", Width = 500, Height = 500, Opacity = 100, Rotation = 0, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            //pdf.StampHTML(ForegroundStamp);
            //pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");

            //var PDF = Renderer.RenderHtmlAsPdf("<img src='icons/logo.jpeg'>", @"C:\site\assets\");
            //PDF.SaveAs("html-with-assets.pdf");

            //Header certificate of incorporation
            string name = "Oliver";
            var ForegroundStamp1 = new HtmlStamp() { Html = $"<h1 style='color:black;font-size:36px'><b>Certificate of Incorporation</b></h1>", Top = 250, Rotation = 0, Width = 500, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            pdf.StampHTML(ForegroundStamp1);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");



            // string name = "Oliver";
            var ForegroundStamp2 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>I hereby certify that</h5>", Top = 325, Rotation = 0, Width = 500, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            pdf.StampHTML(ForegroundStamp2);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");


            // string name = COMPANY NAME";
            var ForegroundStamp3 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:18px'><b>{companyInfo.Name} (PRIVATE) LIMITED </b></h5>", Top = 350, Rotation = 0, Width = 500,ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            pdf.StampHTML(ForegroundStamp3);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");

            //footer with  date and details

            var ForegroundStamp12 = new HtmlStamp()
            {
                Html = $"<h5 style='color:black;font-size:11px'><b>For further details relating to this company scan QRcode<br>" +
                $"or <a style='color:blue;'>visit www.dcip.co.zw/verifycompanydetails</a><br>" +
                $"Certificate system generated {DateTime.Now}</b></h5>",
                Bottom = 35,
                Left = 370,
                Rotation = 0,
                ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent
            };
            pdf.StampHTML(ForegroundStamp12);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            //image
            //var ForegroundStamp10 = new HtmlStamp() { Html = "<img style='height:200px;width:200px; opacity:0.5;' src='C:\\My\\download.png'>", Top = 300, Rotation = 0, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            //pdf.StampHTML(ForegroundStamp10);
            //pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");

            //string ForegroundStamp10 = "<img style='height:200px;width:200px;' src='C:\\My\\download.png'>";
            //pdf.WatermarkAllPages(ForegroundStamp10);
            //pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            //<br><h5 style='color:black;font-size:25px'><b>({companyInfo.RegNumber})</b></h5>
            var ForegroundStampn = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:18px'><b>({companyInfo.RegNumber})</b></h5>", Top = 365, Rotation = 0, Width = 500, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            pdf.StampHTML(ForegroundStampn);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");

            // string name = COMPANY NAME";
            var ForegroundStamp4 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'><i>is this day incorporated under the Companies and Other Business Entities Act [Chapter 24:31] and that the company is limited</i></h5>", Top = 410, Rotation = 0, Width = 125, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            pdf.StampHTML(ForegroundStamp4);
            pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");

            string day = companyInfo.Date_Of_Incoperation;
            DateTime date = Convert.ToDateTime(day);
            //var dateb = date.ToShortDateString();
            //var dateTimeb = DateTime.Parse(day);
            int year = date.Year;
            int month = date.Month;
            int dayc = date.Day;

            string.Format("{0}/{1}/{2}", month, dayc, year);


            //get month
            var monthabrv = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            // string[] formats = { "mm/dd/yyyy hh:mm:ss aa" };
            //var dateTime = DateTime.ParseExact(day, "M/d/yyyy", CultureInfo.InvariantCulture);

            //var daynum = day[0] ;
            // fixing indexes
            if (dayc  == 1) { string index = "st";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc} {index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else if (dayc == 2) { string index = "nd";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else if (dayc == 3) { string index = "rd";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
             else if (dayc == 21)
            {
                string index = "st";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc} {index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else if (dayc == 22)
            {
                string index = "nd";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else if (dayc == 23)
            {
                string index = "rd";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else if (dayc == 31)
            {
                string index = "st";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc} {index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }

            else if (dayc > 3) { string index = "th";
                var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

                pdf.StampHTML(ForegroundStamp5);
                pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            }
            else { string index = "none"; }


            
            // string name = COMPANY NAME";
            //if (dayc == 1)
            //{
            //    var ForegroundStamp5 = new HtmlStamp() { Html = $"<h5 style='color:black;font-size:16px'>{companyInfo.Date_Of_Incoperation}Given under my hand this {dayc}{index} day of {monthabrv} {year}</h5>", Top = 455, Rotation = 0, Width = 505, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };

            //    pdf.StampHTML(ForegroundStamp5);
            //    pdf.SaveAs(@"C:\My\HtmlToPDFRAW.pdf");
            //}


            //GeneratedBarcode QRWithLogo = QRCodeWriter.CreateQrCode($"{companyInfo.Name}" +
            //   $"{companyInfo.RegNumber}" + "https://deedsapp.ttcsglobal.com:6868/");
            //QRWithLogo.ResizeTo(125, 125).SetMargins(1).ChangeBarCodeColor(Color.Black);

            //GeneratedBarcode MyBarCode = BarcodeWriter.CreateBarcode("1234567890", BarcodeWriterEncoding.Code128);

            //QRWithLogo.StampToExistingPdfPage("C:\\My\\HtmlToPDFRAW.pdf", 169, 600, 1);  // position x=200 y=50 on page 1


            //string DocPath = @"C:/My/" + $"_Fiscalreport4.pdf";
            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] byteArray = client.DownloadData("C:\\My\\HtmlToPDFRAW.pdf");

            ViewBag.title = "New Search";
            return new FileContentResult(byteArray, "application/pdf");
            //return View();
        }


        [Route("Summary")]
        public IActionResult PvtOutputDoc(string searchref)
        {
            //  string applicationId = "81f768d8-c454-47cb-8986-7544d50870b5"; 

            var Renderer = new IronPdf.HtmlToPdf();

            List<string> HtmlList = new List<string>();
            string[] HtmlArray;

            var clientw = new HttpClient();
            var res = clientw.GetAsync($"{Globals.Globals.end_point_get_company_application_by_search_ref}?SearchRef={searchref}").Result.Content.ReadAsStringAsync().Result;
            var i = 0;

            dynamic json_data = JsonConvert.DeserializeObject(res);
            //var data = json_data.data.value[0].companyInfo;
            //List<mCompanyInfo> names = JsonConvert.DeserializeObject<List<mCompanyInfo>>(data.ToString());
            //var names =    JsonConvert.DeserializeObject<List<mCompanyInfo>>(data.ToString());

            //companyInfo 
            var dattta = json_data.data.value.companyInfo;
            mCompanyInfo companyInfo = JsonConvert.DeserializeObject<mCompanyInfo>(dattta.ToString());
            //members
            var datamembers = json_data.data.value.members;
            List<mMembersInfo> member = JsonConvert.DeserializeObject<List<mMembersInfo>>(datamembers.ToString());

            //memo .liabilityClause
            var dataliabilityClause = json_data.data.value.memo.liabilityClause;
            List<liabilityClause> liabilityClause = JsonConvert.DeserializeObject<List<liabilityClause>>(dataliabilityClause.ToString());
            //memo .shareClause
            var datasharesClause = json_data.data.value.memo.sharesClause;
            List<sharesClause> sharesClauser = JsonConvert.DeserializeObject<List<sharesClause>>(datasharesClause.ToString());
            
            //memo .objects
            var dataobjects = json_data.data.value.memo.objects;
            List<mmainClause> objective = JsonConvert.DeserializeObject<List<mmainClause>>(dataobjects.ToString());

            //articles                       
            var dataarticles = json_data.data.value.articles;
            mArticles articles = JsonConvert.DeserializeObject<mArticles>(dataarticles.ToString());

            ////memo membersprotifolio 
            var datamembersprotifolios = json_data.data.value.membersPotifolios;
            List<mMembersPotifolio> membersprotifolio = JsonConvert.DeserializeObject<List<mMembersPotifolio>>(datamembersprotifolios.ToString());

            //registered office details
            string applicationId = companyInfo.Application_Ref;
            var result = clientw.GetAsync($"{Globals.Globals.service_end_point}/{applicationId}/Details").Result.Content.ReadAsStringAsync().Result;
            dynamic json_datab = JsonConvert.DeserializeObject(result);

            var officedetail = json_datab.office;
            RegisteredOffice office = JsonConvert.DeserializeObject<RegisteredOffice>(officedetail.ToString());


                string header = @"<html> <meta http-equiv = 'content-type' content = 'text/html; charset=utf-8' />";
            HtmlList.Add(header);

            string style = @"<style> body { background: transparent; } " +

                " table { border-collapse: separate; border-spacing: 0; color: #4a4a4d; font: 14px/1.4 'Helvetica Neue', Helvetica, Arial, sans-serif; }" +
                " th, td { padding: 10px 15px; vertical-align: middle; } thead { background: #395870; color: #fff; } th:first-child { text-align: left; }" +
                " tbody tr:nth-child(even){ background: #f0f0f2; } td { border - bottom: 1px solid #cecfd5; border - right: 1px solid #cecfd5; } " +
                " td: first - child { border - left: 1px solid #cecfd5; } .book - title { color: #395870; display: block; } .item - stock, .item - qty { text - align: center; }" +
                " .item - price { text - align: right; } .item - multiple { display: block; } tfoot { text - align: right; } tfoot tr:last - child { background: #f0f0f2; }" +
                "</style>";
            HtmlList.Add(style);


            //border: 1px solid black; 

            // inittiating company status handler
            DateTime date = Convert.ToDateTime(companyInfo.Date_Of_Incoperation);

            //var date = companyInfo.Date_Of_Incoperation;
            var dateinc = date.ToShortDateString();
            string html = @" <meta http-equiv='content-type' content='text/html; charset=utf-8' /><img src='logo.jpeg'><html style = 'p.dashed {border - style: dashed;}'><table style='font-size:16px'><tr><td >Entity No.&nbsp;&nbsp;&nbsp;&nbsp;</td>"
            + $"<td>{companyInfo.RegNumber}</td></tr><tr> <td>Entity Name</td><td>{companyInfo.Name} {companyInfo.Type}</td></tr>"
    + $"<tr><td>Date of Incorporation<span></span></td><td>{dateinc}</td></tr>";
   
            HtmlList.Add(html);

            // inittiating company status handler
            if (companyInfo.Status == "Approved")
            {
                string htmla =  $"<tr>  <td>Status	</td><td>Active</td>"
                    + "</tr></table>"

                    + "<hr>";
                HtmlList.Add(htmla);
            }
            

            string html2 = $"<p>Section 31 and 241 of Act</p>"
           + "<p>Section 10, 11, 13, 14, 15, 17, 18, 20 of Regulations</p>"
           + "<p><b>Situation and Postal Address of a Company’s Registered Office or of a Foreign Company’s Principal Place of Business</b></p>"

           + "<hr>"

           +"<br>"
           +"<p><b>CURRENT ADDRESS</b></p>"

           +"<hr>"

           + $"<table><tr><td>Situated at</td><td>:</td><td>{office.PhysicalAddress}."
           + "</td></tr>"
           + $"  <tr> <td>Postal Address</td><td>:</td><td>{office.PostalAddress}.</td></tr>"
           + $"  <tr> <td>Email Address</td><td>:</td><td>{office.EmailAddress}.</td></tr></table>"

           + "<hr>"
           + "<br>"

           + "<p><b>PREVIOUS ADDRESS</b></p>"

           + "<hr>"

           + $"<table><tr><td >Situated at</td><td>:</td><td>{companyInfo.PostalAddress}."
           + "</td></tr>"
           + $"  <tr> <td>Postal Address</td><td>:</td><td>{companyInfo.Registered_Address}.</td></tr>"
           + $"  <tr> <td>Email Address</td><td>:</td><td>{companyInfo.Registered_Address}.</td></tr></table>"

           + "<hr>"

           +"<br>"
           + $"<tr><td>With effect from<span></span></td><td>:</td><td>{companyInfo.Date_Of_Incoperation}</td></tr>"
               

           + "</tr></table>"

           + "<p>NOTES:-   (a)   In the case of address, online update must be submitted to the registrar BEFORE the proposed change takes place."
           + "<p style='page-break-before:always'>";
            HtmlList.Add(html2);

            ////string html3 = @" <meta http - equiv = 'content-type' content = 'text/html; charset=utf-8' /><img src = 'logo.jpeg' ><html style = 'p.dashed {border - style: dashed;}' ><table style = 'font-size:16px' >< tr >< td > Entity No.& nbsp; &nbsp; &nbsp; &nbsp;</ td > "
            ////                         + $"<td>{companyInfo.RegNumber}</td></tr><tr> <td>Entity Name</td><td>{companyInfo.Name} {companyInfo.Type}</td></tr>"
            ////                 + $"<tr><td>Date of Incorporation<span></span></td><td>{dateinc}</td></tr>";

            HtmlList.Add(html);


            //status handler
            if (companyInfo.Status == "Approved")
            {
                string htmla = $"<tr>  <td>Status	</td><td>active</td>"
                    + "</tr></table>"

                    + "<hr>";
                HtmlList.Add(htmla);
            }


            string html4 = $" <p><b> DIRECTORS</b></p><br> <table> <tr><th>Date of<br>Appointment</th><th>Present Christian Names,<br>Surnames </th><th> ID/Passport#s</th><th>Nationality</th><th>Full Residential or<br>Business Address and<br>Postal Address</th><th>Nature of<br>Change</th></tr>";
            HtmlList.Add(html4);

            var numb = member.Count;
            
            /////////////////////////////////////////////////////////////

            var numg = membersprotifolio.Count;
            string html17 = "";
            for (int j = 0; j < numg; j++)
            {
                //code needs fields change and date of appointment
                //if director
               if (membersprotifolio[j].IsDirector == 1) { html17 += $"<tr><td> {dateinc}</td><td> {member[j].Names} {member[j].Surname} </td><td>{member[j].member_id}</td><td> {member[j].Nationality}.</td><td> {member[j].City} </td> <td>Director</td></tr>"; }
             
            }
            HtmlList.Add(html17);
            /////////////////////////////////////////////////////////////
            ///var numb = member.Count;
            //string html5 = "";
            //for (int j = 0; j < numb; j++)
            //{
            //    if (member[j].memberType == "Entity")
            //    {
            //        //code needs fields change and date of appointment
            //        html5 += $"<tr><td> {member[j].Names}{member[j].Surname}{member[j].ID_No}</td><td> {member[j].Nationality} </td><td> {member[j].Street}{member[j].City}</td><td> Appointed </td><td> {member[j].Street} entity </td></tr>";
            //    }
            //    else { }
            //}
            //HtmlList.Add(html5);

            string html6 = $"</table> <br><br>";
                HtmlList.Add(html6);


            string html4B = $" <p><b> SECRETARIES or PRINCIPAL OFFICER</b></p><br> <table> <tr><th>Date of<br>Appointment</th><th>Present Christian Names,<br>Surnames </th><th> ID/Passport#s</th><th>Nationality</th><th>Full Residential or<br>Business Address and<br>Postal Address</th><th>Nature of<br>Change</th></tr>";
            HtmlList.Add(html4B);

            //var numb = member.Count;

           // var numg = membersprotifolio.Count;
            string html15b = "";
            for (int j = 0; j < numg; j++)
            {
                //code needs fields change and date of appointment
                //if director
                if (membersprotifolio[j].IsCoSec== 1) { html15b += $"<tr><td> {dateinc}</td><td> {member[j].Names} {member[j].Surname} </td><td>{member[j].member_id}</td><td> {member[j].Nationality}.</td><td> {member[j].City} </td> <td>Secretary</td></tr>"; }

            }
            HtmlList.Add(html15b);


            string html6b = $"</table> <br> ";

                HtmlList.Add(html6b);






            string html6c = "  <hr>"
           + "  <p style='page-break-before:always'>";
            HtmlList.Add(html6c);

    //        string html7 = @" <meta http-equiv='content-type' content='text/html; charset=utf-8' /><img src='logo.jpeg'><html style = 'p.dashed {border - style: dashed;}'><table style='font-size:16px'><tr><td >Entity No.&nbsp;&nbsp;&nbsp;&nbsp;</td>"
    //        + $"<td>{companyInfo.RegNumber}</td></tr><tr> <td>Entity Name</td><td>{companyInfo.Name} {companyInfo.Type}</td></tr>"
    //+ $"<tr><td>Date of Incorporation<span></span></td><td>{dateinc}</td></tr>";

            HtmlList.Add(html);


            //status handler
            if (companyInfo.Status == "Approved")
            {
                string htmla = $"<tr>  <td>Status	</td><td>active</td>"
                    + "</tr></table>"

                    + "<hr>";
                HtmlList.Add(htmla);
            }



            string html8 =  $" <p><b>MAJOR OBJECT</b></p>";
            HtmlList.Add(html8);


            string html19 = $"{objective[0].objective}<br>";
            HtmlList.Add(html19);
            //memo .objects
            //var nume = objective.Count;
            //string html19 = "";
            //for (int j = 0; j < nume; j++)
            //{
            //    if (objective[0].objType == "Main")
            //    {
            //        html19 += $"{objective[j].objective}<br>";
            //    }
            //    else { }
            //}
            //HtmlList.Add(html19);

            string html10 = $"<p><b>LIABILITY CLAUSE</b></p>";
            HtmlList.Add(html10);

            // memo.liabilityClause
            var numc = liabilityClause.Count;
            string html11 = "";
            for (int j = 0; j < numc; j++)
            {
                html11 += $"{liabilityClause[j].description}<br>";

            }
            HtmlList.Add(html11);

            string html12 = $"<br><p><b>SHARE CAPITAL CLAUSE</b></p>";
            HtmlList.Add(html12);


            //memo .shareClause
            var numd = sharesClauser.Count;
            string html13 = "";
            for (int j = 0; j < numd; j++)
            {
                html13 += $"{sharesClauser[j].description}<br>";

            }
            HtmlList.Add(html13);


            //article
            string html14 = $"<p><b>ARTICLES OF ASSOCIATION</b></p><p>{articles.articles_type}</p><hr></html> <p style='page-break-before:always'> ";
            HtmlList.Add(html14);




    //        string html15 = @" <meta http-equiv='content-type' content='text/html; charset=utf-8' /><img src='logo.jpeg'><html style = 'p.dashed {border - style: dashed;}'><table style='font-size:16px'><tr><td >Entity No.&nbsp;&nbsp;&nbsp;&nbsp;</td>"
    //        + $"<td>{companyInfo.RegNumber}</td></tr><tr> <td>Entity Name</td><td>{companyInfo.Name} {companyInfo.Type}</td></tr>"
    //+ $"<tr><td>Date of Incorporation<span></span></td><td>{dateinc}</td></tr>";


            HtmlList.Add(html);


            //status handler
            if (companyInfo.Status == "Approved")
            {
               string htmla = $"<tr>  <td>Status	</td><td>active</td>"
                    + "</tr></table>"

                    + "<hr>";
                HtmlList.Add(htmla);
            }

            // NEW MEMBERS OF COMPANY LIST

            //string html4C = $" <p><b> MEMBSERS</b></p><br> <table> <tr><th>Fulll Name</th><th>ID/Passport<br>Number</th><th>Address</th><th>Number<br>of Shares</th></tr>";
            //HtmlList.Add(html4C);

            


            //string html6C = $"</table> <br><br>";
            //HtmlList.Add(html6C);


            // OLD MEMBERS OF COMPANY LIST

            string html16 = $" <p><b> MEMBERS</b></p><br>" +
                $"<p>Entity</p><table> <tr><th>Full names, IDs & Occupation of Subscribers</th><th>Ordinary Shares</th><th>Preference Shares</th><th>Total Shares	</th><th>Post</th></tr>";
            HtmlList.Add(html16);


            //var numb = member.Count;
            string html5C = "";
            for (int j = 0; j < numb; j++)
            {
                if (member[j].memberType == "Entity")
                {
                    //code needs fields change and date of appointment
                    html5C += $"<tr><td> {member[j].Names} {member[j].Surname} {member[j].ID_No}</td><td> {membersprotifolio[j].OrdinaryShares} </td><td> {membersprotifolio[j].PreferenceShares}</td><td> {membersprotifolio[j].number_of_shares} </td><td>  Entity </td></tr>"; 
                }
                else { }
            }
            HtmlList.Add(html5C);


            string html18 = "</table>"

          + "  <hr>";
            HtmlList.Add(html18);



            string html16d = $" <br>" +
                $"<p>Person</p><table> <tr><th>Full names, IDs & Occupation of Subscribers</th><th>Ordinary Shares</th><th>Preference Shares</th><th>Total Shares	</th><th>Post</th></tr>";
            HtmlList.Add(html16d);


            //var numb = member.Count;
            string html5d = "";
            for (int j = 0; j < numb; j++)
            {
                if (member[j].memberType == "Person")
                {
                    //code needs fields change and date of appointment
                    html5d += $"<tr><td> {member[j].Names} {member[j].Surname} {member[j].ID_No}</td><td> {membersprotifolio[j].OrdinaryShares} </td><td> {membersprotifolio[j].PreferenceShares}</td><td> {membersprotifolio[j].number_of_shares} </td><td>  Person </td></tr>";
                }
                else { }
            }
            HtmlList.Add(html5d);


            string html18d = "</table>"

          + "  <hr>";
            HtmlList.Add(html18d);

            // Generate QRCODE
            //GeneratedBarcode QRWithLogo = QRCodeWriter.CreateQrCode($"Company Name: {companyInfo.Name}" +
            //   $"Company Number: {companyInfo.Application_Ref }" + "url to app");
            //QRWithLogo.ResizeTo(50, 50).SetMargins(1).ChangeBarCodeColor(Color.Black);
            //QRWithLogo.SaveAsPng($"C:/My/QRCode.png").SaveAsPdf($"C:/My/QRCode.png.pdf");


            //create the final html string
            // HtmlList.Add(html11;          
            HtmlArray = HtmlList.ToArray();

            string finalhtml = string.Concat(HtmlArray);

            string DocPath = @"C:/My/" + $"_Fiscalreport4.pdf";
            // FileInfo fileInfo = new FileInfo(DocPath);
            //if (File.Exists(DocPath))
            //{

            //}
            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            //Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A3;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            //Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Landscape;
            // Renderer.PrintOptions.PaperOrientation = 0;

            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 500; //milliseconds
            Renderer.PrintOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;

            Renderer.PrintOptions.MarginTop = 60;
            Renderer.PrintOptions.MarginBottom = 60;
            Renderer.PrintOptions.MarginLeft = 15;
            Renderer.PrintOptions.MarginRight = 10;


            //Renderer.RenderHtmlAsPdf(finalhtml).SaveAs(DocPath);
            var bg = Renderer.RenderHtmlAsPdf(finalhtml);
            bg.AddBackgroundPdf(@"C:\\My\\bg.pdf");
            bg.SaveAs(DocPath);
            // var PDF = Renderer.RenderHtmlAsPdf(finalhtml);
            // var OutputPath = "Downloads/HtmlToPDF6.pdf";
            // PDF.SaveAs(OutputPath);

            //pull out file
            var pdf = PdfDocument.FromFile(DocPath);
            //count number of pages it has
            var pdfnum = pdf.PageCount;

            //var ForegroundStamp1 = new HtmlStamp() { Html = $"<h1 style='color:black;font-size:36px'><b>Certificate of Incorporation</b></h1>", Top = 250, Rotation = 0, Width = 500, ZIndex = HtmlStamp.StampLayer.OnTopOfExistingPDFContent };
            //pdf.StampHTML(ForegroundStamp1);
            //pdf.SaveAs(DocPath);

            ////generate QRCODE
            //GeneratedBarcode QRWithLogo = QRCodeWriter.CreateQrCode($"Company Name:New Products" +
            //    $"Company Number: 12345" + "url to app");
            //QRWithLogo.ResizeTo(75, 75).SetMargins(1).ChangeBarCodeColor(Color.Black);
            ////generate BARCODE
            //GeneratedBarcode MyBarCode = BarcodeWriter.CreateBarcode("1234567890", BarcodeWriterEncoding.Code128);
            //MyBarCode.ResizeTo(50, 50).SetMargins(1).ChangeBarCodeColor(Color.Black);



            ////Stamp qr Code
            //for (int j = 1; j <= pdfnum; j++)
            //{
            //    QRWithLogo.StampToExistingPdfPage(DocPath, 120, 740, j);  // position x=200 y=50 on page 1

            //    MyBarCode.StampToExistingPdfPage(DocPath, 350, 740, j);  // position x=200 y=50 on page 1

            //}

            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] byteArray = client.DownloadData(DocPath);


            //////////////////////////////////////////////////////////////////////
            ViewBag.title = "New Search";
            return new FileContentResult(byteArray, "application/pdf");



          


        }
    }
}
