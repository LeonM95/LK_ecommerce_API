using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using src.Data;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : Controller
    {

        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;

        // to initialize controller
        public PaymentMethodsController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get a list of all payment methods as DTOs
        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            // to get the statuses from the database
            var paymentMethod = await dBContext.PaymentMethod.ToListAsync();

            // to map the database entities to our public DTOs
            var paymentMethodDtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethod);
            return Ok(paymentMethodDtos);
        }


        // to get a single product by its id
        [HttpGet("{id:int}", Name = "GetPaymentMethodById")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var paymentMethod = await dBContext.PaymentMethod
                .FirstOrDefaultAsync(p => p.PaymentMethodId == id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethodDto = _mapper.Map<PaymentMethodDto>(paymentMethod);
            return Ok(paymentMethodDto);
        }


    }
}
