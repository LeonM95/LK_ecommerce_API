using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SaleDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _dbContext.Sale
                .Include(s => s.Status)
                .Include(s => s.PaymentMethod)
                .Include(s => s.Address)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .FirstOrDefaultAsync(s => s.SaleId == orderId);

            return _mapper.Map<SaleDto>(order);
        }

        public async Task<IEnumerable<SaleDto>> GetOrdersForUserAsync(int userId)
        {
            var orders = await _dbContext.Sale
                .Include(s => s.Status)
                .Include(s => s.PaymentMethod)
                .Include(s => s.Address)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SaleDto>>(orders);
        }

        public async Task<SaleDto?> CreateOrderFromCartAsync(int userId, CreateSaleDto orderDto)
        {
            // to find the user's active shopping cart with all its items and products
            var cart = await _dbContext.ShoppingCart
                .Include(sc => sc.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId && sc.StatusId == 1);

            // to validate the cart
            if (cart == null || !cart.CartProducts.Any())
            {
                return null; 
            }

            var saleDetails = new List<SaleDetail>();
            decimal total = 0;

            // Validate stock and calculate total
            foreach (var item in cart.CartProducts)
            {
                if (item.Product.Stock < item.Quantity)
                {
                    return null; // Not enough stock for one of the items
                }

                var subtotal = item.Quantity * item.Product.Price;
                total += subtotal;

                saleDetails.Add(new SaleDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    Subtotal = subtotal
                });
            }

            // Create the new Sale (Order)
            var newSale = new Sale
            {
                UserId = userId,
                AddressId = orderDto.AddressId,
                PaymentMethodId = orderDto.PaymentMethodId,
                SaleDate = DateTime.UtcNow,
                Total = total,
                StatusId = 4, // "Pending" or "Processing"
                SaleDetails = saleDetails
            };

            // Update product stock
            foreach (var item in cart.CartProducts)
            {
                item.Product.Stock -= item.Quantity;
            }

            // Deactivate the shopping cart
            cart.StatusId = 2; // "Inactive" or "Completed"
            cart.CartUpdatedDate = DateTime.UtcNow;

            // Add the new Sale to the context
            await _dbContext.Sale.AddAsync(newSale);

            // Save everything in one transaction
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SaleDto>(newSale);
        }


    }
}