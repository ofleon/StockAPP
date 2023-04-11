using StockAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.Web.Repository
{
    public interface IStockRepository
    {
        #region Items Repository
        //Get All Items
        Task<PaginatedList<Items>> GetAllItems(string SearchText = "", int pageIndex = 1, int pageSize = 5);

        //Get Item Description by Item ID
        Task<string> GetItemDescription(int Item_ID);

        //Get All Active Items
        Task<List<Items>> GetAllActiveItems();
        #endregion

        #region Sales Order Repository
        //Get All Sales Order
        Task<PaginatedList<Sales_Order>> GetAllSalesOrders(string SearchText = "", int pageIndex = 1, int pageSize = 5);

        //Get Box Quantity by Sales Order
        Task<int> GetBoxQtybySalesOrder(int Order_ID);
        #endregion

        #region Sales Box Repository
        //Get Sales Boxes by Sales Order ID
        Task<PaginatedList<Sales_Box>> GetSalesBoxbySalesOrderID(int Sales_Order_ID, string SearchText = "", int pageIndex = 1, int pageSize = 5);

        //Get Items Quantity of Sales Box by Sales Box ID
        Task<int> CountItemsSalesBox(int Sales_Box_ID);

        //Get Items Quantity Packaged by Item ID
        Task<int> CountItemsSalesOrderPackaged(int Item_ID, int Sales_Order_ID);
        #endregion

        #region Purchase Order Repository
        //Get All Purchase Order
        Task<PaginatedList<Purchase_Order>> GetAllPurchaseOrders(string SearchText = "", int pageIndex = 1, int pageSize = 5);
        #endregion
    }
}
