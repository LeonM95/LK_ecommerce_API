using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public PaymentMethodService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get a list of all payment methods
        public async Task<IEnumerable<PaymentMethodDto>> GetAllPaymentMethodsAsync()
        {
            var paymentMethods = await _dbContext.PaymentMethod.ToListAsync();
            return _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);
        }

        // to get a single payment method by its id
        public async Task<PaymentMethodDto?> GetPaymentMethodByIdAsync(int id)
        {
            var paymentMethod = await _dbContext.PaymentMethod.FindAsync(id);
            return _mapper.Map<PaymentMethodDto>(paymentMethod);
        }

        // to create a new payment method
        public async Task<PaymentMethodDto> CreatePaymentMethodAsync(CreatePaymentMethodDto paymentMethodDto)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
            await _dbContext.PaymentMethod.AddAsync(paymentMethod);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<PaymentMethodDto>(paymentMethod);
        }

        // to update a payment method
        public async Task<bool> UpdatePaymentMethodAsync(int id, UpdatePaymentMethodDto paymentMethodDto)
        {
            var paymentMethod = await _dbContext.PaymentMethod.FindAsync(id);
            if (paymentMethod == null)
            {
                return false; // Indicates method was not found
            }
            _mapper.Map(paymentMethodDto, paymentMethod);
            await _dbContext.SaveChangesAsync();
            return true; // Indicates success
        }

        // to delete a payment method
        public async Task<bool> DeletePaymentMethodAsync(int id)
        {
            var paymentMethod = await _dbContext.PaymentMethod.FindAsync(id);
            if (paymentMethod == null)
            {
                return false;
            }
            _dbContext.PaymentMethod.Remove(paymentMethod);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
