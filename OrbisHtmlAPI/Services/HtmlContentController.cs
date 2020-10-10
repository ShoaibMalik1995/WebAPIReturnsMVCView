using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http.Cors;
using DataAccess;
using DataAccess.Models;
using DataAccess.BAL;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;

namespace OrbisHtmlAPI.Services
{
    [System.Web.Http.RoutePrefix("api/htmlcontent")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HtmlContentController : ApiController
    {
        #region Propertise
        private AcademyBAL _ABAL;
        private LoginBAL _LBAL;
        private UsersBAL _UBAL;
        #endregion
        
        public HtmlContentController()
        {
            this._ABAL = new AcademyBAL();
            this._LBAL = new LoginBAL();
            this._UBAL = new UsersBAL();
        }

        #region Return cshtml View from Web APi

        [System.Web.Http.Route("GetMenuHeaderHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetMenuHeaderHtml(int AcademyId = 0, int UserId = 0)
        {
            try
            {
                string BuddyBossBaseUrl = ConfigurationManager.AppSettings["BuddyBossBaseUrl"].ToString();
                string group = "Coach";
                AcademyModels _academy;
                CommonModel model = new CommonModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", AcademyId);
                Param.Add("UserId", UserId);

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());

                _academy = _ABAL.getAcademyByAcademyId(AcademyId);
                _academy.EnableFrontEndWebsite = _academy.EnableFrontEndWebsite;
                model.AcademyId = AcademyId;
                model.AcademyName = _academy.AcademyName;
                model.DateFormat = _academy.DateFormat;
                model.AcademyLogo = _academy.AcademyLogo.Trim();
                //model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glflocker.com";
                model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glfbeta.com";
                //
                UserModel _CurrentUsermodel = new UserModel();
                _CurrentUsermodel.AcademyId = AcademyId;
                _CurrentUsermodel.Userid = UserId;
                _CurrentUsermodel = _LBAL.PrepareLogin(_CurrentUsermodel);
                model.CurrentUser = _CurrentUsermodel;
                //
                model.UserRoleList = _UBAL.GetUserRoles(UserId);
                //Get User Menu List
                //group = _LBAL.GetUserGroup(_CurrentUsermodel.UserRoleId);
                model.LoggedInUserId = _CurrentUsermodel.Userid;
                model.UserRoleId = _CurrentUsermodel.UserRoleId;
                model.MenuList = _LBAL.GetMainMenus(group, _CurrentUsermodel.Userid, _CurrentUsermodel.AcademyId);
                //Get Academy Settings
                model.AcademySettings = _ABAL.GetAcademySettings(AcademyId);
                model.TermsContent = model.AcademySettings.TermsAndConditionContent;
                model.PrivacyContent = model.AcademySettings.PrivacyContent;
                
                model.StoreUrl = ConfigurationManager.AppSettings["storeUrl"].ToString();

                //
                /*
                DataTable dt = _UBAL.GetPlayerCountryList();
                List<SelectListItem> ddlCountry = dt.AsEnumerable().Select(row => new SelectListItem()
                {
                    Value = row["Id"].ToString(),
                    Text = row["Name"].ToString(),
                    Selected = row["Id"].ToString() == model.CurrentUser.Country.ToString() ? true : false,
                }).ToList();
                */
                model.CountryList = new List<SelectListItem>();
                //
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/Header.cshtml", model, true);
                //
                var response = new HttpResponseMessage();
                response.Content = new StringContent(stringView);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }

        [System.Web.Http.Route("GetFooterHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetFooterHtml(int AcademyId)
        {
            try
            {
                int UserId = 0;
                string group = "Manager";
                AcademyModels _academy;
                CommonModel model = new CommonModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", AcademyId);
                Param.Add("UserId", UserId);

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());

                _academy = _ABAL.getAcademyByAcademyId(AcademyId);
                _academy.EnableFrontEndWebsite = _academy.EnableFrontEndWebsite;
                model.AcademyName = _academy.AcademyName;
                model.DateFormat = _academy.DateFormat;
                model.AcademyLogo = _academy.AcademyLogo;
                //model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glflocker.com";
                model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glfbeta.com";
                //
                UserModel _CurrentUsermodel = new UserModel();
                /*
                _CurrentUsermodel.AcademyId = AcademyId;
                _CurrentUsermodel.Userid = UserId;
                _CurrentUsermodel = _LBAL.PrepareLogin(_CurrentUsermodel);
                */
                model.CurrentUser = _CurrentUsermodel;
                //Get User Menu List
                /*
                group = _LBAL.GetUserGroup(_CurrentUsermodel.UserRoleId);
                model.LoggedInUserId = _CurrentUsermodel.Userid;
                model.UserRoleId = _CurrentUsermodel.UserRoleId;
                model.MenuList = _LBAL.GetMainMenus(group, _CurrentUsermodel.Userid, _CurrentUsermodel.AcademyId);
                */
                //Get Academy Settings
                model.AcademySettings = _ABAL.GetAcademySettings(AcademyId);
                model.TermsContent = model.AcademySettings.TermsAndConditionContent;
                model.PrivacyContent = model.AcademySettings.PrivacyContent;
                model.StoreUrl = ConfigurationManager.AppSettings["storeUrl"].ToString();
                //
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/Footer.cshtml", model, true);
                //
                var response = new HttpResponseMessage();
                response.Content = new StringContent(stringView);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }
        public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();
            // get context wrapper from HttpContext if available
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException("Cannot create Controller Context if no active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }

        /**
         * Return Just Header API
         * 
         */
        [System.Web.Http.Route("GetHeaderHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetHeaderHtml(int AcademyId = 0, int UserId = 0)
        {
            try
            {
                string group = "Manager";
                AcademyModels _academy;
                CommonModel model = new CommonModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", AcademyId);
                Param.Add("UserId", UserId);

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());

                _academy = _ABAL.getAcademyByAcademyId(AcademyId);
                _academy.EnableFrontEndWebsite = _academy.EnableFrontEndWebsite;
                model.AcademyId = AcademyId;
                model.AcademyName = _academy.AcademyName;
                model.DateFormat = _academy.DateFormat;
                model.AcademyLogo =  _academy.AcademyLogo;
                //model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glflocker.com";
                model.AbsPathURL = _academy.AcademyTitle.ToString() + ".glfbeta.com";
                //
                UserModel _CurrentUsermodel = new UserModel();
                _CurrentUsermodel.AcademyId = AcademyId;
                _CurrentUsermodel.Userid = UserId;
                _CurrentUsermodel = _LBAL.PrepareLogin(_CurrentUsermodel);
                model.CurrentUser = _CurrentUsermodel;
                //Get User Menu List
                group = _LBAL.GetUserGroup(_CurrentUsermodel.UserRoleId);
                model.LoggedInUserId = _CurrentUsermodel.Userid;
                model.UserRoleId = _CurrentUsermodel.UserRoleId;
                model.MenuList = _LBAL.GetMainMenus(group, _CurrentUsermodel.Userid, _CurrentUsermodel.AcademyId);
                //Get Academy Settings
                model.AcademySettings = _ABAL.GetAcademySettings(AcademyId);
                model.TermsContent = model.AcademySettings.TermsAndConditionContent;
                model.PrivacyContent = model.AcademySettings.PrivacyContent;
                model.StoreUrl = ConfigurationManager.AppSettings["storeUrl"].ToString();
                //model.HomeUrl = string.IsNullOrEmpty(model.HomeUrl) ? "" : model.HomeUrl;
                //
                model.UserRoleList = _UBAL.GetUserRoles(UserId);
                //
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/_Header.cshtml", model, true);
                //
                var response = new HttpResponseMessage();
                response.Content = new StringContent(stringView);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }

        public async Task<HttpResponseMessage> GetLogedInUser(int AcademyId, int UserId)
        {
            try
            {
                HttpResponseMessage Res = new HttpResponseMessage();
                UserModel logedInUser = new UserModel();
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
                    Res = await client.GetAsync("api/htmlcontent/GetMenuHeaderHtml?AcademyId=1176&UserId=39058");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list  
                    }
                    // Footer
                    Res = await client.GetAsync("api/htmlcontent/GetFooterHtml?AcademyId=1176");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        EmpResponse = EmpResponse + Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list  
                    }
                    
                }
                return Request.CreateResponse(HttpStatusCode.OK, logedInUser);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }
        #endregion

        #region EncryptString / DecryptString

        [System.Web.Http.Route("GetEncryptString")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetEncryptString([FromBody] Dictionary<string, object> param)
        {
            try {
                Dictionary<string, object> list = new Dictionary<string, object>();
                foreach (string key in param.Keys)
                {
                    string keyval = param[key].ToString();
                    if(!list.ContainsKey(key))
                        list.Add(key,DataAccess.Common.Common.EncryptString(keyval));
                }

                string json = JsonConvert.SerializeObject(list);
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch(Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }            
        }

        [System.Web.Http.Route("GetDecryptString")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetDecryptString([FromBody] Dictionary<string, object> param)
        {
            try 
            {
                Dictionary<string, object> list = new Dictionary<string, object>();
                foreach (string key in param.Keys)
                {
                    string keyval = param[key].ToString();
                    if (!list.ContainsKey(key))
                        list.Add(key, DataAccess.Common.Common.DecryptString(keyval));
                }

                string json = JsonConvert.SerializeObject(list);
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }

        [System.Web.Http.Route("GetDecryptString")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetDecryptString(string uId, string email)
        {
            try
            {
                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("userId", DataAccess.Common.Common.DecryptString(uId));
                list.Add("email", DataAccess.Common.Common.DecryptString(email));
                
                string json = JsonConvert.SerializeObject(list);
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }
        #endregion
    }
}
