using src.DTOs;

namespace src.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodDto>> GetAllPaymentMethodsAsync();
        Task<PaymentMethodDto?> GetPaymentMethodByIdAsync(int id);
        Task<PaymentMethodDto> CreatePaymentMethodAsync(CreatePaymentMethodDto paymentMethodDto);
        Task<bool> UpdatePaymentMethodAsync(int id, UpdatePaymentMethodDto paymentMethodDto);
        Task<bool> DeletePaymentMethodAsync(int id);
    }
}
