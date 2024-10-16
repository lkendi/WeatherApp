using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Contracts;
using WeatherApp.MVC.Models;

namespace WeatherApp.MVC.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index(String city)
        {
            var currentWeather = await _weatherService.GetCurrentWeather(city);
            var weatherForecasts = await _weatherService.GetWeatherForecast(city);

            
            var viewModel = new WeatherVM
            {
                CityName = currentWeather.CityName,
                Country = currentWeather.Country,
                Temperature = currentWeather.Temperature,
                MainWeather = currentWeather.MainWeather,
                WeatherDescription = currentWeather.WeatherDescription,
                Icon = currentWeather.Icon,
                WindSpeed = currentWeather.WindSpeed,
                Humidity = currentWeather.Humidity,
                DateTime = currentWeather.DateTime,
                Pressure = currentWeather.Pressure,
                Forecasts = weatherForecasts.Select(f => new WeatherForecastVM
                {
                    ForecastDateTime = f.DateTime,
                    ForecastTemperature = f.Temperature,
                    ForecastMainWeather = f.MainWeather,
                    ForecastDescription = f.WeatherDescription,
                    ForecastIcon = f.Icon
                }).ToList()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

