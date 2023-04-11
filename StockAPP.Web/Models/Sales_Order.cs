using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAPP.Web.Models
{
    public class Sales_Order
    {
        public Sales_Order()
        {

        }

        public int Sales_Order_ID { get; set; }
        public string Order_Code { get; set; }

        [Required(ErrorMessage = "Order Status is required")]
        public byte Order_Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Order Date is required")]
        public DateTime Order_Date { get; set; }

        [Required(ErrorMessage = "Order Active is required")]
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Modified_Date { get; set; }

        public virtual IList<Sales_Box> Sales_Box { get; set; }
        public virtual IList<Sales_Order_Detail> Sales_Order_Detail { get; set; } = new List<Sales_Order_Detail>();
    }
}
