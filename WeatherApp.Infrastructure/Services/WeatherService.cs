using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Contracts;
using WeatherApp.Application.DTOs;
using WeatherApp.Infrastructure.Settings;

namespace WeatherApp.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiSettings _settings;

        public WeatherService(HttpClient httpClient, IOptions<WeatherApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<WeatherDto> GetCurrentWeather(string cityName)
        {
            var response = await _httpClient.GetAsync($"{_settings.BaseUrl}/weather?q={cityName}&appid={_settings.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weather = JsonSerializer.Deserialize<WeatherDto>(json);
                return weather ?? new WeatherDto();
            }

            throw new Exception("Could not get current weather data");
        }

        public async Task<List<WeatherDto>> GetWeatherForecast(string cityName)
        {
            var response = await _httpClient.GetAsync($"{_settings.BaseUrl}/forecast?q={cityName}&appid={_settings.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherForecast = JsonSerializer.Deserialize<List<WeatherDto>>(json);
                return weatherForecast ?? new List<WeatherDto>();
            }

            throw new Exception("Could not get weather forecast data");
        }

    }
}