using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockApp.Web.Helpers;
using StockAPP.Web.ExtendedModel;
using StockAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockAPP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        

        #region constructor and global variables
        private readonly ILogger<HomeController> _logger;
        StockAPI _api = new StockAPI();

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        //Printing Method
        public IActionResult Print()
        {
            var dt = new DataTable();
            //dt = GetAllPurchaseOrder();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\rptStockApp.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(result.MainStream,"application/pdf");
        }

        //public DataTable GetAllPurchaseOrder()
        //{
        //    var dt = new DataTable();
        //    dt.Columns.Add("Order_Code");
        //    dt.Columns.Add("Order_Status");
        //    dt.Columns.Add("Order_Date");
        //    dt.Columns.Add("Active");
        //    dt.Columns.Add("Modified_Date");

        //    DataRow row;
        //}

        #region User Authorization and Authentication
        //Login Process Method
        public async Task<IActionResult> LoginUser()
        {
            //HttpClient client = _api.Initial();
            //string serializedUser = JsonConvert.SerializeObject(user);

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/AuthManagement/Login");
            //requestMessage.Content = new StringContent(serializedUser);

            //requestMessage.Content.Headers.ContentType
            //    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //string usernamelogged = null;
            //using (var response = await client.SendAsync(requestMessage))
            //{
            //    var responseStatusCode = response.StatusCode;
            //    var token = await response.Content.ReadAsStringAsync();
            //    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(token);
            //    string accessToken = jwtDynamic.Value<string>("accessToken");
            //    if (accessToken is null)
            //    {
            //        ViewBag.Message = "Incorrect Username or Password";
            //return Redirect("~/Home/Index");
        //}
        //UserLogged userauth = new UserLogged()
        //        {
        //            AccessToken = accessToken,
        //            UserName = jwtDynamic.Value<string>("username"),
        //            RefreshToken = jwtDynamic.Value<string>("refreshToken")
        //        };
        //        usernamelogged = userauth.UserName;
        //        HttpContext.Session.SetString("JWToken", token);
        //    }

            //return RedirectToAction("Index", "Dashboard", new { Username = usernamelogged });
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
