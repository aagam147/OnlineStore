using Newtonsoft.Json;

namespace OnlineStore.ViewModel
{
    public class LoginSuccessViewModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
