using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Models
{
    public class AuthResponse
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("reason")]
        public string? Reason { get; set; }
    }
}
