using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BannerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Banner>>> Get() => Ok(await appDbContext.Banner.ToListAsync());
        [HttpPost]
        public async Task<ActionResult<Banner>> Add(Banner banner)
        {
            if (banner != null)
            {
                var result = appDbContext.Banner.Add(banner).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
    }
}
