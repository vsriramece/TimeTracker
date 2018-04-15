using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reviso.Common.HttpAccess
{
    public class ApiClient : IApiClient
    {
        private readonly Uri baseAddress;
        private readonly HttpClient client;
        private const string JSON_CONTENT_TYPE = "application/json";
        public ApiClient(string baseAddress) 
        {
            this.baseAddress = new Uri(baseAddress);
            this.client = new HttpClient() { BaseAddress = this.baseAddress };
        }


        public async Task<TOutputType> DeleteAsync<TOutputType>(string requestUri)
        {
            HttpRequestMessage resourceRequest = CreateRequest(HttpMethod.Delete, requestUri, null);
            HttpResponseMessage response = await client.SendAsync(resourceRequest).ConfigureAwait(false);
            return await ProcessResponse<TOutputType>(response);
        }

        public async Task<TOutputType> GetAsync<TOutputType>(string requestUri)
        {
            HttpRequestMessage resourceRequest = CreateRequest(HttpMethod.Get,requestUri,null);
            HttpResponseMessage response = await client.SendAsync(resourceRequest).ConfigureAwait(false);
            return await ProcessResponse<TOutputType>(response);
        }

        public async Task<TOutputType> PostAsync<TInputType, TOutputType>(string requestUri, TInputType input)
        {
            HttpRequestMessage resourceRequest = CreateRequest(HttpMethod.Post, requestUri, input);
            HttpResponseMessage response = await client.SendAsync(resourceRequest).ConfigureAwait(false);
            return await ProcessResponse<TOutputType>(response);
        }

        public async Task<TOutputType> PutAsync<TInputType, TOutputType>(string requestUri, TInputType input)
        {
            HttpRequestMessage resourceRequest = CreateRequest(HttpMethod.Put, requestUri, input);
            HttpResponseMessage response = await client.SendAsync(resourceRequest).ConfigureAwait(false);
            return await ProcessResponse<TOutputType>(response);
        }


        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    // To do - Throw custom exception
                    string respMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Unable to {response.RequestMessage.Method} data to API service at URL: {response.RequestMessage.RequestUri}. Response message:{respMessage}.");
                }
                return default(T);
            }
            string data = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }

        private HttpRequestMessage CreateRequest(HttpMethod verb, string uri, object input)
        {
            HttpRequestMessage request = new HttpRequestMessage(verb,
                $"{this.baseAddress.ToString().TrimEnd('/')}/{uri.TrimStart('/')}");
            if (input != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, JSON_CONTENT_TYPE);
            }
            return request;
        }
    }
}
