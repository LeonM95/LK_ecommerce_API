using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using src.Models.Entities;
using src.Data;
using src.Services;
using Microsoft.Extensions.Configuration.UserSecrets;



namespace src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;


        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        // to get shopping Cart of current user
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserShoppingCart(int userId)
        {
            var shpCartDto = await _shoppingCartService.GetCartByUserIdAsync(userId);

            if (shpCartDto == null)
            {
                return NotFound("Shopping Cart not found");
            }

            return Ok(shpCartDto);
        }


        // to add a new item to a cart
        [HttpPost("items")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartDto itemDto)
        {
            // The userId now comes directly from the DTO
            var updatedCart = await _shoppingCartService.AddItemToCartAsync(itemDto.UserId, itemDto);

            if (updatedCart == null)
            {
                return BadRequest("Could not add item to cart. Check if product exists.");
            }

            return Ok(updatedCart);
        }

        // to update the quantity of an item in the cart
        [HttpPatch("items/{cartProductId:int}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartProductId, [FromBody] UpdateCartProductDto updateDto)
        {
            if (!updateDto.Quantity.HasValue)
            {
                return BadRequest("Let's select an quanlty of items");
            }

            var success = await _shoppingCartService.UpdateItemQuantityAsync(cartProductId, updateDto.Quantity.Value);

            if (!success)
            {
                return NotFound("Cart item not found.");
            }

            return NoContent();
        }

        // to remove an item from the cart
        [HttpDelete("items/{cartProductId:int}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartProductId)
        {
            var success = await _shoppingCartService.RemoveItemFromCartAsync(cartProductId);

            if (!success)
            {
                return NotFound("Cart item not found.");
            }

            return NoContent();
        }



    }
}
