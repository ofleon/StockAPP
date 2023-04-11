using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ItemController : Controller
    {
        //Instance of api
        StockAPI _api = new StockAPI();
        StockRepository _repo = new StockRepository();

        //Index Page List of Items
        public IActionResult Index(string SearchText = "", int pg = 1, int pageSize = 5)
        {
            PaginatedList<Items> itemslist = _repo.GetAllItems(SearchText, pg, pageSize).Result;
            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(itemslist.TotalRecords, pg, pageSize);
            this.ViewBag.Pager = pager;

            return View(itemslist);
        }

        [HttpGet]
        [Route("Item/Details/{Item_ID}")]
        public async Task<IActionResult> Details(int Item_ID)
        {
            var item = new Items();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getitembyid/{Item_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Items>(results);
            }
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Items item)
        {
            HttpClient client = _api.Initial();
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Items>("api/StockAPP/additem", item);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Route("Item/Update/{Item_ID}")]
        public async Task<ActionResult> Update(int? Item_ID)
        {
            if (Item_ID == null || Item_ID == 0)
            {
                return NotFound();
            }

            var item = new Items();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getitembyid/{Item_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Items>(results);
            }

            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Item/Update/{Item_ID}")]
        public IActionResult Update(Items item)
        {
            HttpClient client = _api.Initial();
            //HTTP POST
            item.Modified_Date = DateTime.Now;
            var postTask = client.PostAsJsonAsync<Items>("api/StockAPP/updateitem", item);
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
