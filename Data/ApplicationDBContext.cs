using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        //class we creted under Models > Entities
        public DbSet<Users> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //match with datbase table names
            //match with datbase table names
            //match with datbase table names
            
            //status
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Status>().Property(s => s.StatusDescription).HasColumnName("status");

            //role
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().Property(r => r.RoleName).HasColumnName("rolName");

            //user
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>().Property(u => u.Fullname).HasColumnName("fullName");
            modelBuilder.Entity<Users>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<Users>().Property(u => u.Password).HasColumnName("password");
            modelBuilder.Entity<Users>().Property(u => u.PhoneNumber).HasColumnName("phoneNumber");
            modelBuilder.Entity<Users>().Property(u => u.Description).HasColumnName("description");
            modelBuilder.Entity<Users>().Property(u => u.RoleId).HasColumnName("roleId");
            modelBuilder.Entity<Users>().Property(u => u.StatusId).HasColumnName("statusId");

        }
    }
}
