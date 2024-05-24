using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PetApps.api.ServicesAPi;

namespace PetApps.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PayPalConfiguration _payPalConfiguration;

        public PaymentsController(PayPalConfiguration payPalConfiguration)
        {
            _payPalConfiguration = payPalConfiguration;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment()
        {
            var apiContext = _payPalConfiguration.GetAPIContext();
            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Transaction description.",
                        amount = new Amount { currency = "USD", total = "10.00" }
                    }
                },
                //redirect_urls = new RedirectUrls
                //{
                //    cancel_url = "http://your_cancel_url",
                //    return_url = "http://your_return_url"
                //}
            };

            var createdPayment = payment.Create(apiContext);

            var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;

            return Ok(new { ApprovalUrl = approvalUrl });
        }

        [HttpPost("execute-payment")]
        public IActionResult ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = _payPalConfiguration.GetAPIContext();
            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new Payment { id = paymentId };

            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return Ok(executedPayment);
        }
    }
}
