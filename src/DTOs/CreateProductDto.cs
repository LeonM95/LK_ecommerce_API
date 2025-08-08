namespace src.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
