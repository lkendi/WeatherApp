using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WeatherApp.Application.Contracts;
using WeatherApp.Infrastructure.Services;
using WeatherApp.Infrastructure.Settings;

namespace WeatherApp.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

        var weatherApiSettings = configuration.GetSection("WeatherApiSettings").Get<WeatherApiSettings>();
        services.Configure<WeatherApiSettings>(configuration.GetSection("WeatherApiSettings"));

            services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                client.BaseAddress = new Uri(weatherApiSettings.BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {weatherApiSettings.ApiKey}");
            });

            return services;
        }
    }
}
