using System;
using System.Net.Http;
using Refit;

namespace Thunderstruck.UI.Api
{
    public class ApiService<T>:IDisposable
    {
        private HttpClient _httpClient;
        public T myService;
        public ApiService(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

            _httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
            myService = RestService.For<T>(_httpClient);

        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
