using System.ComponentModel.DataAnnotations;

namespace Weather.Core.Requests
{
    public class WeatherLatLongRequestDTO
    {
        [Required]
        public double latitude { get; set; }

        [Required]
        public double longtitude { get; set; }
    }
}