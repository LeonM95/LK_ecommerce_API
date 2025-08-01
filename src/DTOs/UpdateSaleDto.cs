using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateSaleDto
    {
        [Required]
        public int? StatusId { get; set; }
    }
}
