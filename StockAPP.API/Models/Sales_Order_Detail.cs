using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class Sales_Order_Detail
    {
        public int Sales_OrderDetail_ID { get; set; }
        public int Sales_Order_ID { get; set; }

        [ForeignKey("Items")]
        [Required(ErrorMessage = "Item ID is required")]
        public int Item_ID { get; set; }

        [Required(ErrorMessage = "Item Quantity is required")]
        public int Item_Qty { get; set; }
        public DateTime Modified_Date { get; set; }

        public virtual Items Items { get; set; }

        [ForeignKey(nameof(Sales_Order_ID))]
        public virtual Sales_Order Sales_Order { get; set; }
    }
}
