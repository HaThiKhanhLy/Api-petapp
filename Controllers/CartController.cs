using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public CartController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Cart>>> Get() => Ok(await appDbContext.Cart.ToListAsync());
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Cart>>> GetByUserId(int userId)
        {
            // Lấy thông tin về giỏ hàng
            var carts = await appDbContext.Cart
                .Where(c => c.UserID == userId)
                .Include(c => c.Pet) // Include thú cưng liên quan đến giỏ hàng
                .ToListAsync();

            return Ok(carts); // Trả về mảng sản phẩm trong giỏ hàng, có thể là mảng rỗng
        }



        [HttpPost]
        public async Task<ActionResult<Cart>> Add(Cart cart)
        {
            if (cart != null)
            {
                var result = appDbContext.Cart.Add(cart).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
        [HttpDelete("{userId}/{petId}")]
        public async Task<ActionResult> DeleteCartItem(int userId, int petId)
        {
            var cartItem = await appDbContext.Cart.FirstOrDefaultAsync(c => c.UserID == userId && c.PetID == petId);
            if (cartItem == null)
            {
                return NotFound($"Cart item not found for user with ID: {userId} and pet with ID: {petId}");
            }

            appDbContext.Cart.Remove(cartItem);
            await appDbContext.SaveChangesAsync();

            return Ok($"Cart item successfully deleted for user with ID: {userId} and pet with ID: {petId}");
        }

        [HttpPut("{userId}/{petId}")]
        public async Task<ActionResult<Cart>> UpdateQuantity(int userId, int petId, [FromBody] int newQuantity)
        {
            // Tìm kiếm mục Cart theo userId và petId
            var cartItem = await appDbContext.Cart.FirstOrDefaultAsync(c => c.UserID == userId && c.PetID == petId);

            // Nếu không tìm thấy mục Cart
            if (cartItem == null)
            {
                return NotFound("Cart item not found for this user and pet.");
            }

            // Cập nhật số lượng mới
            cartItem.Quantity = newQuantity;

            // Lưu thay đổi vào cơ sở dữ liệu
            await appDbContext.SaveChangesAsync();

            return Ok(cartItem);
        }

    }
}
