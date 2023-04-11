using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockApp.Web.Helpers;
using StockAPP.Web.Models;
using StockAPP.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockAPP.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        //Instance of api
        StockAPI _api = new StockAPI();
        StockRepository _repo = new StockRepository();

        //Index Page list of Orders

        public IActionResult Index(string SearchText = "", int pg = 1, int pageSize = 5)
        {
            PaginatedList<Purchase_Order> orderlist = _repo.GetAllPurchaseOrders(SearchText, pg, pageSize).Result;
            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(orderlist.TotalRecords, pg, pageSize);
            this.ViewBag.Pager = pager;

            return View(orderlist);
        }

        [HttpGet]
        [Route("PurchaseOrder/Details/{Purchase_Order_ID}")]
        public async Task<IActionResult> Details(int Purchase_Order_ID)
        {
            var purchaseorder = new Purchase_Order();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getpurchaseorderdetail/{Purchase_Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                purchaseorder = JsonConvert.DeserializeObject<Purchase_Order>(results);
            }
            return View(purchaseorder);
        }

        //[Route("PurchaseOrder/Update/{Purchase_Order_ID}")]
        //public async Task<ActionResult> Update(int? Purchase_Order_ID)
        //{
        //    if (Purchase_Order_ID == null || Purchase_Order_ID == 0)
        //    {
        //        return NotFound();
        //    }

        //    var item = new Items();
        //    HttpClient client = _api.Initial();
        //    HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getpurchaseorderbyid/{Purchase_Order_ID}");
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var results = res.Content.ReadAsStringAsync().Result;
        //        item = JsonConvert.DeserializeObject<Items>(results);
        //    }

        //    return View();
        //}


        [HttpGet]
        public ActionResult Create()
        {
            Purchase_Order purchaseOrder = new Purchase_Order();
            purchaseOrder.Purchase_Order_Detail.Add(new Purchase_Order_Detail() { Purchase_Order_ID = 1 });
            return View(purchaseOrder);
        }

        [HttpPost]
        public IActionResult Create(Purchase_Order purchaseorder)
        {
            purchaseorder.Order_Status = 1;
            purchaseorder.Order_Date = DateTime.Now;
            purchaseorder.Active = true;
            HttpClient client = _api.Initial();
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Purchase_Order>("api/StockAPP/addpurchaseorder", purchaseorder);
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
