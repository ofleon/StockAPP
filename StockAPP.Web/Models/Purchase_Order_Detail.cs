using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.Web.Models
{
    public class Purchase_Order_Detail
    {
        public int Purchase_OrderDetail_ID { get; set; }
        public int Purchase_Order_ID { get; set; }

        [ForeignKey("Items")]
        [Required(ErrorMessage = "Item ID is required")]
        public int Item_ID { get; set; }

        [Required(ErrorMessage = "Item Quantity is required")]
        public int Item_Qty { get; set; }
        public int Received_Qty { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Date_Received { get; set; }

        [Required(ErrorMessage = "Item Status is required")]
        public byte Item_Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Modified_Date { get; set; }

        public virtual Items Items { get; set; }

        [ForeignKey(nameof(Purchase_Order_ID))]
        public virtual Purchase_Order Purchase_Order { get; set; }
    }
}
