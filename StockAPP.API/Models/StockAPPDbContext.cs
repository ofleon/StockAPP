using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StockAPP.API.Models
{
    public class StockAPPDbContext : IdentityDbContext, IStockAPPDbContext
    {
        public StockAPPDbContext(DbContextOptions<StockAPPDbContext> options)
            : base(options)
        {
        }

        public DbSet<Purchase_Order> Purchase_Order { get; set; }
        public DbSet<Purchase_Order_Detail> Purchase_Order_Detail { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Item_Inventory> Item_Inventory { get; set; }
        public DbSet<Sales_Order> Sales_Order { get; set; }
        public DbSet<Sales_Order_Detail> Sales_Order_Detail { get; set; }
        public DbSet<Sales_Box> Sales_Box { get; set; }
        public DbSet<Sales_Box_Detail> Sales_Box_Detail { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item_Inventory>(entity =>
            {
                entity.HasKey(e => e.Item_Inventory_ID);
                entity.ToTable("Item_Inventory");
            });

            modelBuilder.Entity<Item_Inventory>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Item_Inventory>(entity =>
            {
                entity.HasCheckConstraint("CK_Item_Inventory_Qty", "[Quantity] > 0");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Item_ID);
                entity.ToTable("Items");
            });

            modelBuilder.Entity<Items>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Purchase_Order>(entity =>
            {
                entity.HasKey(e => e.Purchase_Order_ID);
                entity.ToTable("Purchase_Order");
            });

            modelBuilder.Entity<Purchase_Order>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Purchase_Order_Detail>(entity =>
            {
                entity.HasKey(e => e.Purchase_OrderDetail_ID);
                entity.ToTable("Purchase_Order_Detail");
            });

            modelBuilder.Entity<Purchase_Order_Detail>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Purchase_Order_Detail>(entity =>
            {
                entity.HasCheckConstraint("CK_Purchase_Order_Detail_ItemQty", "[Item_Qty] > 0");
            });

            modelBuilder.Entity<Sales_Order>(entity =>
            {
                entity.HasKey(e => e.Sales_Order_ID);
                entity.ToTable("Sales_Order");
            });

            modelBuilder.Entity<Sales_Order>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sales_Order_Detail>(entity =>
            {
                entity.HasKey(e => e.Sales_OrderDetail_ID);
                entity.ToTable("Sales_Order_Detail");
            });

            modelBuilder.Entity<Sales_Order_Detail>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sales_Order_Detail>(entity =>
            {
                entity.HasCheckConstraint("CK_Sales_Order_Detail_ItemQty", "[Item_Qty] > 0");
            });

            modelBuilder.Entity<Sales_Box>(entity =>
            {
                entity.HasKey(e => e.Sales_Box_ID);
                entity.ToTable("Sales_Box");
            });

            modelBuilder.Entity<Sales_Box>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sales_Box_Detail>(entity =>
            {
                entity.HasKey(e => e.Sales_Box_Detail_ID);
                entity.ToTable("Sales_Box_Detail");
            });

            modelBuilder.Entity<Sales_Box_Detail>()
            .Property(b => b.Modified_Date)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sales_Box_Detail>(entity =>
            {
                entity.HasCheckConstraint("CK_Sales_Box_Detail_ItemQty", "[Item_Qty] > 0");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Token_ID);
                entity.ToTable("RefreshToken");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
