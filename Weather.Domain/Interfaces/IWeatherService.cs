namespace Weather.Domain.Interfaces
{
    public interface IWeatherService
    {
        Task<(HttpStatusCode status, dynamic response)> GetCurrentAsync(string locationName);

        Task<(HttpStatusCode status, dynamic response)> GetCurrentByLatLongAsync(double latitude, double longtitude);

        Task<(HttpStatusCode status, dynamic response)> GetForeCastAsync(string locationName, int day);
    }
}