using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockAPP.API.ExtendedModel;
using StockAPP.API.Models;
using StockAPP.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAPPController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockAPPController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        #region Items
        //Get All Items
        [Produces("application/json")]
        [HttpGet("getallitems")]
        public async Task<ActionResult<IEnumerable<Items>>> GetAllItems()
        {
            try
            {
                var items = await _stockRepository.GetAllItems();
                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get All Items
        [Produces("application/json")]
        [HttpGet("getallactiveitems")]
        public async Task<ActionResult<IEnumerable<Items>>> GetAllActiveItems()
        {
            try
            {
                var items = await _stockRepository.GetAllActiveItems();
                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Item by ID
        [Produces("application/json")]
        [HttpGet("getitembyid/{Item_ID}")]
        public async Task<ActionResult<Items>> GetItembyID(int Item_ID)
        {
            try
            {
                var item = await _stockRepository.GetItembyID(Item_ID);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Item by Code
        [Produces("application/json")]
        [HttpGet("getitembycode/{Item_Code}")]
        public async Task<ActionResult<Items>> GetItembyCode(string Item_Code)
        {
            try
            {
                var item = await _stockRepository.GetItembyCode(Item_Code);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Item by FNSKU
        [Produces("application/json")]
        [HttpGet("getitembyfnsku/{FNSKU}")]
        public async Task<ActionResult<Items>> GetItembyFNSKU(string FNSKU)
        {
            try
            {
                var item = await _stockRepository.GetItembyFNSKU(FNSKU);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Item by ID
        [Produces("application/json")]
        [HttpGet("getitemdescriptionbyid/{Item_ID}")]
        public async Task<ActionResult> GetItemDescription(int Item_ID)
        {
            try
            {
                var item = await _stockRepository.GetItemDescription(Item_ID);

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Insert New Item
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("additem")]
        public async Task<ActionResult> AddItem([FromBody] ItemModel newitem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Items item = new()
                    {
                        Item_Code = newitem.Item_Code,
                        FNSKU = newitem.FNSKU,
                        SKU = newitem.SKU,
                        Name = newitem.Name,
                        Description = newitem.Description,
                        Active = true
                    };
                    var Item_ID = await _stockRepository.AddItem(item);
                    if (Item_ID > 0)
                    {
                        return Ok(Item_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        //Update Item
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updateitem")]
        public async Task<ActionResult> UpdateItem([FromBody] Items item)
        {
            try
            {
                await _stockRepository.UpdateItem(item);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Purchase_Order
        //Get All Purchase Order
        [Produces("application/json")]
        [HttpGet("getallpurchaseorder")]
        public async Task<ActionResult<IEnumerable<Purchase_Order>>> GetAllPurchaseOrders()
        {
            try
            {
                var purchaseorder = await _stockRepository.GetAllPurchaseOrders();
                if (purchaseorder == null)
                {
                    return NotFound();
                }

                return Ok(purchaseorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Purchase Order by Order ID
        [Produces("application/json")]
        [HttpGet("getpurchaseorderbyid/{Order_ID}")]
        public async Task<ActionResult<Purchase_Order>> GetPurchaseOrderbyID(int Order_ID)
        {
            try
            {
                var purchaseorder = await _stockRepository.GetPurchaseOrderbyID(Order_ID);
                if (purchaseorder == null)
                    return NotFound();

                return Ok(purchaseorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Purchase Order by Order Code
        [Produces("application/json")]
        [HttpGet("getpurchaseorderbycode/{Order_Code}")]
        public async Task<ActionResult<Purchase_Order>> GetPurchaseOrderbyCode(string Order_Code)
        {
            try
            {
                var purchaseorder = await _stockRepository.GetPurchaseOrderbyCode(Order_Code);
                if (purchaseorder == null)
                    return NotFound();

                return Ok(purchaseorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Purchase Order Detail by Order ID
        [Produces("application/json")]
        [HttpGet("getpurchaseorderdetail/{Order_ID}")]
        public async Task<ActionResult<Purchase_Order>> GetPurchaseOrderDetail(int Order_ID)
        {
            try
            {
                var purchaseorder = await _stockRepository.GetPurchaseOrderDetail(Order_ID);
                if (purchaseorder == null)
                    return NotFound();

                return Ok(purchaseorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Insert New Purchase Order
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addpurchaseorder")]
        public async Task<ActionResult> AddPurchaseOrder([FromBody] Purchase_Order purchaseorder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Purchase_ID = await _stockRepository.AddPurchaseOrder(purchaseorder);
                    if (Purchase_ID > 0)
                    {
                        return Ok(Purchase_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatepurchaseorder")]
        public async Task<ActionResult> UpdatePurchaseOrder([FromBody] Purchase_Order purchaseorder)
        {
            try
            {
                await _stockRepository.UpdatePurchaseOrder(purchaseorder);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Purchase_Order_Detail
        //Get All Purchase Order
        [Produces("application/json")]
        [HttpGet("getallpurchaseorderdetail")]
        public async Task<ActionResult<IEnumerable<Purchase_Order_Detail>>> GetAllPurchaseOrderDetail()
        {
            try
            {
                var purchaseorderdetail = await _stockRepository.GetAllPurchaseOrderDetail();
                if (purchaseorderdetail == null)
                {
                    return NotFound();
                }

                return Ok(purchaseorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Purchase Order by Order ID
        [Produces("application/json")]
        [HttpGet("getpurchaseorderdetailbyid/{Order_ID}")]
        public async Task<ActionResult<Purchase_Order_Detail>> GetPurchaseOrderDetailbyID(int Order_ID)
        {
            try
            {
                var purchaseorderdetail = await _stockRepository.GetPurchaseOrderDetailbyID(Order_ID);
                if (purchaseorderdetail == null)
                    return NotFound();

                return Ok(purchaseorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Purchase Order by Order Code
        [Produces("application/json")]
        [HttpGet("getpurchaseorderdetailbycode/{Order_Code}")]
        public async Task<ActionResult<Purchase_Order_Detail>> GetPurchaseOrderDetailbyCode(string Order_Code)
        {
            try
            {
                var purchaseorderdetail = await _stockRepository.GetPurchaseOrderDetailbyCode(Order_Code);
                if (purchaseorderdetail == null)
                    return NotFound();

                return Ok(purchaseorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Insert New Purchase Order
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addpurchaseorderdetail")]
        public async Task<ActionResult> AddPurchaseOrderDetail([FromBody] Purchase_Order_Detail purchaseorderdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var PurchaseOrderDetail_ID = await _stockRepository.AddPurchaseOrderDetail(purchaseorderdetail);
                    if (PurchaseOrderDetail_ID > 0)
                    {
                        return Ok(PurchaseOrderDetail_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatepurchaseorderdetail")]
        public async Task<ActionResult> UpdatePurchaseOrderDetail([FromBody] Purchase_Order_Detail purchaseorderdetail)
        {
            try
            {
                await _stockRepository.UpdatePurchaseOrderDetail(purchaseorderdetail);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Sales Order Repository
        //Get All Sales Order
        [Produces("application/json")]
        [HttpGet("getallsalesorder")]
        public async Task<ActionResult<IEnumerable<Sales_Order>>> GetAllSalesOrders()
        {
            try
            {
                var salesorder = await _stockRepository.GetAllSalesOrders();
                if (salesorder == null)
                {
                    return NotFound();
                }

                return Ok(salesorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get All Sales Order by Status (1=Open)
        [Produces("application/json")]
        [HttpGet("getallopensalesorder")]
        public async Task<ActionResult<IEnumerable<Sales_Order>>> GetAllSalesOpenOrders()
        {
            try
            {
                var salesopenorders = await _stockRepository.GetAllSalesOpenOrders();
                if (salesopenorders == null)
                {
                    return NotFound();
                }

                return Ok(salesopenorders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order by Order ID
        [Produces("application/json")]
        [HttpGet("getsalesorderbyid/{Order_ID}")]
        public async Task<ActionResult<Sales_Order>> GetSalesOrderbyID(int Order_ID)
        {
            try
            {
                var salesorder = await _stockRepository.GetSalesOrderbyID(Order_ID);
                if (salesorder == null)
                    return NotFound();

                return Ok(salesorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order by Order Code
        [Produces("application/json")]
        [HttpGet("getsalesorderbycode/{Order_Code}")]
        public async Task<ActionResult<Sales_Order>> GetSalesOrderbyCode(string Order_Code)
        {
            try
            {
                var salesorder = await _stockRepository.GetSalesOrderbyCode(Order_Code);
                if (salesorder == null)
                    return NotFound();

                return Ok(salesorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order by Order ID
        [Produces("application/json")]
        [HttpGet("getsalesorderdetail/{Order_ID}")]
        public async Task<ActionResult<Sales_Order>> GetSalesOrderDetail(int Order_ID)
        {
            try
            {
                var salesorder = await _stockRepository.GetSalesOrderDetail(Order_ID);
                if (salesorder == null)
                    return NotFound();

                return Ok(salesorder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Items Quantity of Sales Box by Sales Box ID
        [Produces("application/json")]
        [HttpGet("getboxqtysalesorder/{Order_ID}")]
        public async Task<IActionResult> GetBoxQtybySalesOrder(int Order_ID)
        {
            try
            {
                var boxqty = await _stockRepository.GetBoxQtybySalesOrder(Order_ID);
                return Ok(boxqty);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Insert New Sales Order
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addsalesorder")]
        public async Task<ActionResult> AddSalesOrder(Sales_Order salesorder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SalesOrder_ID = await _stockRepository.AddSalesOrder(salesorder);
                    if (SalesOrder_ID > 0)
                    {
                        return Ok(SalesOrder_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        //Update Sales Order
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatesalesorder")]
        public async Task<ActionResult> UpdateSalesOrder(Sales_Order salesorder)
        {
            try
            {
                await _stockRepository.UpdateSalesOrder(salesorder);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Sales Order Detail Repository
        //Get All Sales Order Detail
        [Produces("application/json")]
        [HttpGet("getallsalesorderdetails")]
        public async Task<ActionResult<IEnumerable<Sales_Order_Detail>>> GetAllSalesOrderDetails()
        {
            try
            {
                var salesorderdetail = await _stockRepository.GetAllSalesOrderDetails();
                if (salesorderdetail == null)
                {
                    return NotFound();
                }

                return Ok(salesorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order Detail by Order ID
        [Produces("application/json")]
        [HttpGet("getsalesorderdetailbyid/{Order_ID}")]
        public async Task<ActionResult<Sales_Order_Detail>> GetSalesOrderDetailbyID(int SalesOrder_ID)
        {
            try
            {
                var salesorderdetail = await _stockRepository.GetSalesOrderDetailbyID(SalesOrder_ID);
                if (salesorderdetail == null)
                    return NotFound();

                return Ok(salesorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order Detail by Order Code
        [Produces("application/json")]
        [HttpGet("getsalesorderdetailbycode/{Order_Code}")]
        public async Task<ActionResult<Sales_Order_Detail>> GetSalesOrderDetailbyCode(string Order_Code)
        {
            try
            {
                var salesorderdetail = await _stockRepository.GetSalesOrderDetailbyCode(Order_Code);
                if (salesorderdetail == null)
                    return NotFound();

                return Ok(salesorderdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Items Quantity of Sales Order by Order Code
        [Produces("application/json")]
        [HttpGet("countitemssalesorderbycode/{Order_Code}")]
        public async Task<ActionResult> CountItemsSalesOrder(string Order_Code)
        {
            try
            {
                var itemsqty = await _stockRepository.CountItemsSalesOrder(Order_Code);
                return Ok(itemsqty);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Insert New Sales Order Detail
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addsalesorderdetail")]
        public async Task<ActionResult> AddSalesOrderDetail([FromBody] Sales_Order_Detail salesorderdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SalesOrderDetail_ID = await _stockRepository.AddSalesOrderDetail(salesorderdetail);
                    if (SalesOrderDetail_ID > 0)
                    {
                        return Ok(SalesOrderDetail_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        //Update Sales Order Detail
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatesalesorderdetail")]
        public async Task<ActionResult> UpdateSalesOrderDetail([FromBody] Sales_Order_Detail salesorderdetail)
        {
            try
            {
                await _stockRepository.UpdateSalesOrderDetail(salesorderdetail);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Sales Box Repository
        //Get All Sales Boxes
        [Produces("application/json")]
        [HttpGet("getallsalesboxes")]
        public async Task<ActionResult<IEnumerable<Sales_Box>>> GetAllSalesBoxes()
        {
            try
            {
                var salesboxes = await _stockRepository.GetAllSalesBoxes();
                if (salesboxes == null)
                {
                    return NotFound();
                }

                return Ok(salesboxes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Boxes by Sales Order ID
        [Produces("application/json")]
        [HttpGet("getsalesboxbysalesorderid/{Sales_Order_ID}")]
        public async Task<ActionResult<IEnumerable<Sales_Box>>> GetSalesBoxbySalesOrderID(int Sales_Order_ID)
        {
            try
            {
                var salesboxes = await _stockRepository.GetSalesBoxbySalesOrderID(Sales_Order_ID);
                if (salesboxes == null)
                {
                    return NotFound();
                }

                return Ok(salesboxes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Order by Order Code
        [Produces("application/json")]
        [HttpGet("getsalesboxbyorderidanduserid/{Sales_Order_ID}/{User_ID}")]
        public async Task<ActionResult<IEnumerable<Sales_Box>>> GetSalesBoxesbyOrderIDandUserID(int Sales_Order_ID, string User_ID)
        {
            try
            {
                var salesboxes = await _stockRepository.GetSalesBoxesbyOrderIDandUserID(Sales_Order_ID, User_ID);
                if (salesboxes == null)
                {
                    return NotFound();
                }

                return Ok(salesboxes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Box by Box ID and Sales Order ID
        [Produces("application/json")]
        [HttpGet("getsalesboxbyboxidandsalesorderid/{Sales_Order_ID}/{Box_ID}")]
        public async Task<ActionResult<Sales_Box>> GetSalesBoxbyBoxIDandSalesOrderID(int Sales_Order_ID, int Box_ID)
        {
            try
            {
                var salesbox = await _stockRepository.GetSalesBoxbyBoxIDandSalesOrderID(Sales_Order_ID, Box_ID);
                if (salesbox == null)
                    return NotFound();

                return Ok(salesbox);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Box by Box Code
        [Produces("application/json")]
        [HttpGet("getsalesboxbyboxcode/{Box_Code}")]
        public async Task<ActionResult<Sales_Box>> GetSalesBoxbyBoxCode(string Box_Code)
        {
            try
            {
                var salesbox = await _stockRepository.GetSalesBoxbyBoxCode(Box_Code);
                if (salesbox == null)
                    return NotFound();

                return Ok(salesbox);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Insert New Sales Box
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addsalesbox")]
        public async Task<ActionResult> AddSalesBox(Sales_Box salesbox)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SalesBox_ID = await _stockRepository.AddSalesBox(salesbox);
                    if (SalesBox_ID > 0)
                    {
                        return Ok(SalesBox_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        //Update Sales Box
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatesalesbox")]
        public async Task<ActionResult> UpdateSalesBox(Sales_Box salesbox)
        {
            try
            {
                await _stockRepository.UpdateSalesBox(salesbox);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion

        #region Sales Box Detail Repository
        //Get All Sales Box Detail
        [Produces("application/json")]
        [HttpGet("getallsalesboxesdetail")]
        public async Task<ActionResult<IEnumerable<Sales_Box_Detail>>> GetAllSalesBoxDetail()
        {
            try
            {
                var salesboxdetail = await _stockRepository.GetAllSalesBoxDetail();
                if (salesboxdetail == null)
                {
                    return NotFound();
                }

                return Ok(salesboxdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Sales Box Detail by Sales Order ID
        [Produces("application/json")]
        [HttpGet("getsalesboxdetailbysalesboxid/{Sales_Box_ID}")]
        public async Task<ActionResult<IEnumerable<Sales_Box_Detail>>> GetSalesBoxDetailbySalesBoxID(int Sales_Box_ID)
        {
            try
            {
                var salesboxdetail = await _stockRepository.GetSalesBoxDetailbySalesBoxID(Sales_Box_ID);
                if (salesboxdetail == null)
                {
                    return NotFound();
                }

                return Ok(salesboxdetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Items Quantity of Sales Box by Sales Box ID
        [Produces("application/json")]
        [HttpGet("getcountitemssalesbox/{Sales_Box_ID}")]
        public async Task<ActionResult> CountItemsSalesBox(int Sales_Box_ID)
        {
            try
            {
                var itemsqty = await _stockRepository.CountItemsSalesBox(Sales_Box_ID);
                return Ok(itemsqty);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Get Items Quantity Packaged by Item ID and Sales Order ID
        [Produces("application/json")]
        [HttpGet("getcountitemssalesorderpackaged/{Item_ID}/{Sales_Order_ID}")]
        public async Task<ActionResult> CountItemsSalesOrderPackaged(int Item_ID, int Sales_Order_ID)
        {
            try
            {
                var itemsqty = await _stockRepository.CountItemsSalesOrderPackaged(Item_ID, Sales_Order_ID);
                return Ok(itemsqty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Insert New Sales Box Detail
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("addsalesboxdetail")]
        public async Task<ActionResult> AddSalesBoxDetail(Sales_Box_Detail salesboxdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var SalesBoxDetail_ID = await _stockRepository.AddSalesBoxDetail(salesboxdetail);
                    if (SalesBoxDetail_ID > 0)
                    {
                        return Ok(SalesBoxDetail_ID);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        //Update Sales Box Detail
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("updatesalesboxdetail")]
        public async Task<ActionResult> UpdateSalesBoxDetail(Sales_Box_Detail salesboxdetail)
        {
            try
            {
                await _stockRepository.UpdateSalesBoxDetail(salesboxdetail);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }
        #endregion
    }
}
