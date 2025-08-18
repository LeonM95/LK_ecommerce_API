using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;
using src.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase // Use ControllerBase for APIs
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IPaymentMethodService _paymentMethodService;

        // to initialize the controller with the payment method service
        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        // to get a list of all payment methods
        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
            return Ok(paymentMethods);
        }

        // to get a single payment method by its id
        [HttpGet("{id:int}", Name = "GetPaymentMethodById")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return Ok(paymentMethod);
        }

        // to create a new payment method (Admin only)
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] CreatePaymentMethodDto paymentMethodDto)
        {
            var newMethod = await _paymentMethodService.CreatePaymentMethodAsync(paymentMethodDto);
            return CreatedAtRoute("GetPaymentMethodById", new { id = newMethod.PaymentMethodId }, newMethod);
        }

        // to update a payment method (Admin only)
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, [FromBody] UpdatePaymentMethodDto paymentMethodDto)
        {
            var success = await _paymentMethodService.UpdatePaymentMethodAsync(id, paymentMethodDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a payment method (Admin only)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var success = await _paymentMethodService.DeletePaymentMethodAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}