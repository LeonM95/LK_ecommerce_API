using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;

namespace test_LK_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        // to initialize controller
        public UserController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        // to get list of all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dBContext.Users.ToListAsync();
            return Ok(users);
        }

        // to search/get a user by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await dBContext.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // to search/get a user by name
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            var lowerCaseName = name.ToLower();


            var users = await dBContext.Users.
                Where(u => u.Fullname.ToLower().Contains(lowerCaseName)).ToListAsync();

                return Ok(users); 
        }

        // to create a user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Users user)
        {
            // to avoid creating a new role and user
            if (user.Role != null)
                dBContext.Entry(user.Role).State = EntityState.Unchanged;
            if (user.Status != null)
                dBContext.Entry(user.Status).State = EntityState.Unchanged;

            dBContext.Users.Add(user);
            await dBContext.SaveChangesAsync();

            return Ok(user);
        }

        // to modify a user
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            var user = await dBContext.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Fullname = updatedUser.Fullname;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Description = updatedUser.Description;

            await dBContext.SaveChangesAsync();

            return Ok(user);
        }

        // to delete a user
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await dBContext.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            dBContext.Users.Remove(user);
            await dBContext.SaveChangesAsync();

            return Ok(new { message = $"User with ID {id} has been deleted." });
        }
    }
}
