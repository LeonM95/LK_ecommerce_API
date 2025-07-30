namespace test_LK_ecommerce.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // related FK entities for easy display
        public string? CategoryName { get; set; }
        public string? StatusName { get; set; }
    }
}
