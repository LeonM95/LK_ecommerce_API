using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
