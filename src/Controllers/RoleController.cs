using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;
using test_LK_ecommerce.DTOs;

namespace test_LK_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase // use ControllerBase for APIs
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper; // to use AutoMapper

        // to initialize controller
        public RoleController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get list of all roles as DTOs
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            // to get the roles from the database
            var roles = await dBContext.Role.ToListAsync();

            // to map the database entities to our public DTOs
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        // to get a single role by its id
        [HttpGet("{id:int}", Name = "GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await dBContext.Role.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        // to create a new role from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleDto)
        {
            // to map the incoming DTO to our internal Role entity
            var role = _mapper.Map<Role>(roleDto);

            dBContext.Role.Add(role);
            await dBContext.SaveChangesAsync();

            // to map the new entity back to a DTO to return to the client
            var createdDto = _mapper.Map<RoleDto>(role);

            // return a 201 Created status with a link to the new role
            return CreatedAtRoute("GetRoleById", new { id = role.RoleId }, createdDto);
        }

        // to update a role from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto roleDto)
        {
            var role = await dBContext.Role.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            // use automapper to update the entity from the dto
            _mapper.Map(roleDto, role);
            await dBContext.SaveChangesAsync();

            // return 204 No Content, the standard for a successful update
            return NoContent();
        }

        // to delete a role by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await dBContext.Role.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            dBContext.Role.Remove(role);
            await dBContext.SaveChangesAsync();

            // return 204 No Content for a successful delete
            return NoContent();
        }
    }
}