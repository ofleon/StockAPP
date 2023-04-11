using System;
using System.Net.Http;

namespace StockApp.Web.Helpers
{
    public class StockAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44369/");
            return client;
        }
    }
}
