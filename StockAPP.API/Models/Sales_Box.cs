using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class Sales_Box
    {
        public Sales_Box()
        {
            
        }

        public int Sales_Box_ID { get; set; }
        public int Sales_Order_ID { get; set; }

        [Required(ErrorMessage = "Box Number is required")]
        public int Box_Number { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Box_Code { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string User_ID { get; set; }

        [Required(ErrorMessage = "Box Status is required")]
        public byte Box_Status { get; set; }
        public DateTime Modified_Date { get; set; }

        public virtual IList<Sales_Box_Detail> Sales_Box_Detail { get; set; }

        [ForeignKey(nameof(Sales_Order_ID))]
        public virtual Sales_Order Sales_Order { get; set; }

        [ForeignKey(nameof(User_ID))]
        public virtual IdentityUser User { get; set; }
    }
}
