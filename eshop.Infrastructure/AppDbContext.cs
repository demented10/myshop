using eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace eshop.Infrastructure
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
       // public DbSet<Order> Orders { get; set; }
       // public DbSet<User> Users { get; set; }
       // public DbSet<Basket> Baskets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=eshop;Username=postgres;Password=password")
                .EnableSensitiveDataLogging(true)
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create product realtions
            modelBuilder.Entity<Product>().HasKey(p => p.Id).HasName("ProductPrimaryKey");
         
            //create Category relations
            modelBuilder.Entity<Category>();
            
        }
    }
}