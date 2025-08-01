namespace src.DTOs
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal Total {  get; set; }

        // Flattened FK
        public string? PaymentMethodName { get; set; }
        public string? StatusName { get; set; }
        public string? ProductName { get; set; } 
        public string? ShippingAddress { get; set; }
    }
}
