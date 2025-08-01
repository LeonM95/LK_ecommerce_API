namespace src.DTOs
{
    public class SaleDetailDto
    {
        public int SalesDetailsId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
