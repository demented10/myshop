using eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace eshop.Infrastructure
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<BasketItem> BasketItems { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create product realtions
            //modelBuilder.Entity<Product>().HasKey(p => p.Id).HasName("ProductPrimaryKey");
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p=>p.CategoryId);

            //create Category relations
            //modelBuilder.Entity<Category>().HasMany<Product>();
            modelBuilder.Entity<BasketItem>()
            .HasKey(bi => new { bi.BasketId, bi.ProductId });
            modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<int>();

            modelBuilder.Entity<BasketItem>().HasOne(b_i => b_i.Basket).WithMany(b => b.BasketItems).HasForeignKey(b_i => b_i.BasketId);
            modelBuilder.Entity<BasketItem>().HasOne(b_i => b_i.Product).WithMany().HasForeignKey(b_i => b_i.ProductId);

            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}