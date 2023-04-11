using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace StockAPP.API.Models
{
    public interface IStockAPPDbContext
    {
        DbSet<Purchase_Order> Purchase_Order { get; set; }
        DbSet<Purchase_Order_Detail> Purchase_Order_Detail { get; set; }
        DbSet<Items> Items { get; set; }
        DbSet<Item_Inventory> Item_Inventory { get; set; }
        DbSet<Sales_Order> Sales_Order { get; set; }
        DbSet<Sales_Order_Detail> Sales_Order_Detail { get; set; }
        DbSet<Sales_Box> Sales_Box { get; set; }
        DbSet<Sales_Box_Detail> Sales_Box_Detail { get; set; }
        DbSet<RefreshToken> RefreshToken { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
