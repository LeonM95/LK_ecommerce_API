using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateShoppingCartDto
    {
        //Usually a shopping cart is created automatically we just need the userId
        [Required]
        public int UserId { get; set; }
    }
}
