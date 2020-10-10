using DataAccess.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Net;
using System.Collections.Specialized;
using DataAccess.Models;

namespace OrbisHtmlAPI.Controllers
{
    public class HomeController : Controller
    {
        private UsersBAL _UBAL = new UsersBAL();
        private AcademyBAL _ABAL = new AcademyBAL();
        public ActionResult Index()
        {
            ViewBag.AcademyId = 72;
            ViewBag.UserId = 54417;
            //string Encryemail = DataAccess.Common.Common.EncryptString("bahmad@jjtestsite.us");
            //DataTable dt = _ABAL.GetAcademyData();
            //NameValueCollection formData = new NameValueCollection();
            //formData.Add("ReturnUrl", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            //formData.Add("academyId", "1176");
            //formData.Add("userid", "39058");
            //formData.Add("first_name", "Dev");
            //formData.Add("last_name", "MSK");
            //formData.Add("username", "devmsk47");
            //formData.Add("email", "devmsk47@glf.com");
            //formData.Add("password", "biology1");
            //string Url = "https://dev.jjtestsite.us/BuddyBossGLF/login-processing/";

            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //    byte[] result = client.UploadValues(Url, "POST", formData);
            //    string ResultAuthTicket = System.Text.Encoding.UTF8.GetString(result);
            //}            

            //return RedirectToAction("BuddyBossloginProcessing");

            return View();
        }

        public ActionResult BuddyBossloginProcessing(bool valid = false, string msg = "", string baseurl = "")
        {
            if (!valid && string.IsNullOrEmpty(msg) && string.IsNullOrEmpty(baseurl))
            {

                int orignalAcademyId = 72;
                int orignaluserId = 54417;
                string orignalAcademyTitle = "clubcorpacademy";
                string useremail = "testcoach@glfbeta.com";
                string orignalReturnUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;

                string EncryAcademyId = DataAccess.Common.Common.EncryptString(orignalAcademyId.ToString());
                string EncryUserId = DataAccess.Common.Common.EncryptString(orignaluserId.ToString());
                string EncryReturnUrl = DataAccess.Common.Common.EncryptString(orignalReturnUrl.ToString());
                string Encryemail = DataAccess.Common.Common.EncryptString(useremail.ToString());

                int DecryAcademyId = Convert.ToInt32(DataAccess.Common.Common.DecryptString(EncryAcademyId));
                int DecryUserId = Convert.ToInt32(DataAccess.Common.Common.DecryptString(EncryUserId));
                string DecryReturnUrl = DataAccess.Common.Common.DecryptString(EncryReturnUrl.ToString());
                
                ViewBag.email = Encryemail;
                ViewBag.academyId = EncryAcademyId;
                ViewBag.userId = EncryUserId;
                ViewBag.ReturnUrl = orignalReturnUrl;
                string hashset = orignalAcademyId.ToString() + "_" + orignalAcademyTitle.ToString() + ".glfbeta.com";
                ViewBag.HostMDHash = DataAccess.Common.Common.EncryptStringMD5HASH(hashset);

                return View();
            }
            else
            {
                TempData["model"] = new WPUserModel { UserId = 1, AcademyId = 2, AcademyTitle = "aaa", Email = "ssss" };
                return RedirectToAction("About");
            }
        }

        public ActionResult About()
        {
            WPUserModel _m = TempData["model"] != null ? (WPUserModel) TempData["model"] : null;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult TestPost(FormCollection form)
        {
            int _AcademyId = 0;
            string _UserName = "", _Password = "";
            foreach (string key in form.AllKeys)
            {
                if (key.ToLower() == "academyid")
                    _AcademyId = string.IsNullOrEmpty(form[key].ToString()) ? 0 : Convert.ToInt32(form[key].ToString());
                if (key.ToLower() == "username")
                    _UserName = form[key].ToString();
                if (key.ToLower() == "password")
                    _Password = form[key].ToString();
            }
            var result = new { status = "Success", AcademyId = _AcademyId, UserName = _UserName };
            return Json(result, "SUccess", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GetPlayerCountryList_NBS()
        {
            DataTable dt = _UBAL.GetPlayerCountryList();
            string ddlCountry = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlCountry += "<option value=\"" + dt.Rows[i]["Id"].ToString() + "\">" + dt.Rows[i]["Name"].ToString() + "</option>";
            }
            return ddlCountry;
        }

        public async Task<String> GetHeaderFooterHtml()
        {
            ViewBag.Message = "Your application description page.";
            string EmpResponse = "";
            string Baseurl = "http://app.glfbeta.com/MenuHtml/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                //Header Calls
                HttpResponseMessage Res = await client.GetAsync("api/htmlcontent/GetMenuHeaderHtml?AcademyId=1176&UserId=39058");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                }
                // Clients
                Res = await client.GetAsync("api/htmlcontent/GetFooterHtml?AcademyId=1176");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    EmpResponse = EmpResponse + Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                }

            }
            return EmpResponse;
        }
    }

    public class WPUserModel
    {
        public int UserId { get; set; }
        public int AcademyId { get; set; }
        public string Email { get; set; }
        public string AcademyTitle { get; set; }
    }
}