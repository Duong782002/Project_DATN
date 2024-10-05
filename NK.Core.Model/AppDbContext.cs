using Microsoft.EntityFrameworkCore;
using NK.Core.Model.Entities;

namespace NK.Core.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<WishLists>()
            .HasKey(w => new { w.ProductsId, w.UserId });

            modelBuilder.Entity<WishLists>()
                .HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProductsId);

            modelBuilder.Entity<WishLists>()
                .HasOne(w => w.AppUser)
                .WithMany()
                .HasForeignKey(w => w.UserId);

        }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Sole> Soles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<WishLists> WishLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Wards> Wards { get; set; }
    }
}
