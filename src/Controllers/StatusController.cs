using Microsoft.AspNetCore.Mvc;
using src.DTOs;
using src.Services;
using System.Threading.Tasks;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        // to initialize the controller with the status service
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // to get a list of all statuses
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await _statusService.GetAllStatusesAsync();
            return Ok(statuses);
        }

        // to get a single status by its id
        [HttpGet("{id:int}", Name = "GetStatusById")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await _statusService.GetStatusByIdAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // to create a new status
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto statusDto)
        {
            var newStatus = await _statusService.CreateStatusAsync(statusDto);
            return CreatedAtRoute("GetStatusById", new { id = newStatus.StatusId }, newStatus);
        }

        // to update a status
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto statusDto)
        {
            var success = await _statusService.UpdateStatusAsync(id, statusDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a status
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var success = await _statusService.DeleteStatusAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}