using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockAPP.API.Models;

namespace StockAPP.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IStockAPPDbContext _context;
        public StockRepository(IStockAPPDbContext context)
        {
            _context = context;
        }

        #region Items Repository
        //Get All Items
        public async Task<IEnumerable<Items>> GetAllItems()
        {
            if (_context != null)
            {
                return await _context.Items.ToListAsync();
            }

            return null;
        }

        //Get All Active Items
        public async Task<IEnumerable<Items>> GetAllActiveItems()
        {
            if (_context != null)
            {
                return await _context.Items.Where(m => m.Active == true).ToListAsync();
            }

            return null;
        }

        //Get Item by ID
        public async Task<Items> GetItembyID(int Item_ID)
        {
            if (_context != null)
            {
                return await _context.Items.FindAsync(Item_ID);
            }

            return null;
        }

        //Get Item by Item Code
        public async Task<Items> GetItembyCode(string Item_Code)
        {
            if (_context != null)
            {
                return await _context.Items.FindAsync(Item_Code);
            }

            return null;
        }

        //Get Item by Item FNSKU
        public async Task<Items> GetItembyFNSKU(string FNSKU)
        {
            if (_context != null)
            {
                return await _context.Items.FindAsync(FNSKU);
            }

            return null;
        }

        //Get Item Description by Item ID
        public async Task<string> GetItemDescription(int Item_ID)
        {
            if (_context != null)
            {
                var item = await _context.Items.FindAsync(Item_ID);
                return item.Description;
            }

            return null;
        }

        //Insert New Item
        public async Task<int> AddItem(Items item)
        {
            if (_context != null)
            {
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();

                return item.Item_ID;
            }

            return 0;
        }

        //Update Item
        public async Task UpdateItem(Items item)
        {
            if (_context != null)
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Purchase Order Repository
        //Get All Purchase Order
        public async Task<IEnumerable<Purchase_Order>> GetAllPurchaseOrders()
        {
            if (_context != null)
            {
                return await _context.Purchase_Order.ToListAsync();
            }

            return null;
        }

        //Get Purchase Order by Order ID
        public async Task<Purchase_Order> GetPurchaseOrderbyID(int Order_ID)
        {
            if (_context != null)
            {
                return await _context.Purchase_Order.FindAsync(Order_ID);
            }

            return null;
        }

        //Get Purchase Order by Order Code
        public async Task<Purchase_Order> GetPurchaseOrderbyCode(string Order_Code)
        {
            if (_context != null)
            {
                return await _context.Purchase_Order.FindAsync(Order_Code);
            }

            return null;
        }

        //Get Purchase Detail Order by Order ID
        public async Task<Purchase_Order> GetPurchaseOrderDetail(int Order_ID)
        {
            if (_context != null)
            {
                return await _context.Purchase_Order.Include(x => x.Purchase_Order_Detail).SingleOrDefaultAsync(m => m.Purchase_Order_ID == Order_ID);
            }

            return null;
        }

        //Insert New Purchase Order
        public async Task<int> AddPurchaseOrder(Purchase_Order purchaseorder)
        {
            if (_context != null)
            {
                

                await _context.Purchase_Order.AddAsync(purchaseorder);
                await _context.SaveChangesAsync();

                return purchaseorder.Purchase_Order_ID;
            }

            return 0;
        }

        //Update Purchase Order
        public async Task UpdatePurchaseOrder(Purchase_Order purchaseorder)
        {
            if (_context != null)
            {
                _context.Purchase_Order.Update(purchaseorder);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Purchase Order Detail Repository
        //Get All Purchase Orders Detail
        public async Task<IEnumerable<Purchase_Order_Detail>> GetAllPurchaseOrderDetail()
        {
            if (_context != null)
            {
                return await _context.Purchase_Order_Detail.ToListAsync();
            }

            return null;
        }

        //Get Purchase Order Detail by Order ID
        public async Task<Purchase_Order_Detail> GetPurchaseOrderDetailbyID(int Order_ID)
        {
            if (_context != null)
            {
                return await _context.Purchase_Order_Detail.FindAsync(Order_ID);
            }

            return null;
        }

        //Get Purchase Order Detail by Order Code
        public async Task<Purchase_Order_Detail> GetPurchaseOrderDetailbyCode(string Order_Code)
        {
            if (_context != null)
            {
                var Order_ID = _context.Purchase_Order.SingleOrDefault(purchaseorderdetail => purchaseorderdetail.Order_Code == Order_Code).Purchase_Order_ID;
                return await _context.Purchase_Order_Detail.FindAsync(Order_ID);
            }

            return null;
        }

        //Insert New Purchase Order
        public async Task<int> AddPurchaseOrderDetail(Purchase_Order_Detail purchaseorderdetail)
        {
            if (_context != null)
            {
                await _context.Purchase_Order_Detail.AddAsync(purchaseorderdetail);
                await _context.SaveChangesAsync();

                return purchaseorderdetail.Purchase_OrderDetail_ID;
            }

            return 0;
        }

        //Update Purchase Order
        public async Task UpdatePurchaseOrderDetail(Purchase_Order_Detail purchaseorderdetail)
        {
            if (_context != null)
            {
                _context.Purchase_Order_Detail.Update(purchaseorderdetail);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Sales Order Repository
        //Get All Sales Order
        public async Task<IEnumerable<Sales_Order>> GetAllSalesOrders()
        {
            if (_context != null)
            {
                return await _context.Sales_Order.ToListAsync();
            }

            return null;
        }

        //Get All Sales Order by Status (1=Open)
        public async Task<IEnumerable<Sales_Order>> GetAllSalesOpenOrders()
        {
            if (_context != null)
            {
                return await _context.Sales_Order.Where(s => s.Order_Status == 1).ToListAsync();
            }

            return null;
        }

        //Get Sales Order by Order ID
        public async Task<Sales_Order> GetSalesOrderbyID(int Order_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Order.FindAsync(Order_ID);
            }

            return null;
        }

        //Get Sales Order by Order Code
        public async Task<Sales_Order> GetSalesOrderbyCode(string Order_Code)
        {
            if (_context != null)
            {
                return await _context.Sales_Order.FindAsync(Order_Code);
            }

            return null;
        }

        //Get Sales Order Detail by Order ID
        public async Task<Sales_Order> GetSalesOrderDetail(int Order_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Order.Include(x => x.Sales_Order_Detail).SingleOrDefaultAsync(m => m.Sales_Order_ID == Order_ID);
            }

            return null;
        }

        //Get Box Quantity by Sales Order
        public async Task<int> GetBoxQtybySalesOrder(int Order_ID)
        {
            if (_context != null)
            {
                return await (from x in _context.Sales_Box where x.Sales_Order_ID == Order_ID select x).CountAsync();
            }

            return 0;
        }

        //Insert New Sales Order
        public async Task<int> AddSalesOrder(Sales_Order salesorder)
        {
            if (_context != null)
            {
                await _context.Sales_Order.AddAsync(salesorder);
                await _context.SaveChangesAsync();

                return salesorder.Sales_Order_ID;
            }

            return 0;
        }

        //Update Sales Order
        public async Task UpdateSalesOrder(Sales_Order salesorder)
        {
            if (_context != null)
            {
                _context.Sales_Order.Update(salesorder);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Sales Order Detail Repository
        //Get All Sales Order Detail
        public async Task<IEnumerable<Sales_Order_Detail>> GetAllSalesOrderDetails()
        {
            if (_context != null)
            {
                return await _context.Sales_Order_Detail.ToListAsync();
            }

            return null;
        }

        //Get Sales Order Detail by Order ID
        public async Task<Sales_Order_Detail> GetSalesOrderDetailbyID(int SalesOrder_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Order_Detail.FindAsync(SalesOrder_ID);
            }

            return null;
        }

        //Get Sales Order Detail by Order Code
        public async Task<Sales_Order_Detail> GetSalesOrderDetailbyCode(string Order_Code)
        {
            if (_context != null)
            {
                var SalesOrder_ID = _context.Sales_Order.SingleOrDefault(salesorderdetail => salesorderdetail.Order_Code == Order_Code).Sales_Order_ID;
                return await _context.Sales_Order_Detail.FindAsync(SalesOrder_ID);
            }

            return null;
        }

        //Get Items Quantity of Sales Order by Order Code
        public async Task<int> CountItemsSalesOrder(string Order_Code)
        {
            if (_context != null)
            {
                var SalesOrder_ID = _context.Sales_Order.SingleOrDefault(salesorderdetail => salesorderdetail.Order_Code == Order_Code).Sales_Order_ID;
                return await (from x in _context.Sales_Order_Detail where x.Sales_Order_ID == SalesOrder_ID select x).CountAsync();
            }

            return 0;
        }

        //Insert New Sales Order Detail
        public async Task<int> AddSalesOrderDetail(Sales_Order_Detail salesorderdetail)
        {
            if (_context != null)
            {
                await _context.Sales_Order_Detail.AddAsync(salesorderdetail);
                await _context.SaveChangesAsync();

                return salesorderdetail.Sales_OrderDetail_ID;
            }

            return 0;
        }

        //Update Sales Order Detail
        public async Task UpdateSalesOrderDetail(Sales_Order_Detail salesorderdetail)
        {
            if (_context != null)
            {
                _context.Sales_Order_Detail.Update(salesorderdetail);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Sales Box Repository
        //Get All Sales Boxes
        public async Task<IEnumerable<Sales_Box>> GetAllSalesBoxes()
        {
            if (_context != null)
            {
                return await _context.Sales_Box.ToListAsync();
            }

            return null;
        }

        //Get Sales Boxes by Sales Order ID
        public async Task<IEnumerable<Sales_Box>> GetSalesBoxbySalesOrderID(int Sales_Order_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Box.Where(b => b.Sales_Order_ID == Sales_Order_ID).ToListAsync();
            }

            return null;
        }

        //Get Sales Order by Order Code
        public async Task<IEnumerable<Sales_Box>> GetSalesBoxesbyOrderIDandUserID(int Sales_Order_ID, string User_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Box
                    .Where(b => b.Sales_Order_ID == Sales_Order_ID)
                    .Where(c => c.User_ID == User_ID)
                    .ToListAsync();
            }

            return null;
        }

        //Get Sales Box by Box ID and Sales Order ID
        public async Task<Sales_Box> GetSalesBoxbyBoxIDandSalesOrderID(int Sales_Order_ID, int Box_Number)
        {
            if (_context != null)
            {
                return await _context.Sales_Box
                    .Where(s => s.Sales_Order_ID == Sales_Order_ID)
                    .Where(b => b.Box_Number == Box_Number)
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        //Get Sales Box by Box Code
        public async Task<Sales_Box> GetSalesBoxbyBoxCode(string Box_Code)
        {
            if (_context != null)
            {
                return await _context.Sales_Box.FindAsync(Box_Code);
            }

            return null;
        }

        //Insert New Sales Box
        public async Task<int> AddSalesBox(Sales_Box salesbox)
        {
            if (_context != null)
            {
                await _context.Sales_Box.AddAsync(salesbox);
                await _context.SaveChangesAsync();

                return salesbox.Sales_Box_ID;
            }

            return 0;
        }

        //Update Sales Box
        public async Task UpdateSalesBox(Sales_Box salesbox)
        {
            if (_context != null)
            {
                _context.Sales_Box.Update(salesbox);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Sales Box Detail Repository
        //Get All Sales Box Detail
        public async Task<IEnumerable<Sales_Box_Detail>> GetAllSalesBoxDetail()
        {
            if (_context != null)
            {
                return await _context.Sales_Box_Detail.ToListAsync();
            }

            return null;
        }

        //Get Sales Box Detail by Sales Order ID
        public async Task<IEnumerable<Sales_Box_Detail>> GetSalesBoxDetailbySalesBoxID(int Sales_Box_ID)
        {
            if (_context != null)
            {
                return await _context.Sales_Box_Detail.Where(b => b.Sales_Box_ID == Sales_Box_ID).ToListAsync();
            }

            return null;
        }

        //Get Items Quantity of Sales Box by Sales Box ID
        public async Task<int> CountItemsSalesBox(int Sales_Box_ID)
        {
            if (_context != null)
            {
                return await (from x in _context.Sales_Box_Detail where x.Sales_Box_ID == Sales_Box_ID select x).CountAsync();
            }

            return 0;
        }

        //Get Items Quantity Packaged by Item ID and Sales Order ID
        public async Task<int> CountItemsSalesOrderPackaged(int Item_ID, int Sales_Order_ID)
        {
            if (_context != null)
            {
                //return await (from x in _context.Sales_Box_Detail where x. == Sales_Box_ID select x).CountAsync();

              return await  (from e in _context.Sales_Box_Detail
                         join d in _context.Sales_Box on e.Sales_Box_ID equals d.Sales_Box_ID into table1
                         from d in table1.ToList()
                        join i in _context.Sales_Order on d.Sales_Order_ID equals i.Sales_Order_ID into table2
                        from i in table2.ToList()
                         where ((e.Item_ID == Item_ID) && (d.Sales_Order_ID==Sales_Order_ID))
                         select e.Item_Qty).SumAsync();
            }

            return 0;
        }

        //Insert New Sales Box Detail
        public async Task<int> AddSalesBoxDetail(Sales_Box_Detail salesboxdetail)
        {
            if (_context != null)
            {
                await _context.Sales_Box_Detail.AddAsync(salesboxdetail);
                await _context.SaveChangesAsync();

                return salesboxdetail.Sales_Box_Detail_ID;
            }

            return 0;
        }

        //Update Sales Box Detail
        public async Task UpdateSalesBoxDetail(Sales_Box_Detail salesboxdetail)
        {
            if (_context != null)
            {
                _context.Sales_Box_Detail.Update(salesboxdetail);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
