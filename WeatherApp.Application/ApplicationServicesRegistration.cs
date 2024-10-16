using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Application
{

    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}