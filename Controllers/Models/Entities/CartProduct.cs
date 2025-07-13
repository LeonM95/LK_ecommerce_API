using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class CartProduct
    {
        [Key]
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Reference { get; set; }


        public required int StatusId { get; set; }
        public required Status Status { get; set; }
    }
}
