using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Description { get; set; }
        public required int RoleId { get; set; }
        public required int StatusId { get; set; }

        public required Role Role { get; set; }

        public required Status Status { get; set; }
    }
}
