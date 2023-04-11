using Microsoft.AspNetCore.Mvc;
using StockApp.Web.Helpers;
using StockAPP.Web.Models;
using StockAPP.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.Web.Controllers
{
    public class SalesBoxController : Controller
    {
        //Instance of api
        StockAPI _api = new StockAPI();
        StockRepository _repo = new StockRepository();

        //Index Page list of Orders
        [Route("SalesBox/Index/{Sales_Order_ID}")]
        public IActionResult Index(int Sales_Order_ID, string SearchText = "", int pg = 1, int pageSize = 5)
        {
            PaginatedList<Sales_Box> boxeslist = _repo.GetSalesBoxbySalesOrderID(Sales_Order_ID, SearchText, pg, pageSize).Result;
            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(boxeslist.TotalRecords, pg, pageSize);
            this.ViewBag.Pager = pager;

            return View(boxeslist);
        }
    }
}
