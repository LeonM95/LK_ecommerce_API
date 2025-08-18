using AutoMapper;
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
    public class RoleController : ControllerBase // use ControllerBase for APIs
    {
        private readonly IRoleService _roleService;

        // to initialize the controller with the role service
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // to get a list of all roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // to get a single role by its id
        [HttpGet("{id:int}", Name = "GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // to create a new role
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleDto)
        {
            var newRole = await _roleService.CreateRoleAsync(roleDto);
            return CreatedAtRoute("GetRoleById", new { id = newRole.RoleId }, newRole);
        }

        // to update a role
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto roleDto)
        {
            var success = await _roleService.UpdateRoleAsync(id, roleDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a role
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var success = await _roleService.DeleteRoleAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}