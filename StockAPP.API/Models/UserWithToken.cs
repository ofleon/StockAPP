using StockAPP.API.ExtendedModel;
using System;

namespace StockAPP.API.Models
{
    public class UserWithToken : LoginModel
    {
        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(LoginModel user)
        {
            Username = user.Username;
        }
    }
}
