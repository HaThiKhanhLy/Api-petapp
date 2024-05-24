using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public OrdersController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Orders>>> GetOrdersWithUsers()
        {
            var ordersWithUsers = await appDbContext.Orders
                .Include(order => order.User) // Kích hoạt tự động load thông tin người dùng
                .ToListAsync();

            if (ordersWithUsers == null)
            {
                return NotFound();
            }

            return Ok(ordersWithUsers);
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Orders>>> GetOrdersByUserId(int userId)
        {
            var orders = await appDbContext.Orders.Include(o => o.User).Where(o => o.UserId == userId).ToListAsync();
            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }
        [HttpPost]
        public async Task<ActionResult<Orders>> AddOrder(int userId, double total, DateTime date)
        {
            // Kiểm tra xem người dùng có tồn tại không
            var user = await appDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Tạo một đối tượng đơn hàng từ thông tin được truyền vào
            var order = new Orders
            {
                UserId = userId,
                Total = total,
                Date = date
            };

            // Thêm đơn hàng vào context của ứng dụng
            appDbContext.Orders.Add(order);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await appDbContext.SaveChangesAsync();

            // Trả về đơn hàng đã tạo thành công
            return Ok(order);
        }
        //[HttpPut("{orderId}")]
        //public async Task<ActionResult<Orders>> UpdateOrderStatus(int orderId, [FromBody] int newStatus)
        //{
        //    // Tìm kiếm đơn hàng theo ID
        //    var order = await appDbContext.Orders.FindAsync(orderId);
        //    if (order == null)
        //    {
        //        return NotFound("Order not found");
        //    }

        //    // Cập nhật trạng thái của đơn hàng
        //    order.Status = newStatus;

        //    // Lưu các thay đổi vào cơ sở dữ liệu
        //    await appDbContext.SaveChangesAsync();

        //    // Trả về kết quả thành công
        //    return Ok(order);
        //}
        [HttpPut("{orderId}/{newStatus}")]
        public async Task<ActionResult<Orders>> UpdateOrderStatus(int orderId, int newStatus)
        {
            // Tìm kiếm đơn hàng theo ID
            var order = await appDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            // Cập nhật trạng thái của đơn hàng
            order.Status = newStatus;

            // Lưu các thay đổi vào cơ sở dữ liệu
            await appDbContext.SaveChangesAsync();

            // Trả về kết quả thành công
            return Ok(order);
        }


    }

}
