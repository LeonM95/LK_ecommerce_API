using Microsoft.EntityFrameworkCore;
using System.Net;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        //class we creted under Models > Entities
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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //match with datbase table names
            //match with datbase table names
            //match with datbase table names


            //status
            //status
            //status
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Status>().Property(s => s.StatusDescription).HasColumnName("status");

            //role
            //role
            //role
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().Property(r => r.RoleName).HasColumnName("rolName");

            //user
            //user
            //user
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>().Property(u => u.Fullname).HasColumnName("fullName");
            modelBuilder.Entity<Users>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<Users>().Property(u => u.Password).HasColumnName("password");
            modelBuilder.Entity<Users>().Property(u => u.PhoneNumber).HasColumnName("phoneNumber");
            modelBuilder.Entity<Users>().Property(u => u.Description).HasColumnName("description");
            modelBuilder.Entity<Users>().Property(u => u.RoleId).HasColumnName("roleId");
            modelBuilder.Entity<Users>().Property(u => u.StatusId).HasColumnName("statusId");
            // FK Users - Role
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Role)
                .WithMany()  // or .WithMany(r => r.Users) if Role has Users collection navigation property
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Users - Status
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Status)
                .WithMany()  // or .WithMany(s => s.Users) if Status has Users collection navigation property
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);


            // address
            // address
            // address
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Address>().Property(a => a.AddressId).HasColumnName("addressId");
            modelBuilder.Entity<Address>().Property(a => a.AddressLine).HasColumnName("address");
            modelBuilder.Entity<Address>().Property(a => a.PostalCode).HasColumnName("postalcode");
            modelBuilder.Entity<Address>().Property(a => a.City).HasColumnName("city");
            modelBuilder.Entity<Address>().Property(a => a.Country).HasColumnName("country");
            modelBuilder.Entity<Address>().Property(a => a.UserId).HasColumnName("userId");
            modelBuilder.Entity<Address>().Property(a => a.StatusId).HasColumnName("statusId");
            // FK Addresses - Users
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Users)
                .WithMany() 
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Addresses - Status
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Status)
                .WithMany() 
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // cartProducts
            // cartProducts
            // cartProducts
            modelBuilder.Entity<CartProduct>().ToTable("CartProducts");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.CartProductsId).HasColumnName("cartProductsId");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.AddedDate).HasColumnName("addedDate");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.CartId).HasColumnName("cartId");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.ProductId).HasColumnName("productId");
            modelBuilder.Entity<CartProduct>().Property(cp => cp.StatusId).HasColumnName("statusId");
            // FK CartProducts - ShoppingCart
            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.ShoppingCart)
                .WithMany()  
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK CartProducts - Products
            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany()  
                .HasForeignKey(cp => cp.ProductId)
                .HasConstraintName("FK_PRODUCTSId")
                .OnDelete(DeleteBehavior.Restrict);
            // FK CartProducts - Status
            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Status)
                .WithMany()  
                .HasForeignKey(cp => cp.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // category
            // category
            // category
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Category>().Property(c => c.CategoryId).HasColumnName("categoryId");
            modelBuilder.Entity<Category>().Property(c => c.CategoryName).HasColumnName("categoryName");
            modelBuilder.Entity<Category>().Property(c => c.Reference).HasColumnName("reference");
            modelBuilder.Entity<Category>().Property(c => c.StatusId).HasColumnName("statusId");
            // FK Category - Status
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Status)
                .WithMany()  
                .HasForeignKey(c => c.StatusId)
                .HasConstraintName("FK_category_Status")
                .OnDelete(DeleteBehavior.Restrict);

            // image
            // image
            // image
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<Image>().Property(i => i.UrlImage).HasColumnName("urlImage");
            modelBuilder.Entity<Image>().Property(i => i.Description).HasColumnName("description");
            modelBuilder.Entity<Image>().Property(i => i.ProductId).HasColumnName("productId");
            modelBuilder.Entity<Image>().Property(i => i.StatusId).HasColumnName("statusId");
            // FK Image - Product
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithMany() // no Images collection on Product
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict); 
            // FK Image - Status
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Status)
                .WithMany()
                .HasForeignKey(i => i.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // payment method
            // payment method
            // payment method
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            modelBuilder.Entity<PaymentMethod>()
                .Property(p => p.MethodName)
                .HasColumnName("methodName");

            // product
            // product
            // product
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().Property(p => p.ProductName).HasColumnName("productName");
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnName("price");
            modelBuilder.Entity<Product>().Property(p => p.Stock).HasColumnName("stock");
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("categoryId");
            modelBuilder.Entity<Product>().Property(p => p.StatusId).HasColumnName("statusId");
            // FK Product - Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany() 
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Product - Status
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);


            // review
            // review
            // review
            modelBuilder.Entity<Review>().ToTable("review");
            modelBuilder.Entity<Review>().Property(r => r.ReviewId).HasColumnName("reviewId");
            modelBuilder.Entity<Review>().Property(r => r.ReviewText).HasColumnName("review");
            modelBuilder.Entity<Review>().Property(r => r.ProductId).HasColumnName("productId");
            // FK Review - Product
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // sale
            // sale
            // sale
            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<Sale>().Property(s => s.SaleId).HasColumnName("saleId");
            modelBuilder.Entity<Sale>().Property(s => s.SaleDate).HasColumnName("saleDate");
            modelBuilder.Entity<Sale>().Property(s => s.Total).HasColumnName("total");
            modelBuilder.Entity<Sale>().Property(s => s.PaymentMethodId).HasColumnName("paymentMethodId");
            modelBuilder.Entity<Sale>().Property(s => s.StatusId).HasColumnName("statusId");
            modelBuilder.Entity<Sale>().Property(s => s.ProductId).HasColumnName("productId");
            modelBuilder.Entity<Sale>().Property(s => s.AddressId).HasColumnName("AddressId");
            // FK Sale - PaymentMethod
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.PaymentMethod)
                .WithMany() 
                .HasForeignKey(s => s.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Sale - Product
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Sale - Status
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Status)
                .WithMany() 
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK Sale - Address
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Address)
                .WithMany()
                .HasForeignKey(s => s.AddressId)
                .HasConstraintName("FK_Address")
                .OnDelete(DeleteBehavior.Restrict);


            // salesDetails
            // salesDetails
            // salesDetails
            modelBuilder.Entity<SaleDetail>().ToTable("SalesDatails");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.SalesDetailsId).HasColumnName("SalesDatailsId");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.UnitPrice).HasColumnName("unitPrice");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.Subtotal).HasColumnName("subtotal");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.SaleId).HasColumnName("saleId");
            modelBuilder.Entity<SaleDetail>().Property(sd => sd.ProductId).HasColumnName("productId");
            // FK SalesDatails - Sale
            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Sale)
                .WithMany() 
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK SalesDatails - Product
            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany() 
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // shoppingCart
            // shoppingCart
            // shoppingCart
            modelBuilder.Entity<ShoppingCart>().ToTable("shoppingCart");
            modelBuilder.Entity<ShoppingCart>().Property(c => c.CartId).HasColumnName("cartId");
            modelBuilder.Entity<ShoppingCart>().Property(c => c.CartAddedDate).HasColumnName("cartAddedDate");
            modelBuilder.Entity<ShoppingCart>().Property(c => c.CartUpdatedDate).HasColumnName("cartUpdatedDate");
            modelBuilder.Entity<ShoppingCart>().Property(c => c.UserId).HasColumnName("userId");
            modelBuilder.Entity<ShoppingCart>().Property(c => c.StatusId).HasColumnName("statusId");
            // FK shoppingCart - Users
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(c => c.Users)
                .WithMany() 
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            // FK shoppingCart - Status
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(c => c.Status)
                .WithMany() 
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
