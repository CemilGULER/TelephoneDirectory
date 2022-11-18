using Newtonsoft.Json;
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
            this.httpClient.Timeout = TimeSpan.FromMinutes(1);
        }

     

        public async Task<TResult> GetAsync<TResult>(string requestUri, CancellationToken cancellationToken = default)
        {
            var resp = await MakeApiRequest(httpClient.GetAsync, requestUri, cancellationToken);
            if (resp.IsSuccessStatusCode)
            {
                var contenet = await resp.Content.ReadAsStringAsync();
                if (contenet == null)
                {
                    throw new Exception("contenet boş");
                }
                else
                {
                    return JsonConvert.DeserializeObject<TResult>(contenet);
                }

            }
            else
            {
                throw new Exception("İstek gönderilirken hata oluştu");
            }
        }
        


        public async Task<TResult> PostAsync<TContent, TResult>(string requestUri, TContent obj, CancellationToken cancellationToken = default)
        {
            HttpContent content;
            if (obj is HttpContent httpContent)
            {
                content = obj as HttpContent;
            }
            else
            {
                content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            }
            var resp = await MakeApiRequest(httpClient.PostAsync, requestUri,content, cancellationToken);
            if (resp.IsSuccessStatusCode)
            {
                var contenet = await resp.Content.ReadAsStringAsync();
                if (contenet == null)
                {
                    throw new Exception("contenet boş");
                }
                else
                {
                    return JsonConvert.DeserializeObject<TResult>(contenet);
                }

            }
            else
            {
                throw new Exception("İstek gönderilirken hata oluştu");
            }

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
        private async Task<HttpResponseMessage> MakeApiRequest(Func<string, HttpContent, CancellationToken, Task<HttpResponseMessage>> callMethod, string requestUri, HttpContent content,  CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                response = await callMethod(requestUri,content, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw new OperationCanceledException();
            }

            return response;
           
        }



    }
}
