using Microsoft.EntityFrameworkCore;
using System.Net;
using src.Controllers.Models.Entities;

namespace src.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<CartProduct> CartProduct { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This should be the first line

            // --- Entity Configurations ---

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(a => a.Users)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // An address should not be cascade deleted
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // Deleting a category shouldn't delete products
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // If a user is deleted, their reviews can go too
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(s => s.Total).HasColumnType("decimal(18,2)");

                // Deleting a User should not cascade to delete their Sales
                entity.HasOne(s => s.Users)
                    .WithMany() // Assuming a User can have many Sales
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.Property(sd => sd.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(sd => sd.Subtotal).HasColumnType("decimal(18,2)");

                entity.HasOne(sd => sd.Sale)
                    .WithMany(s => s.SaleDetails)
                    .HasForeignKey(sd => sd.SaleId)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting a Sale should delete its details
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasMany(sc => sc.CartProducts)
                    .WithOne(cp => cp.ShoppingCart)
                    .HasForeignKey(cp => cp.CartId)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting a cart should delete its items
            });

        }
    }
}