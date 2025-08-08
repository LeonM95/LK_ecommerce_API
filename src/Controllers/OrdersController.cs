using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using src.Controllers.Models.Entities;
using src.Data;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;


        public OrdersController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }


        // to get orders of user
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetOrdersOfUser(int userId)
        {
            var orders = await dBContext.Sale
                // list data of DTO
                .Include(sc => sc.Status)
                .Include(sc => sc.PaymentMethod)
                .Include(sc => sc.Address)
                .Include(sc => sc.SaleDetails)
                // include list of sale items
                .ThenInclude(sd => sd.Product)
                .Where(s => s.UserId == userId).ToListAsync();

            if (!orders.Any())
            {
                return NotFound("Orders not found for this user");
            }

            var orderDto = _mapper.Map<IEnumerable<SaleDto>>(orders);
            return Ok(orderDto);
        }


        // to get orders of user
        [HttpGet("{OrderId:int}")]
        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var order = await dBContext.Sale
                // list data of DTO
                .Include(sc => sc.Status)
                .Include(sc => sc.PaymentMethod)
                .Include(sc => sc.Address)
                .Include(sc => sc.SaleDetails)
                // include list of sale items
                .ThenInclude(sd => sd.Product)
                .FirstOrDefaultAsync(s => s.SaleId == OrderId);

            if (order == null)
            {
                return NotFound("This order has not been found");
            }

            var orderDto = _mapper.Map<SaleDto>(order);
            return Ok(orderDto);
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateSaleDto orderDto)
        {
            // to map the DTO with internal Sales entity
            var order = _mapper.Map<Sale>(orderDto);

            // to set default values on the server
            order.StatusId = 4; // pending
            order.SaleDate = DateTime.UtcNow;
   
            //here I have to call the function that calculates the order's amount

            await dBContext.Sale.AddAsync(order);
            await dBContext.SaveChangesAsync();

            var createdOrderDto = _mapper.Map<SaleDto>(order);


            // to return a 201 Created status with a link to the new order
            return CreatedAtRoute(
             "GetOrdersOfUser", new { userId = order.UserId }, createdOrderDto);
        }



    }
}
