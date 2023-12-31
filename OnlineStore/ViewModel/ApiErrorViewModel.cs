using Newtonsoft.Json;

namespace OnlineStore.ViewModel
{
    public class ApiErrorViewModel
    {
        [JsonProperty("result")]
        public string Message { get; set; }
    }

    public class ApiDefaultErrorViewModel
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class ApiErrorModel
    {
        [JsonProperty("result")]
        public List<ApiDefaultErrorViewModel> Result { get; set; }
    }


}
