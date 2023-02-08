using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace CoolGuys.Helpers
{
    public class FlurlClientFactory : FlurlClientFactoryBase
    {
        protected override IFlurlClient Create(Url url)
        {
            var httpHandler = new HttpClientHandler();
      
            httpHandler.ServerCertificateCustomValidationCallback += (message, certificate2, arg3, arg4) =>  true; 
          
            var cl = new HttpClient(httpHandler); 
            cl.BaseAddress = url.ToUri();
            var client = new FlurlClient(cl)
                .WithTimeout(15)
                .WithHeader("Accept", "application/json");
            return client;
        }

        protected override string GetCacheKey(Url url)
        {
            return url.ToString();
        }
    }
}