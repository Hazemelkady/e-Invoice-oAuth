using Newtonsoft.Json;

namespace Entitys.Class
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
        [JsonProperty("token_type")]
        public string token_type { get; set; }
    }
}
