using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypesController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public PetTypesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() => Ok(await appDbContext.PetTypes.ToListAsync());
        [HttpPost]
        public async Task<ActionResult<User>> Add(PetTypes petTypes)
        {
            if (petTypes != null)
            {
                var result = appDbContext.PetTypes.Add(petTypes).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
    }
}
