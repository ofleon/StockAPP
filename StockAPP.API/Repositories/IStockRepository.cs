using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockAPP.API.Models;

namespace StockAPP.API.Repositories
{
    public interface IStockRepository
    {
        #region Items Repository
        //Get User by ID
        Task<Items> GetItembyID(int Item_ID);

        //Get User by Item Code
        Task<Items> GetItembyCode(string Item_Code);

        //Get User by Item FNSKU
        Task<Items> GetItembyFNSKU(string FNSKU);

        //Get All Items
        Task<IEnumerable<Items>> GetAllItems();

        //Get All Active Items
        Task<IEnumerable<Items>> GetAllActiveItems();

        //Get Item Description by Item ID
        Task<string> GetItemDescription(int Item_ID);

        //Insert New Item
        Task<int> AddItem(Items item);

        //Update Item
        Task UpdateItem(Items item);
        #endregion

        #region Purchase Order Repository
        //Get Purchase Order by Order ID
        Task<Purchase_Order> GetPurchaseOrderbyID(int Order_ID);

        //Get Purchase Order by Order Code
        Task<Purchase_Order> GetPurchaseOrderbyCode(string Order_Code);

        //Get All Purchase Order
        Task<IEnumerable<Purchase_Order>> GetAllPurchaseOrders();

        //Get Purchase Detail Order by Order ID
        Task<Purchase_Order> GetPurchaseOrderDetail(int Order_ID);

        //Insert New Purchase Order
        Task<int> AddPurchaseOrder(Purchase_Order purchaseorder);

        //Update Purchase Order
        Task UpdatePurchaseOrder(Purchase_Order purchaseorder);
        #endregion

        #region Purchase Order Detail Repository
        //Get All Purchase Orders Details
        Task<IEnumerable<Purchase_Order_Detail>> GetAllPurchaseOrderDetail();

        //Get Purchase Order Detail by Order ID
        Task<Purchase_Order_Detail> GetPurchaseOrderDetailbyID(int Order_ID);

        //Get Purchase Order Detail by Order Code
        Task<Purchase_Order_Detail> GetPurchaseOrderDetailbyCode(string Order_Code);

        //Insert New Purchase Order Detail
        Task<int> AddPurchaseOrderDetail(Purchase_Order_Detail purchaseorderdetail);

        //Update Purchase Order Detail
        Task UpdatePurchaseOrderDetail(Purchase_Order_Detail purchaseorderdetail);
        #endregion

        #region Sales Order Repository
        //Get Sales Order by Order ID
        Task<Sales_Order> GetSalesOrderbyID(int Order_ID);

        //Get Sales Order by Order Code
        Task<Sales_Order> GetSalesOrderbyCode(string Order_Code);

        //Get All Sales Order
        Task<IEnumerable<Sales_Order>> GetAllSalesOrders();

        //Get All Sales Order by Status (1=Open)
        Task<IEnumerable<Sales_Order>> GetAllSalesOpenOrders();

        //Get Sales Order Detail by Order ID
        Task<Sales_Order> GetSalesOrderDetail(int Order_ID);

        //Get Box Quantity by Sales Order
        Task<int> GetBoxQtybySalesOrder(int Order_ID);

        //Insert New Sales Order
        Task<int> AddSalesOrder(Sales_Order salesorder);

        //Update Sales Order
        Task UpdateSalesOrder(Sales_Order salesorder);
        #endregion

        #region Sales Order Detail Repository
        //Get All Sales Order Detail
        Task<IEnumerable<Sales_Order_Detail>> GetAllSalesOrderDetails();

        //Get Sales Order Detail by Order ID
        Task<Sales_Order_Detail> GetSalesOrderDetailbyID(int SalesOrder_ID);

        //Get Sales Order Detail by Order Code
        Task<Sales_Order_Detail> GetSalesOrderDetailbyCode(string Order_Code);

        //Get Items Quantity of Sales Order by Order Code
        Task<int> CountItemsSalesOrder(string Order_Code);

        //Insert New Sales Order Detail
        Task<int> AddSalesOrderDetail(Sales_Order_Detail salesorderdetail);

        //Update Sales Order Detail
        Task UpdateSalesOrderDetail(Sales_Order_Detail salesorderdetail);
        #endregion

        #region Sales Box Repository
        //Get All Sales Boxes
        Task<IEnumerable<Sales_Box>> GetAllSalesBoxes();

        //Get Sales Boxes by Sales Order ID
        Task<IEnumerable<Sales_Box>> GetSalesBoxbySalesOrderID(int Sales_Order_ID);

        //Get Sales Order by Order Code
        Task<IEnumerable<Sales_Box>> GetSalesBoxesbyOrderIDandUserID(int Sales_Order_ID, string User_ID);

        //Get Sales Box by Box ID and Sales Order ID
        Task<Sales_Box> GetSalesBoxbyBoxIDandSalesOrderID(int Sales_Order_ID, int Box_ID);

        //Get Sales Box by Box Code
        Task<Sales_Box> GetSalesBoxbyBoxCode(string Box_Code);

        //Insert New Sales Box
        Task<int> AddSalesBox(Sales_Box salesbox);

        //Update Sales Box
        Task UpdateSalesBox(Sales_Box salesbox);
        #endregion

        #region Sales Box Detail Repository
        //Get All Sales Box Detail
        Task<IEnumerable<Sales_Box_Detail>> GetAllSalesBoxDetail();

        //Get Sales Box Detail by Sales Order ID
        Task<IEnumerable<Sales_Box_Detail>> GetSalesBoxDetailbySalesBoxID(int Sales_Box_ID);

        //Get Items Quantity of Sales Box by Sales Box ID
        Task<int> CountItemsSalesBox(int Sales_Box_ID);

        //Get Items Quantity Packaged by Item ID
        Task<int> CountItemsSalesOrderPackaged(int Item_ID, int Sales_Order_ID);

        //Insert New Sales Box Detail
        Task<int> AddSalesBoxDetail(Sales_Box_Detail salesboxdetail);

        //Update Sales Box Detail
        Task UpdateSalesBoxDetail(Sales_Box_Detail salesboxdetail);
        #endregion
    }
}
