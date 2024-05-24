using PayPal.Api;

namespace PetApps.api.ServicesAPi
{
    public class PayPalConfiguration
    {
        private readonly IConfiguration _configuration;

        public PayPalConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public APIContext GetAPIContext()
        {
            var config = GetConfig();
            var accessToken = new OAuthTokenCredential(config["clientId"], config["clientSecret"], config).GetAccessToken();
            return new APIContext(accessToken) { Config = config };
        }

        private Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string>
            {
                { "clientId", _configuration["PayPal:ClientId"] },
                { "clientSecret", _configuration["PayPal:ClientSecret"] },
                { "mode", _configuration["PayPal:Mode"] }
            };
        }
    }
}
