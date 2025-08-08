using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using src.Controllers.Models.Entities;

namespace src.DTOs
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
