using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.UserName = Username;
            return View();
        }
    }
}
