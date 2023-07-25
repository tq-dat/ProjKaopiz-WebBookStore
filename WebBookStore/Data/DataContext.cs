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

        public DbSet<Cartitem> Cartitems { get; set; }
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

            modelBuilder.Entity<Cartitem>()
                    .HasKey(ci => new { ci.Id });
            modelBuilder.Entity<Cartitem>()
                    .HasOne(p => p.Product)
                    .WithMany(ci => ci.Cartitems)
                    .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Cartitem>()
                    .HasOne(o => o.Order)
                    .WithMany(ci => ci.Cartitems)
                    .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<Cartitem>()
                    .HasOne(u => u.User)
                    .WithMany(ci => ci.Cartitems)
                    .HasForeignKey(u => u.UserId);

        }
    }
}


