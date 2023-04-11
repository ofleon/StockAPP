using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class Item_Inventory
    {
        public int Item_Inventory_ID { get; set; }


        [ForeignKey("Items")]
        [Required(ErrorMessage = "Item ID is required")]
        public int Item_ID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public DateTime Modified_Date { get; set; }

        public virtual Items Items { get; set; }
    }
}
