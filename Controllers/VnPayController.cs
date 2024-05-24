using Microsoft.AspNetCore.Mvc;
using PetApps.api.Models;
using PetApps.api.ServicesAPi;
using System.Threading.Tasks;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayServices _vnPayServices;

        public VnPayController(IVnPayServices vnPayServices)
        {
            _vnPayServices = vnPayServices;
        }

        [HttpPost("CreatePaymentUrl")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VnPaymentRequestModel model)
        {
            var paymentUrl = _vnPayServices.CreatePaymentUrl(HttpContext, model);
            return Ok(paymentUrl);
        }

        [HttpGet("VnPayReturn")]
        public IActionResult VnPayReturn([FromQuery] IQueryCollection query)
        {
            var response = _vnPayServices.PaymentExecute(query);
            return Redirect($"PetsApp://paymentresult?vnp_ResponseCode={response.VnPayResponseCode}&orderId={response.OrderId}");
        }
    }
}
