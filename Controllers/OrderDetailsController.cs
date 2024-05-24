using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Data;
using PetApps.api.Models;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public OrderDetailsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDetails>>> Get() => Ok(await appDbContext.OrderDetail.ToListAsync());
        //[HttpGet("{orderId}")]
        //public async Task<ActionResult<List<OrderDetails>>> GetOrderDetails(int orderId)
        //{
        //    // Lấy chi tiết đơn hàng dựa trên ID của đơn hàng
        //    var orderDetails = await appDbContext.OrderDetail
        //        .Where(od => od.OrderID == orderId)
        //        .ToListAsync();

        //    if (orderDetails == null || orderDetails.Count == 0)
        //    {
        //        return NotFound(); // Trả về HTTP 404 Not Found nếu không tìm thấy chi tiết đơn hàng
        //    }

        //    return Ok(orderDetails); // Trả về danh sách chi tiết đơn hàng
        //}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<List<OrderDetails>>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await appDbContext.OrderDetail
                                        .Include(od => od.Pet)
                                        .Include(od => od.Order)
                                        .Where(od => od.OrderID == orderId)
                                        .ToListAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetails>> Add(OrderDetails orderDetails)
        {
            if (orderDetails != null)
            {
                var result = appDbContext.OrderDetail.Add(orderDetails).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");

        }
    }
}
