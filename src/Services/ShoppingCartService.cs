using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public ShoppingCartService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dBContext = dbContext;
            _mapper = mapper;
        }

        // To get cart by userId
        public async Task<ShoppingCartDto?> GetCartByUserIdAsync(int userId)
        {
            var cart = await _dBContext.ShoppingCart
                // list of items in Cart
              .Include(sc => sc.User)
              .Include(sc => sc.Status)
              .Include(sc => sc.CartProducts)
              // deteails of each item
              .ThenInclude(cp => cp.Product).
              FirstOrDefaultAsync(sc => sc.UserId == userId);

            return _mapper.Map<ShoppingCartDto>(cart);
  
        }

        //to add item to cart
        public async Task<ShoppingCartDto> AddItemToCartAsync(int userId, AddItemToCartDto itemDto)
        {
            // to find the user's active cart by their UserId
            var cart = await _dBContext.ShoppingCart
                .Include(sc => sc.CartProducts)
                .FirstOrDefaultAsync(sc => sc.UserId == userId && sc.StatusId == 1);

            // if the user has no active cart, create one
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    StatusId = 1, // Active
                    CartAddedDate = DateTime.UtcNow
                };
                // Add the new cart to the context
                await _dBContext.ShoppingCart.AddAsync(cart);
            }

            // to check if this product is already in the cart
            var existingItem = cart.CartProducts
                .FirstOrDefault(cp => cp.ProductId == itemDto.ProductId);

            if (existingItem != null)
            {
                // if item exists update the quantity
                existingItem.Quantity += itemDto.Quantity;
            }
            else
            {
                // if item is new, create it and link it to the cart object
                var newItem = new CartProduct
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    StatusId = 1, // Active
                    AddedDate = DateTime.UtcNow,
                    ShoppingCart = cart // This explicitly creates the link for EF Core
                };
                await _dBContext.CartProduct.AddAsync(newItem);
            }

            // to update the timestamp
            cart.CartUpdatedDate = DateTime.UtcNow;

            // A single SaveChanges call will handle everything in one transaction
            await _dBContext.SaveChangesAsync();

            return _mapper.Map<ShoppingCartDto>(cart);
        }

  

        public async Task<bool> UpdateItemQuantityAsync(int cartProductId, int quantity)
        {
            var cartItem = await _dBContext.CartProduct.FindAsync(cartProductId);

            // to check if the item was found
            if (cartItem == null)
            {
                return false; //  not found
            }

            // to find the parent cart and update its timestamp
            var cart = await _dBContext.ShoppingCart.FindAsync(cartItem.CartId);
            if (cart != null)
            {
                cart.CartUpdatedDate = DateTime.UtcNow;
            }

            // to update or remove the item based on the new quantity
            if (quantity <= 0)
            {
                _dBContext.CartProduct.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity; // Use the 'quantity' parameter
            }

            await _dBContext.SaveChangesAsync();
            return true; // Indicates success
        }


        public async Task<bool> RemoveItemFromCartAsync(int cartProductId)
        {
            var cartItem = await _dBContext.CartProduct.FindAsync(cartProductId);

            if (cartItem == null)
            {
                return false;
            }

            // to update the parent cart's timestamp before removing the item
            var cart = await _dBContext.ShoppingCart.FindAsync(cartItem.CartId);
            if (cart != null)
            {
                cart.CartUpdatedDate = DateTime.UtcNow;
            }

            _dBContext.CartProduct.Remove(cartItem);
            await _dBContext.SaveChangesAsync();
            return true;
        }

    }
}
