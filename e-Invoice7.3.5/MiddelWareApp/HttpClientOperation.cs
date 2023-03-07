using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiddelWareApp
{
    public class HttpClientOperation<T> : IDisposable, IHttpClientOperation<T> where T : class
    {
        private readonly HttpClient _client;
        public HttpClientOperation()
        {
            _client = new HttpClient();
        }
        public void Dispose()
        {
            _client.Dispose();
            //GC.Collect();
            GC.SuppressFinalize(this);
        }
        public async Task<T> GetAsyncResult(string url)
        {
            T result;
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<T>(content);
                Console.WriteLine("Success Get From URL:  " + url + " StatusCode :  " + response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{content}");
            }
            return result;
        }
        public async Task<TResult> PostAsyncRequwst<TResult>(string url, T ObjectRequestToPost)
        {
            TResult _result;
            var JsonContentToPost = JsonSerializer.Serialize(ObjectRequestToPost, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            var PostContent = new StringContent(JsonContentToPost, Encoding.UTF8, "application/json");
            var _respExecute = await _client.PostAsync(url, PostContent);
            if (_respExecute.IsSuccessStatusCode)
            {
                var _respContent = await _respExecute.Content.ReadAsStringAsync();
                _result = JsonSerializer.Deserialize<TResult>(_respContent);
                Console.WriteLine("Success Post Method : " + _respExecute.StatusCode);
            }
            else
            {
                var _respContent = await _respExecute.Content.ReadAsStringAsync();
                _respExecute.Content?.Dispose();
                Console.WriteLine("Failure Post Method : " + _respExecute.StatusCode);
                throw new HttpRequestException($"{_respContent}");
            }
            return _result;
        }
    }
}
