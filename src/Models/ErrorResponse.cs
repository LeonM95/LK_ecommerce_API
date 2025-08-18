namespace test_LK_ecommerce.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; } // nullable, only filled in dev
    }
}
