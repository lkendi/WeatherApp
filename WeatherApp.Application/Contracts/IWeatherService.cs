using WeatherApp.Application.DTOs;

namespace WeatherApp.Application.Contracts
{
    public interface IWeatherService
    {
        public Task<WeatherDto> GetCurrentWeather(string cityName);
        public Task<List<WeatherDto>> GetWeatherForecast(string cityName);
    }
}