using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Common.Http
{
    public abstract class TelephoneDirectoryHttpClientBase
    {
        private readonly HttpClient httpClient;
        public TelephoneDirectoryHttpClientBase(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default)
        {
            return MakeApiRequest(httpClient.GetAsync, requestUri, cancellationToken);
           
        }

        private async Task<HttpResponseMessage> MakeApiRequest(Func<string, CancellationToken, Task<HttpResponseMessage>> callMethod, string requestUri, CancellationToken cancellationToken)
        {
           
            HttpResponseMessage response;
            try
            {
                response = await callMethod(requestUri, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw new OperationCanceledException();
            }

            return response;
        }

       
       
    }
}
