using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class Items
    {
        public Items()
        {
            
        }

        public int Item_ID { get; set; }

        [Required(ErrorMessage = "Item Code is required")]
        public string Item_Code { get; set; }

        [Required(ErrorMessage ="FNSKU is required")]
        public string FNSKU { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Active is required")]
        public bool Active { get; set; }

        public DateTime Modified_Date { get; set; }


        public virtual IList<Item_Inventory> Item_Inventory { get; set; }
        public virtual IList<Purchase_Order_Detail> Purchase_Order_Detail { get; set; }
        public virtual IList<Sales_Box_Detail> Sales_Box_Detail { get; set; }
        public virtual IList<Sales_Order_Detail> Sales_Order_Detail { get; set; }
    }
}
