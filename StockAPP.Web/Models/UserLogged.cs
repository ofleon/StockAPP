using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.Web.Models
{
    public class UserLogged
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
    }
}
