using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class Sales_Order
    {
        public Sales_Order()
        {
            
        }

        public int Sales_Order_ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Order_Code { get; set; }

        [Required(ErrorMessage = "Order Status is required")]
        public byte Order_Status { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        public DateTime Order_Date { get; set; }

        [Required(ErrorMessage = "Order Active is required")]
        public bool Active { get; set; }
        public DateTime Modified_Date { get; set; }

        public virtual IList<Sales_Box> Sales_Box { get; set; }
        public virtual IList<Sales_Order_Detail> Sales_Order_Detail { get; set; }
    }
}
