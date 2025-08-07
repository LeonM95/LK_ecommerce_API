using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;
using test_LK_ecommerce.DTOs;



namespace src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;


        public ShoppingCartController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }


        // to get shopping Cart of current user
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserShoppingCart(int userId)
        {
            var cart = await dBContext.ShoppingCart
                      // list of items in Cart
                .Include(sc => sc.Users)
                .Include(sc => sc.Status)
                .Include(sc => sc.CartProducts)
                // deteails of each item
                .ThenInclude(cp => cp.Product).
                FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null) {
                return NotFound("Shopping Cart not found");
            }

            var shpCartDto = _mapper.Map<ShoppingCartDto>(cart);
            return Ok(shpCartDto);
        }

        // to add a new item to a cart
        [HttpPost("items")]
        public async Task<IActionResult> AddItemToCart([FromBody] CreateCartProductDto itemDto)
        {
            // to find the user's active cart by their UserId
            var cart = await dBContext.ShoppingCart
                .Include(sc => sc.CartProducts)
                .FirstOrDefaultAsync(sc => sc.UserId == itemDto.UserId && sc.StatusId == 1);

            // if the user has no active cart, create one
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = itemDto.UserId,
                    StatusId = 1, // Active
                    CartAddedDate = DateTime.UtcNow
                };
                // Add the new cart to the context, but DON'T save yet
                await dBContext.ShoppingCart.AddAsync(cart);
            }

            // to check if this product is already in the cart
            var existingItem = cart.CartProducts
                .FirstOrDefault(cp => cp.ProductId == itemDto.ProductId);

            if (existingItem != null)
            {
                // if item exists, just update the quantity
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
                await dBContext.CartProduct.AddAsync(newItem);
            }

            // to update the timestamp
            cart.CartUpdatedDate = DateTime.UtcNow;

            // A single SaveChanges call will handle everything in one transaction
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        // to update the quantity of an item in the cart
        [HttpPatch("items/{cartProductId:int}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartProductId, [FromBody] UpdateCartProductDto updateDto)
        {
            var cartItem = await dBContext.CartProduct.FindAsync(cartProductId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            // to update the quantity from the DTO
            cartItem.Quantity = updateDto.Quantity.Value;

            // to find the parent cart and update its timestamp
            var cart = await dBContext.ShoppingCart.FindAsync(cartItem.CartId);
            if (cart != null)
            {
                cart.CartUpdatedDate = DateTime.UtcNow;
            }

            await dBContext.SaveChangesAsync();
            return NoContent();
        }

        // to remove an item from the cart
        [HttpDelete("items/{cartProductId:int}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartProductId)
        {
            var cartItem = await dBContext.CartProduct.FindAsync(cartProductId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            // to update the parent cart's timestamp before removing the item
            var cart = await dBContext.ShoppingCart.FindAsync(cartItem.CartId);
            if (cart != null)
            {
                cart.CartUpdatedDate = DateTime.UtcNow;
            }

            dBContext.CartProduct.Remove(cartItem);
            await dBContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
