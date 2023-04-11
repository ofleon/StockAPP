using Newtonsoft.Json;
using StockApp.Web.Helpers;
using StockAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockAPP.Web.Repository
{
    public class StockRepository : IStockRepository
    {
        StockAPI _api = new StockAPI();

        #region Items Repository

        //Get All Items
        public async Task<PaginatedList<Items>> GetAllItems(string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Items> items = new List<Items>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/StockAPP/getallitems");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<Items>>(results);
            }

            if (SearchText != "" && SearchText != null)
            {
                items = items.Where(n => n.Item_Code.Contains(SearchText) || n.FNSKU.Contains(SearchText) || n.SKU.Contains(SearchText) || n.Name.Contains(SearchText)).ToList();
            }
            else
                items = items.ToList();

            PaginatedList<Items> retItems = new PaginatedList<Items>(items, pageIndex, pageSize);

            return retItems;
        }


        //Get Item Description by Item ID
        public async Task<string> GetItemDescription(int Item_ID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getitemdescriptionbyid/{Item_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                string Descrip = JsonConvert.DeserializeObject<string>(results);

                return Descrip;
            }

            return null;
        }

        //Get All Active Items
        public async Task<List<Items>> GetAllActiveItems()
        {
            List<Items> items = new List<Items>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/StockAPP/getallactiveitems");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<Items>>(results);
            }

            return items;
        }
        #endregion

        #region Sales Order Repository
        //Get All Sales Order
        public async Task<PaginatedList<Sales_Order>> GetAllSalesOrders(string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Sales_Order> salesorder = new List<Sales_Order>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/StockAPP/getallsalesorder");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                salesorder = JsonConvert.DeserializeObject<List<Sales_Order>>(results);
            }

            if (SearchText != "" && SearchText != null)
            {
                salesorder = salesorder.Where(n => n.Order_Code.Contains(SearchText)).ToList();
            }
            else
                salesorder = salesorder.ToList();

            PaginatedList<Sales_Order> retSalesOrder = new PaginatedList<Sales_Order>(salesorder, pageIndex, pageSize);

            return retSalesOrder;
        }

        //Get Box Quantity by Sales Order
        public async Task<int> GetBoxQtybySalesOrder(int Order_ID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getboxqtysalesorder/{Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                int qty = JsonConvert.DeserializeObject<int>(results);

                return qty;
            }

            return 0;
        }
        #endregion

        #region Sales Box Repository
        //Get Sales Boxes by Sales Order ID
        public async Task<PaginatedList<Sales_Box>> GetSalesBoxbySalesOrderID(int Sales_Order_ID, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Sales_Box> salesboxes = new List<Sales_Box>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getsalesboxbysalesorderid/{Sales_Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                salesboxes = JsonConvert.DeserializeObject<List<Sales_Box>>(results);
            }

            if (SearchText != "" && SearchText != null)
            {
                salesboxes = salesboxes.Where(n => n.Box_Code.Contains(SearchText)).ToList();
            }
            else
                salesboxes = salesboxes.ToList();

            PaginatedList<Sales_Box> retSalesBox = new PaginatedList<Sales_Box>(salesboxes, pageIndex, pageSize);

            return retSalesBox;
        }

        //Get Items Quantity of Sales Box by Sales Box ID
        public async Task<int> CountItemsSalesBox(int Sales_Box_ID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getcountitemssalesbox/{Sales_Box_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                int qty = JsonConvert.DeserializeObject<int>(results);

                return qty;
            }

            return 0;
        }

        //Get Items Quantity Packaged by Item ID and Sales Order ID
        public async Task<int> CountItemsSalesOrderPackaged(int Item_ID, int Sales_Order_ID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/StockAPP/getcountitemssalesorderpackaged/{Item_ID}/{Sales_Order_ID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                int sumqty = JsonConvert.DeserializeObject<int>(results);

                return sumqty;
            }

            return 0;
        }
        #endregion

        #region Purchase Order Repository
        //Get All Purchase Order
        public async Task<PaginatedList<Purchase_Order>> GetAllPurchaseOrders(string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Purchase_Order> purchaseorder = new List<Purchase_Order>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/StockAPP/getallpurchaseorder");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                purchaseorder = JsonConvert.DeserializeObject<List<Purchase_Order>>(results);
            }

            if (SearchText != "" && SearchText != null)
            {
                purchaseorder = purchaseorder.Where(n => n.Order_Code.Contains(SearchText)).ToList();
            }
            else
                purchaseorder = purchaseorder.ToList();

            PaginatedList<Purchase_Order> retPurchaseOrder = new PaginatedList<Purchase_Order>(purchaseorder, pageIndex, pageSize);

            return retPurchaseOrder;
        }
        #endregion
    }
}
