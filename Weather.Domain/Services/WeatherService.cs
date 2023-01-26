namespace Weather.Domain.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings AppSettings;

        public WeatherService(IHttpClientFactory httpClientFactory,
                              IOptions<AppSettings> appSettings)
        {
            this._httpClientFactory = httpClientFactory;
            this.AppSettings = appSettings.Value;
        }



        public async Task<(HttpStatusCode status, dynamic response)> GetCurrentAsync(string locationName)
        {
            var locationQuery = $"current.json?key={this.AppSettings.WeatherKey}&q={locationName}&aqi=no";
            var weatherClient = this._httpClientFactory.CreateClient("weatherAPI");

            var weatherResponse = await weatherClient.GetAsync(locationQuery);

            if (!weatherResponse.IsSuccessStatusCode)
            {
                var jsonStringError = await weatherResponse.Content.ReadAsStringAsync();
                var responseDataError = JsonConvert.DeserializeObject<WeatherErrorResponseDTO>(jsonStringError);

                return (HttpStatusCode.NotFound, responseDataError.Error.Message);
            }
            
            var jsonString = await weatherResponse.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<WeatherCurrentResponseDTO>(jsonString);

            return (HttpStatusCode.OK, responseData); 
        }



        public async Task<(HttpStatusCode status, dynamic response)> GetCurrentByLatLongAsync(double latitude, double longtitude)
        {
            var locationQuery = $"current.json?key={this.AppSettings.WeatherKey}&q={latitude},{longtitude}&aqi=no";
            var weatherClient = this._httpClientFactory.CreateClient("weatherAPI");

            var weatherResponse = await weatherClient.GetAsync(locationQuery);
            
            if (!weatherResponse.IsSuccessStatusCode)
            {
                var jsonStringError = await weatherResponse.Content.ReadAsStringAsync();
                var responseDataError = JsonConvert.DeserializeObject<WeatherErrorResponseDTO>(jsonStringError);

                return (HttpStatusCode.NotFound, responseDataError.Error.Message);
            }
            
            var jsonString = await weatherResponse.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<WeatherCurrentResponseDTO>(jsonString);

            return (HttpStatusCode.OK, responseData); 
        }



        public async Task<(HttpStatusCode status, dynamic response)> GetForeCastAsync(string locationName, int day)
        {
            if (day > 10)
            {
                day = 10;
            }

            var locationQuery = $"forecast.json?key={this.AppSettings.WeatherKey}&q={locationName}&days={day}&aqi=no&alerts=no";
            var weatherClient = this._httpClientFactory.CreateClient("weatherAPI");

            var weatherResponse = await weatherClient.GetAsync(locationQuery);
            
            if (!weatherResponse.IsSuccessStatusCode)
            {
                var jsonStringError = await weatherResponse.Content.ReadAsStringAsync();
                var responseDataError = JsonConvert.DeserializeObject<WeatherErrorResponseDTO>(jsonStringError);

                return (HttpStatusCode.NotFound, responseDataError.Error.Message);
            }
            
            var jsonString = await weatherResponse.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<WeatherForecastResponseDTO>(jsonString);

            return (HttpStatusCode.OK, responseData); 
        }
    }
}