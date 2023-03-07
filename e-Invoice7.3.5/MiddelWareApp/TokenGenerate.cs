using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Entitys.Class;
using Newtonsoft.Json;

namespace MiddelWareApp
{
    public class TokenGenerate<T> : IDisposable, ITokenGenerate<T> where T : class
    {
        HttpClient _client;
        public TokenGenerate()
        {
            _client = new HttpClient();
        }
        public void Dispose()
        {
            _client.Dispose();
            GC.Collect();
        }
        public async Task<TResult> GenerateToken<TResult>(string _TokenBaseAddres)
        {
            try
            {
                ClientSecret lg = new ClientSecret()
                {
                    grant_type = "client_credentials",
                    client_id = "Put here the client ID",
                    client_secret = "Put Here the Client Secret"

                };
                TResult Result;
                var client_id = lg.client_id;
                var client_secret = lg.client_secret;
                var clientInfo = client_id + ":" + client_secret;
                var clientCreds = System.Text.Encoding.UTF8.GetBytes(clientInfo);
                _client.BaseAddress = new Uri(_TokenBaseAddres);
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));

                var postMessage = new Dictionary<string, string>();
                postMessage.Add("grant_type", "client_credentials");
                var request = new HttpRequestMessage(HttpMethod.Post, _TokenBaseAddres);
                request.Content = new FormUrlEncodedContent(postMessage);
                CancellationToken tc = new CancellationToken();
                var response = await _client.SendAsync(request, tc);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<TResult>(json);
                }
                else
                {
                    var _contentResp = await response.Content.ReadAsStringAsync();
                    response.Content?.Dispose();
                    throw new HttpRequestException($"{_contentResp}");

                }
                return Result;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"{ex}");
                //Console.WriteLine(ex.ToString());

            }
        }
    }
}
