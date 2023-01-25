using Newtonsoft.Json;

namespace Weather.Core.Responses
{
    public class WeatherForecastResponseDTO
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("forecast")]
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("forecastday")]
        public List<Forecastday> Forecastday { get; set; }
    }

    public class Forecastday
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("date_epoch")]
        public long DateEpoch { get; set; }

        [JsonProperty("day")]
        public Day Day { get; set; }

        [JsonProperty("astro")]
        public Astro Astro { get; set; }

        [JsonProperty("hour")]
        public List<Hour> Hour { get; set; }
    }

    public class Astro
    {
        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("moonrise")]
        public string Moonrise { get; set; }

        [JsonProperty("moonset")]
        public string Moonset { get; set; }

        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }

        [JsonProperty("moon_illumination")]
        public string MoonIllumination { get; set; }
    }

    public class Day
    {
        [JsonProperty("maxtemp_c")]
        public double MaxtempC { get; set; }

        [JsonProperty("maxtemp_f")]
        public double MaxtempF { get; set; }

        [JsonProperty("mintemp_c")]
        public double MintempC { get; set; }

        [JsonProperty("mintemp_f")]
        public double MintempF { get; set; }

        [JsonProperty("avgtemp_c")]
        public double AvgtempC { get; set; }

        [JsonProperty("avgtemp_f")]
        public double AvgtempF { get; set; }

        [JsonProperty("maxwind_mph")]
        public double MaxwindMph { get; set; }

        [JsonProperty("maxwind_kph")]
        public double MaxwindKph { get; set; }

        [JsonProperty("totalprecip_mm")]
        public double TotalprecipMm { get; set; }

        [JsonProperty("totalprecip_in")]
        public double TotalprecipIn { get; set; }

        [JsonProperty("totalsnow_cm")]
        public long TotalsnowCm { get; set; }

        [JsonProperty("avgvis_km")]
        public double AvgvisKm { get; set; }

        [JsonProperty("avgvis_miles")]
        public long AvgvisMiles { get; set; }

        [JsonProperty("avghumidity")]
        public long Avghumidity { get; set; }

        [JsonProperty("daily_will_it_rain")]
        public long DailyWillItRain { get; set; }

        [JsonProperty("daily_chance_of_rain")]
        public long DailyChanceOfRain { get; set; }

        [JsonProperty("daily_will_it_snow")]
        public long DailyWillItSnow { get; set; }

        [JsonProperty("daily_chance_of_snow")]
        public long DailyChanceOfSnow { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("uv")]
        public long Uv { get; set; }
    }

    public class Hour
    {
        [JsonProperty("time_epoch")]
        public long TimeEpoch { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("temp_c")]
        public double TempC { get; set; }

        [JsonProperty("temp_f")]
        public double TempF { get; set; }

        [JsonProperty("is_day")]
        public long IsDay { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("wind_mph")]
        public double WindMph { get; set; }

        [JsonProperty("wind_kph")]
        public double WindKph { get; set; }

        [JsonProperty("wind_degree")]
        public long WindDegree { get; set; }

        [JsonProperty("wind_dir")]
        public string WindDir { get; set; }

        [JsonProperty("pressure_mb")]
        public long PressureMb { get; set; }

        [JsonProperty("pressure_in")]
        public double PressureIn { get; set; }

        [JsonProperty("precip_mm")]
        public double PrecipMm { get; set; }

        [JsonProperty("precip_in")]
        public double PrecipIn { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("cloud")]
        public long Cloud { get; set; }

        [JsonProperty("feelslike_c")]
        public double FeelslikeC { get; set; }

        [JsonProperty("feelslike_f")]
        public double FeelslikeF { get; set; }

        [JsonProperty("windchill_c")]
        public double WindchillC { get; set; }

        [JsonProperty("windchill_f")]
        public double WindchillF { get; set; }

        [JsonProperty("heatindex_c")]
        public double HeatindexC { get; set; }

        [JsonProperty("heatindex_f")]
        public double HeatindexF { get; set; }

        [JsonProperty("dewpoint_c")]
        public double DewpointC { get; set; }

        [JsonProperty("dewpoint_f")]
        public double DewpointF { get; set; }

        [JsonProperty("will_it_rain")]
        public long WillItRain { get; set; }

        [JsonProperty("chance_of_rain")]
        public long ChanceOfRain { get; set; }

        [JsonProperty("will_it_snow")]
        public long WillItSnow { get; set; }

        [JsonProperty("chance_of_snow")]
        public long ChanceOfSnow { get; set; }

        [JsonProperty("vis_km")]
        public long VisKm { get; set; }

        [JsonProperty("vis_miles")]
        public long VisMiles { get; set; }

        [JsonProperty("gust_mph")]
        public double GustMph { get; set; }

        [JsonProperty("gust_kph")]
        public double GustKph { get; set; }

        [JsonProperty("uv")]
        public long Uv { get; set; }
    }
}