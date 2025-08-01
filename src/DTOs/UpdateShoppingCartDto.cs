using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateShoppingCartDto
    {
        [Required]
        public int? StatusId { get; set; }
    }
}
