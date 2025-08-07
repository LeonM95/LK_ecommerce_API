using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(50)]
        public string? RoleName { get; set; }
    }
}
