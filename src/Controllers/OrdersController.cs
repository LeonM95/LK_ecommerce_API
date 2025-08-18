using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;
using src.Services;
using System.Security.Claims;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // to get orders of the logged-in user
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var orders = await _orderService.GetOrdersForUserAsync(userId);
            return Ok(orders);
        }

        // to get a single order by its id
        [HttpGet("{orderId:int}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            // You might add a check here to ensure the user owns this order
            return Ok(order);
        }

        // to create a new order from the user's cart (checkout)
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateSaleDto orderDto)
        {
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var newOrder = await _orderService.CreateOrderFromCartAsync(orderDto.UserId, orderDto);

            if (newOrder == null)
            {
                return BadRequest("Could not create order. Check cart status or product stock.");
            }

            return CreatedAtAction(nameof(GetOrderById), new { orderId = newOrder.SaleId }, newOrder);
        }



    }
}
