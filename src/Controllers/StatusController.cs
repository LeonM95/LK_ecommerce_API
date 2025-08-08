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
    public class StatusController : ControllerBase // use ControllerBase for APIs
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper; // to use AutoMapper

        // to initialize controller
        public StatusController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get a list of all statuses as DTOs
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            // to get the statuses from the database
            var statuses = await dBContext.Status.ToListAsync();

            // to map the database entities to our public DTOs
            var statusDtos = _mapper.Map<IEnumerable<StatusDto>>(statuses);
            return Ok(statusDtos);
        }

        // to get a single status by its id
        [HttpGet("{id:int}", Name = "GetStatusById")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await dBContext.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            var statusDto = _mapper.Map<StatusDto>(status);
            return Ok(statusDto);
        }

        // to create a new status from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto statusDto)
        {
            // to map the incoming DTO to our internal Status entity
            var status = _mapper.Map<Status>(statusDto);

            dBContext.Status.Add(status);
            await dBContext.SaveChangesAsync();

            // to map the new entity back to a DTO to return to the client
            var createdDto = _mapper.Map<StatusDto>(status);

            // return a 201 Created status with a link to the new status
            return CreatedAtRoute("GetStatusById", new { id = status.StatusId }, createdDto);
        }

        // to update a status from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto statusDto)
        {
            var status = await dBContext.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            // use automapper to update the entity from the dto
            _mapper.Map(statusDto, status);
            await dBContext.SaveChangesAsync();

            // return 204 No Content, the standard for a successful update
            return NoContent();
        }

        // to delete a status by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await dBContext.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            dBContext.Status.Remove(status);
            await dBContext.SaveChangesAsync();

            // return 204 No Content for a successful delete
            return NoContent();
        }
    }
}