using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public PetController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Pet>>> Get() => Ok(await appDbContext.Pets.ToListAsync());
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(int id)
        {
            var pet = await appDbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Add(Pet pet)
        {
            if (pet != null)
            {
                var result = appDbContext.Pets.Add(pet).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<List<Pet>>> GetByName(string name)
        {
            // Tìm kiếm các sản phẩm có tên chứa từ khóa được cung cấp
            var pets = await appDbContext.Pets
                                        .Where(p => p.NamePets.Contains(name))
                                        .ToListAsync();

            if (pets == null || pets.Count == 0)
            {
                return NotFound();
            }

            return Ok(pets);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> Delete(int id)
        {
            var pet = await appDbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            appDbContext.Pets.Remove(pet);
            await appDbContext.SaveChangesAsync();

            return Ok("Pet deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pet>> Update(int id, Pet updatedPet)
        {
            if (id != updatedPet.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingPet = await appDbContext.Pets.FindAsync(id);

            if (existingPet == null)
            {
                return NotFound();
            }

            existingPet.NamePets = updatedPet.NamePets;
            existingPet.PetsTypeID = updatedPet.PetsTypeID;
            existingPet.Gender = updatedPet.Gender;
            existingPet.Size = updatedPet.Size;
            existingPet.Age = updatedPet.Age;
            existingPet.Description = updatedPet.Description;
            existingPet.Species = updatedPet.Species;
            existingPet.Image = updatedPet.Image;
            existingPet.Price = updatedPet.Price;
            existingPet.Stock = updatedPet.Stock;
            existingPet.Unit = updatedPet.Unit;
            // Cập nhật các trường khác tương tự

            await appDbContext.SaveChangesAsync();

            return Ok(existingPet);
        }
        [HttpGet("Top10HighestId")]
        public async Task<ActionResult<List<Pet>>> GetTop10HighestIdPets()
        {
            // Sắp xếp danh sách các thú cưng theo ID giảm dần và lấy 10 thú cưng đầu tiên
            var top10Pets = await appDbContext.Pets
                .OrderByDescending(p => p.Id)
                .Take(10)
                .ToListAsync();

            if (top10Pets == null || top10Pets.Count == 0)
            {
                return NotFound();
            }

            return Ok(top10Pets);
        }

    }
}
