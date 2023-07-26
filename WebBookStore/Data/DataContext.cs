using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using WebBookStore.Models;

namespace WebBookStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                    .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            modelBuilder.Entity<ProductCategory>()
                    .HasOne(p => p.Product)
                    .WithMany(pc => pc.ProductCategories)
                    .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<ProductCategory>()
                    .HasOne(c => c.Category)
                    .WithMany(pc => pc.ProductCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<CartItem>()
                    .HasKey(ci => new { ci.Id });
            modelBuilder.Entity<CartItem>()
                    .HasOne(p => p.Product)
                    .WithMany(ci => ci.CartItems)
                    .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<CartItem>()
                    .HasOne(o => o.Order)
                    .WithMany(ci => ci.CartItems)
                    .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<CartItem>()
                    .HasOne(u => u.User)
                    .WithMany(ci => ci.CartItems)
                    .HasForeignKey(u => u.UserId);

        }
    }
}


