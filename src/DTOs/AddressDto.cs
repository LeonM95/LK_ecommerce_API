namespace src.DTOs
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public required string AddressLine { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        /// FK 
        public string? userName { get; set; }
        public string?  StatusName { get; set; }
    }
}
