using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace BookServiceClientApp
{
    public abstract class HttpClientHelper<T> where T : class
    {
        protected Uri _baseAddress;

        public HttpClientHelper(string baseAddress)
        {
            if(baseAddress == null) 
                throw new ArgumentNullException(nameof(baseAddress));

            _baseAddress = new Uri(baseAddress);
        }

        private async Task<string> GetInternalAsync(string requestUri) {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                HttpResponseMessage resp = await client.GetAsync(requestUri);
                Console.WriteLine($"status from GET {resp.StatusCode}");
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadAsStringAsync();
            }
        }

        public async virtual Task<IEnumerable<T>?> GetAllAsync(string requestUri)
        { 
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        public async virtual Task<T?> GetAsync(string requestUri)
        {
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<T?>(json);
        }

        public async Task<T?> PostAsync(string uri, T item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                string json = JsonConvert.SerializeObject(item);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage resp = await client.PostAsync(uri, content);
                Console.WriteLine($"status from POST {resp.StatusCode}");
                resp.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {resp.Headers.Location}");

                json = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T?>(json);
            }
        }

        public async Task PutAsync(string uri, T item)
        {
            using (var client = new HttpClient()) { 
                client.BaseAddress = _baseAddress;
                string json = JsonConvert.SerializeObject(item);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage resp = await client.PutAsync(uri, content);
                Console.WriteLine($"status from PUT {resp.StatusCode}");
                resp.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(string uri)
        {
            using (var client = new HttpClient()) {
                client.BaseAddress = _baseAddress;
                HttpResponseMessage resp = await client.DeleteAsync(uri);
                Console.WriteLine($"status from DELETE {resp.StatusCode}");
                resp.EnsureSuccessStatusCode();
            }
        }
    }
}
