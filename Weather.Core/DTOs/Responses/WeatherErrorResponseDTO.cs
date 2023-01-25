using Newtonsoft.Json;

namespace Weather.Core.Responses
{
    public class WeatherErrorResponseDTO
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}