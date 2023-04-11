using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.Web.Models
{
    public class Sales_Box_Detail
    {
        public int Sales_Box_Detail_ID { get; set; }
        public int Sales_Box_ID { get; set; }

        [ForeignKey("Items")]
        [Required(ErrorMessage = "Item ID is required")]
        public int Item_ID { get; set; }

        [Required(ErrorMessage = "Item Quantity is required")]
        public int Item_Qty { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Modified_Date { get; set; }

        public virtual Items Items { get; set; }

        [ForeignKey(nameof(Sales_Box_ID))]
        public virtual Sales_Box Sales_Box { get; set; }
    }
}
