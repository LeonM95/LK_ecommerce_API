using Microsoft.EntityFrameworkCore;
using System.Net;
using src.Models.Entities;

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
            base.OnModelCreating(modelBuilder);

            //status
            //status
            //status
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");
                entity.Property(s => s.StatusDescription).HasColumnName("status");
            });

            //role
            //role
            //role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.Property(r => r.RoleName).HasColumnName("rolName");
            });

            //user
            //user
            //user
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.Fullname).HasColumnName("fullName");
                entity.Property(u => u.Email).HasColumnName("email");
                entity.Property(u => u.Password).HasColumnName("password");
                entity.Property(u => u.PhoneNumber).HasColumnName("phoneNumber");
                entity.Property(u => u.Description).HasColumnName("description");
                entity.Property(u => u.RoleId).HasColumnName("roleId");
                entity.Property(u => u.StatusId).HasColumnName("statusId");

                //  Role has many Users, not RoleName collection
                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)      // assuming Role has ICollection<Users> Users { get; set; }
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                //Status has many Users, not StatusDescription collection
                entity.HasOne(u => u.Status)
                    .WithMany(s => s.Users)      // assuming Status has ICollection<Users> Users { get; set; }
                    .HasForeignKey(u => u.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // address
            // address
            // address
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");
                entity.Property(a => a.AddressId).HasColumnName("addressId");
                entity.Property(a => a.AddressLine).HasColumnName("address");
                entity.Property(a => a.PostalCode).HasColumnName("postalcode");
                entity.Property(a => a.City).HasColumnName("city");
                entity.Property(a => a.Country).HasColumnName("country");
                entity.Property(a => a.UserId).HasColumnName("userId");
                entity.Property(a => a.StatusId).HasColumnName("statusId");

                entity.HasOne(a => a.Users)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // cartProducts
            // cartProducts
            // cartProducts
            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.ToTable("CartProducts");
                entity.Property(cp => cp.CartProductsId).HasColumnName("cartProductsId");
                entity.Property(cp => cp.Quantity).HasColumnName("quantity");
                entity.Property(cp => cp.AddedDate).HasColumnName("addedDate");
                entity.Property(cp => cp.CartId).HasColumnName("cartId");
                entity.Property(cp => cp.ProductId).HasColumnName("productId");
                entity.Property(cp => cp.StatusId).HasColumnName("statusId");

                entity.HasOne(cp => cp.Product)
                    .WithMany()
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // category
            // category
            // category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.Property(c => c.CategoryId).HasColumnName("categoryId");
                entity.Property(c => c.CategoryName).HasColumnName("categoryName");
                entity.Property(c => c.Reference).HasColumnName("reference");
                entity.Property(c => c.StatusId).HasColumnName("statusId");

                entity.HasOne(c => c.Status)
                    .WithMany()
                    .HasForeignKey(c => c.StatusId)
                    .HasConstraintName("FK_category_Status")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // image
            // image
            // image
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");
                entity.Property(i => i.UrlImage).HasColumnName("urlImage");
                entity.Property(i => i.Description).HasColumnName("description");
                entity.Property(i => i.ProductId).HasColumnName("productId");
                entity.Property(i => i.StatusId).HasColumnName("statusId");

                entity.HasOne(i => i.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(i => i.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // payment method
            // payment method
            // payment method
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");
                entity.Property(p => p.MethodName).HasColumnName("methodName");
            });

            // product
            // product
            // product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(p => p.ProductName).HasColumnName("productName");
                entity.Property(p => p.Description).HasColumnName("description");
                entity.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
                entity.Property(p => p.Stock).HasColumnName("stock");
                entity.Property(p => p.CategoryId).HasColumnName("categoryId");
                entity.Property(p => p.StatusId).HasColumnName("statusId");
                entity.Property(p => p.UserId).HasColumnName("userId");

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // review
            // review
            // review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");
                entity.Property(r => r.ReviewId).HasColumnName("reviewId");
                entity.Property(r => r.ReviewText).HasColumnName("review");
                entity.Property(r => r.ReviewDate).HasColumnName("ReviewDate");
                entity.Property(r => r.Raiting).HasColumnName("Raiting");
                entity.Property(r => r.UserId).HasColumnName("UserId");
                entity.Property(r => r.ProductId).HasColumnName("productId");
                entity.Property(r => r.StatusId).HasColumnName("StatusId");

                entity.HasOne(r => r.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.NoAction); // or Restrict

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(r => r.Status)
                    .WithMany()
                    .HasForeignKey(r => r.StatusId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });


            // sale
            // sale
            // sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");
                entity.Property(s => s.SaleId).HasColumnName("saleId");
                entity.Property(s => s.SaleDate).HasColumnName("saleDate");
                entity.Property(s => s.Total).HasColumnName("total").HasColumnType("decimal(18,2)");
                entity.Property(s => s.PaymentMethodId).HasColumnName("paymentMethodId");
                entity.Property(s => s.StatusId).HasColumnName("statusId");
                entity.Property(s => s.AddressId).HasColumnName("AddressId");
                entity.Property(s => s.UserId).HasColumnName("UserId");

                entity.HasOne(s => s.Address)
                    .WithMany()  
                    .HasForeignKey(s => s.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);  

                entity.HasOne(s => s.PaymentMethod)
                    .WithMany() 
                    .HasForeignKey(s => s.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(s => s.Status)
                    .WithMany() 
                    .HasForeignKey(s => s.StatusId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(s => s.User)
                    .WithMany()  
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });


            // salesDetails
            // salesDetails
            // salesDetails
            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.ToTable("SalesDatails");
                entity.Property(sd => sd.SalesDetailsId).HasColumnName("SalesDatailsId");
                entity.Property(sd => sd.Quantity).HasColumnName("quantity");
                entity.Property(sd => sd.UnitPrice).HasColumnName("unitPrice").HasColumnType("decimal(18,2)");
                entity.Property(sd => sd.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(18,2)");
                entity.Property(sd => sd.SaleId).HasColumnName("saleId");
                entity.Property(sd => sd.ProductId).HasColumnName("productId");

                entity.HasOne(sd => sd.Sale)
                    .WithMany(s => s.SaleDetails)
                    .HasForeignKey(sd => sd.SaleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // shoppingCart
            // shoppingCart
            // shoppingCart
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("shoppingCart");
                entity.Property(c => c.CartId).HasColumnName("cartId");
                entity.Property(c => c.CartAddedDate).HasColumnName("cartAddedDate");
                entity.Property(c => c.CartUpdatedDate).HasColumnName("cartUpdatedDate");
                entity.Property(c => c.UserId).HasColumnName("userId");
                entity.Property(c => c.StatusId).HasColumnName("statusId");

                entity.HasOne(sc => sc.User)
                    .WithMany(u => u.ShoppingCarts)
                    .HasForeignKey(sc => sc.UserId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(sc => sc.Status)
                    .WithMany()
                    .HasForeignKey(sc => sc.StatusId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasMany(sc => sc.CartProducts)
                    .WithOne(cp => cp.ShoppingCart)
                    .HasForeignKey(cp => cp.CartId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}
