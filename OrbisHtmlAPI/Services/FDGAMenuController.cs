using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DataAccess.Models;
using MenuBarApi.Models;
using DataAccess.BAL;
using System.Web.Http.Cors;

namespace MenuBarApi.Services
{
    [System.Web.Http.RoutePrefix("api/fdgamenu")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FDGAMenuController : ApiController
    {
        #region Propertise
        private AcademyBAL _ABAL = new AcademyBAL();
        private LoginBAL _LBAL = new LoginBAL();
        private UsersBAL _UBAL = new UsersBAL();
        #endregion

        #region Basic
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        #endregion

        #region Return cshtml View from Web APi
        [System.Web.Http.Route("GetHeaderHtml")]
        [System.Web.Http.AcceptVerbs("GET","POST")]
        public HttpResponseMessage GetHeaderHtml([FromBody]Dictionary<string,string> data)
        {
            try
            { 
                int AcademyId =0, UserId = 0;
                string group = "Manager";
                AcademyModels _academy;
                AcademyModel model = new AcademyModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId",Convert.ToInt32(data["AcademyId"]));
                Param.Add("UserId", Convert.ToInt32( data["UserId"]));

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());
                //
                _academy = AcademyId > 0 ? _ABAL.getAcademyByAcademyId(AcademyId) : new AcademyModels();
                _academy.EnableFrontEndWebsite = AcademyId > 0 ? _academy.EnableFrontEndWebsite : false;
                //
                LoginModels _loginmodel = new LoginModels();
                _loginmodel.AcademyId = AcademyId;
                _loginmodel.Userid = UserId;
                _loginmodel = _LBAL.ValidateUserLogin(_loginmodel);
                group = _LBAL.GetUserGroup(_loginmodel.UserRoleId);
                //
                model.AcademyId = AcademyId;
                model.loginModel = _loginmodel;
                model.loginModel.UsersMenu = _LBAL.GetMainMenus(group, _loginmodel.Userid, _loginmodel.AcademyId);
                model.EnableFrontEndWebsite = _academy.EnableFrontEndWebsite;
                //
                var stringView = RenderViewToString(CreateController<OrbisHtmlAPI.Controllers.HomeController>().ControllerContext, "~/Views/Home/_Header.cshtml", model, true);
                //
                var response = new HttpResponseMessage();
                response.Content = new StringContent(stringView);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }
        [System.Web.Http.Route("GetMenuHeaderHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetMenuHeaderHtml(int AcademyId=0, int UserId=0)
        {
            try
            {
                string group = "Manager";
                AcademyModels _academy;
                AcademyModel model = new AcademyModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", AcademyId);
                Param.Add("UserId", UserId);

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());
                //
                _academy = AcademyId > 0 ? _ABAL.getAcademyByAcademyId(AcademyId) : new AcademyModels();
                _academy.EnableFrontEndWebsite = AcademyId > 0 ? _academy.EnableFrontEndWebsite : false;
                //
                LoginModels _loginmodel = new LoginModels();
                _loginmodel.AcademyId = AcademyId;
                _loginmodel.Userid = UserId;
                _loginmodel = _LBAL.ValidateUserLogin(_loginmodel);
                group = _LBAL.GetUserGroup(_loginmodel.UserRoleId);
                //
                model.AcademyId = AcademyId;
                model.loginModel = _loginmodel;
                model.loginModel.UsersMenu = _LBAL.GetMainMenus(group, _loginmodel.Userid, _loginmodel.AcademyId);
                model.EnableFrontEndWebsite = _academy.EnableFrontEndWebsite;
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


        [System.Web.Http.Route("GetFooterHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetFooterHtml([FromBody] Dictionary<string, string> data)
        {
            try
            {
                int AcademyId = 0, UserId = 0;
                AcademyModel model = new AcademyModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", Convert.ToInt32(data["AcademyId"]));
                Param.Add("UserId", Convert.ToInt32(data["UserId"]));

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());

                model.AcademyId = AcademyId;
                //
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/_Footer.cshtml", model, true);
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

        [System.Web.Http.Route("GetFDGAFooterHtml")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetFDGAFooterHtml(int AcademyId)
        {
            try
            {
                int UserId = 0;
                AcademyModel model = new AcademyModel();
                Dictionary<string, int> Param = new Dictionary<string, int>();
                Param.Add("AcademyId", AcademyId);
                Param.Add("UserId", UserId);

                if (Param.ContainsKey("AcademyId"))
                    AcademyId = Convert.ToInt32(Param["AcademyId"].ToString());
                if (Param.ContainsKey("UserId"))
                    UserId = Convert.ToInt32(Param["UserId"].ToString());

                model.AcademyId = AcademyId;
                //
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/_Footer.cshtml", model, true);
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

        [System.Web.Http.ActionName("GetMenuBar")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMenuBar()
        {
            try
            {
                List<Order> list = new List<Order>() {
                    new Order() { OrderId=1, CustomerName="ABC", Date=DateTime.Now, IsActive=true },
                    new Order() { OrderId=2,CustomerName="DEF",Date=DateTime.Now,IsActive=true },
                };
                var stringView = RenderViewToString(CreateController<Controllers.HomeController>().ControllerContext, "~/Views/Home/_List.cshtml", list, true);

                return Ok(stringView);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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

        #endregion  Return cshtml View from Web APi
    }
}