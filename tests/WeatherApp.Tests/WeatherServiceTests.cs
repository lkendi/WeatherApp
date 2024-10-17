using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using WeatherApp.Application.Contracts;
using WeatherApp.Application.DTOs;
using WeatherApp.Infrastructure.Services;
using WeatherApp.Infrastructure.Settings;
using Xunit;

namespace WeatherApp.Tests;

public class WeatherServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly WeatherService _weatherService;

    public WeatherServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        var settings = Options.Create(new WeatherApiSettings
        {
            BaseUrl = "http://api.openweathermap.org/data/2.5",
            ApiKey = "fake-api-key"
        });

        _weatherService = new WeatherService(_httpClient, settings);
    }
    
    [Fact]
    public async Task GetCurrentWeather_ReturnsWeatherDto_WhenApiResponseIsValid()
    {
        var responseContent = "{\"coord\":{\"lon\":-0.1257,\"lat\":51.5085},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03d\"}],\"base\":\"stations\",\"main\":{\"temp\":290.99,\"feels_like\":290.58,\"temp_min\":289.51,\"temp_max\":292.05,\"pressure\":1011,\"humidity\":67,\"sea_level\":1011,\"grnd_level\":1007},\"visibility\":10000,\"wind\":{\"speed\":3.13,\"deg\":146,\"gust\":4.02},\"clouds\":{\"all\":40},\"dt\":1729178942,\"sys\":{\"type\":2,\"id\":2075535,\"country\":\"GB\",\"sunrise\":1729146492,\"sunset\":1729184582},\"timezone\":3600,\"id\":2643743,\"name\":\"London\",\"cod\":200}";
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent)
            });

         var result = await _weatherService.GetCurrentWeather("London");

        Assert.NotNull(result);
        Assert.Equal("London", result.CityName);
        Assert.Equal("GB", result.Country);
        Assert.Equal(290.99, result.Temperature);
    }

}