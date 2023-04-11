using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPP.API.Models
{
    public class RefreshToken
    {
        public int Token_ID { get; set; }
        public string User_ID { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
        public DateTime Expiry_Date { get; set; }

        [ForeignKey(nameof(User_ID))]
        public virtual IdentityUser User { get; set; }
    }
}
