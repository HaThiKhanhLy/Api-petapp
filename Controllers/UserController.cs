using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public UserController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() => Ok(await appDbContext.Users.ToListAsync());
        [HttpPost]
        public async Task<ActionResult<User>> Add([FromBody]User user) 
        {
            if(user != null)
            {
                var result= appDbContext.Users.Add(user).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }

        [HttpGet("{email}/{password}")]
        public  async Task<ActionResult<User>> Login(string email, string password)
        {
            if(email is not null && password is not null)
            {
                User user = await appDbContext.Users
                    .Where(x => x.Email!.ToLower().Equals(email.ToLower()) && x.Password == password)
                    .FirstOrDefaultAsync();
                return user != null ? Ok(user) : NotFound("User not found");
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("updateUser/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.ID)
            {
                return BadRequest("ID mismatch");
            }

            var existingUser = await appDbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FullName = updatedUser.FullName;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.Address = updatedUser.Address;
            existingUser.DayOfBirth = updatedUser.DayOfBirth;
            existingUser.Gender = updatedUser.Gender;
            existingUser.Role = updatedUser.Role;
            existingUser.Status = updatedUser.Status;
            // Cập nhật các trường khác tương tự

            await appDbContext.SaveChangesAsync();

            return Ok(existingUser);
        }

    }

}
