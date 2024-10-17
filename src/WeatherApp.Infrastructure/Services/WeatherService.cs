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
            if (cityName == null)
            {
                cityName = "Nairobi";
            }

            var response = await _httpClient.GetAsync($"{_settings.BaseUrl}/weather?q={cityName}&units=metric&appid={_settings.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonDocument.Parse(json);


                var weather = new WeatherDto
                {
                    CityName = weatherData.RootElement.GetProperty("name").GetString(),
                    Temperature = weatherData.RootElement.GetProperty("main").GetProperty("temp").GetDouble(),
                    Country = weatherData.RootElement.GetProperty("sys").GetProperty("country").GetString(),
                    Humidity = weatherData.RootElement.GetProperty("main").GetProperty("humidity").GetInt32(),
                    Pressure = weatherData.RootElement.GetProperty("main").GetProperty("pressure").GetInt32(),
                    WeatherDescription = weatherData.RootElement.GetProperty("weather").EnumerateArray().FirstOrDefault().GetProperty("description").GetString(),
                    Icon = weatherData.RootElement.GetProperty("weather").EnumerateArray().FirstOrDefault().GetProperty("icon").GetString(),
                    WindSpeed = weatherData.RootElement.GetProperty("wind").GetProperty("speed").GetDouble(),
                    DateTime = DateTime.Now,
                    MainWeather = weatherData.RootElement.GetProperty("weather").EnumerateArray().FirstOrDefault().GetProperty("main").GetString()
                    
                };
                
                weather.Icon = $"http://openweathermap.org/img/wn/{weather.Icon}@2x.png";
                return weather;
            }

            throw new Exception("Could not get current weather data");
        }

        public async Task<List<WeatherDto>> GetWeatherForecast(string cityName)
        {
            if (cityName == null)
            {
                cityName = "Nairobi";
            }

            var response = await _httpClient.GetAsync($"{_settings.BaseUrl}/forecast?q={cityName}&units=metric&appid={_settings.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonDocument.Parse(json);
                var weatherForecast = new List<WeatherDto>();

                foreach (var forecast in weatherData.RootElement.GetProperty("list").EnumerateArray())
                {
                    var weather = new WeatherDto
                    {
                        CityName = cityName,
                        Country = weatherData.RootElement.GetProperty("city").GetProperty("country").GetString(),
                        Temperature = forecast.GetProperty("main").GetProperty("temp").GetDouble(),
                        MainWeather = forecast.GetProperty("weather")[0].GetProperty("main").GetString(),
                        WeatherDescription = forecast.GetProperty("weather")[0].GetProperty("description").GetString(),
                        Icon = forecast.GetProperty("weather")[0].GetProperty("icon").GetString(),
                        WindSpeed = forecast.GetProperty("wind").GetProperty("speed").GetDouble(),
                        Humidity = forecast.GetProperty("main").GetProperty("humidity").GetInt32(),
                        Pressure = forecast.GetProperty("main").GetProperty("pressure").GetInt32(),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(forecast.GetProperty("dt").GetInt64()).DateTime

                    };
                    weather.Icon = $"http://openweathermap.org/img/wn/{weather.Icon}@2x.png";
                    weatherForecast.Add(weather);
                }

                return weatherForecast;
            }

            throw new Exception("Could not get weather forecast data");
        }

    }
}