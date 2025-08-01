using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateStatusDto
    {
        [Required]
        [StringLength(50)]
        public string? StatusDescription { get; set; }
    }
}
