using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public DateTime? CartAddedDate { get; set; }
        public required DateTime? CartUpdatedDate { get; set; }
        public required int UserId { get; set; }
        public required int StatusId { get; set; }
        public required Users Users { get; set; }
        public required Status Status { get; set; }
    }
}
