using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;

namespace test_LK_ecommerce.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        
        // to initialized controller
        public RoleController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }


        // to get list of all roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await dBContext.Role.ToListAsync();
            return Ok(roles);
        }

        // to search/get a role by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await dBContext.Role.FindAsync(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // to create a role
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            dBContext.Role.Add(role);
            await dBContext.SaveChangesAsync();

            // Creates a new role and returns a 201 response with a link to view it.
            return Ok(role);
        }

        // to modify a role
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role updatedRole)
        {
            var role = await dBContext.Role.FindAsync(id);
            if (role == null)
                return NotFound();

            role.RoleName = updatedRole.RoleName;
            await dBContext.SaveChangesAsync();

            return Ok(role);
        }

        // to delete a role
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await dBContext.Role.FindAsync(id);
            if (role == null)
                return NotFound();

            dBContext.Role.Remove(role);
            await dBContext.SaveChangesAsync();

            return Ok(new { message = $"Role with ID {id} has been deleted." });
        }
    }
}
