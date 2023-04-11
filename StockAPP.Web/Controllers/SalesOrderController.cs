using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockApp.Web.Helpers;
using StockAPP.Web.Models;
using StockAPP.Web.Repository;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockAPP.Web.Controllers
{
    public class SalesOrderController : Controller
    {
        //Instance of api
        StockAPI _api = new StockAPI();
        StockRepository _repo = new StockRepository();

        //Index Page list of Orders
        public IActionResult Index(string SearchText = "", int pg = 1, int pageSize = 5)
        {
            PaginatedList<Sales_Order> orderlist = _repo.GetAllSalesOrders(SearchText, pg, pageSize).Result;
            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(orderlist.TotalRecords, pg, pageSize);
            this.ViewBag.Pager = pager;

            return View(orderlist);
        }

        [HttpGet]
        [Route("SalesOrder/Details/{Sales_Order_ID}")]
        public async Task<IActionResult> Details(int Sales_Order_ID)
        {
            var salesorder = new Sales_Order();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getsalesorderdetail/{Sales_Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                salesorder = JsonConvert.DeserializeObject<Sales_Order>(results);
            }
            return View(salesorder);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Sales_Order salesOrder = new Sales_Order();
            salesOrder.Sales_Order_Detail.Add(new Sales_Order_Detail() { Sales_OrderDetail_ID = 1 });
            return View(salesOrder);
        }

        [HttpPost]
        public IActionResult Create(Sales_Order salesorder)
        {
            salesorder.Order_Status = 1;
            salesorder.Order_Date = DateTime.Now;
            salesorder.Active = true;
            HttpClient client = _api.Initial();
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Sales_Order>("api/StockAPP/addsalesorder", salesorder);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Route("SalesOrder/Update/{Order_ID}")]
        public async Task<ActionResult> Update(int? Order_ID)
        {
            if (Order_ID == null || Order_ID == 0)
            {
                return NotFound();
            }

            var salesorder = new Sales_Order();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getsalesorderdetail/{Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                salesorder = JsonConvert.DeserializeObject<Sales_Order>(results);
            }

            return View(salesorder);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SalesOrder/Update/{Order_ID}")]
        public IActionResult Update(Sales_Order order)
        {
            HttpClient client = _api.Initial();
            //HTTP POST
            //item.Modified_Date = DateTime.Now;
            var postTask = client.PostAsJsonAsync<Sales_Order>("api/StockAPP/updatesalesorder", order);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
