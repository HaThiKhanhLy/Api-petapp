using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public ProvinceController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Province>>> Get() => Ok(await appDbContext.Province.ToListAsync());
        [HttpPost]
        public async Task<ActionResult<Province>> Add(Province province)
        {
            if (province != null)
            {
                var result = appDbContext.Province.Add(province).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
    }
    }
