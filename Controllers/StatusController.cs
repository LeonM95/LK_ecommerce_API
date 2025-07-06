
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;

namespace test_LK_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        // to initialized controller
        public StatusController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }


        // Get all statuses
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await dBContext.Status.ToListAsync();
            return Ok(statuses);
        }

        // to search/get one status by id
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await dBContext.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // to create a new status
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status status)
        {
            dBContext.Status.Add(status);
            await dBContext.SaveChangesAsync();

            return Ok(status);
        }

        // to update an status
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Status updatedStatus)
        {
            var status = await dBContext.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            status.StatusDescription = updatedStatus.StatusDescription;
            await dBContext.SaveChangesAsync();
            return Ok(status);
        }

        // to delete a status
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await dBContext.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            dBContext.Status.Remove(status);
            await dBContext.SaveChangesAsync();
            return Ok(new { message = $"Status with ID {id} has been deleted." });
        }
    }
}
