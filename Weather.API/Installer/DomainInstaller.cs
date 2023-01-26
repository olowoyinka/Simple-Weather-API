namespace Weather.API.Installer
{
    public class DomainInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWeatherService, WeatherService>();

            services.AddHttpClient("weatherAPI", client =>
            {
                client.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
            });
        }
    }
}