using PetApps.api.Models;

namespace PetApps.api.ServicesAPi
{
    public interface IVnPayServices
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collection);
    }
}
