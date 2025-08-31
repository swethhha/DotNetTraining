using Microsoft.EntityFrameworkCore;
using ShopTrackPro.Core.Entities;  // ✅ bring in your domain entities

namespace ShopTrackPro.Infrastructure.Data
{
    public partial class ShopTrackProContext : DbContext
    {
        public ShopTrackProContext()
        {
        }

        public ShopTrackProContext(DbContextOptions<ShopTrackProContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=ShopTrackProDB;User Id=sa;Password=Swetha@06112004;TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // same mapping code as before...
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
