using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.DTOs
{
    public class CartProductDto
    {
        public int CartProductsId { get; set; }
        public int Quantity { get; set; }
        public DateTime? AddedDate { get; set; }
        public int CartId { get; set; }
        public string? ProductName { get; set; }
        public string? StatusName { get; set; }

    }
}
