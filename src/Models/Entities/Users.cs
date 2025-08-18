using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
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

        [ForeignKey("RoleId")]
        [JsonIgnore]   
        public Role? Role { get; set; }  

        [ForeignKey("StatusId")]
        [JsonIgnore]   
        public Status? Status { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    }
}
