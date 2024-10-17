namespace WeatherApp.MVC.Models
{
    public class WeatherForecastVM
    {
        public DateTime ForecastDateTime { get; set; }
        public double ForecastTemperature { get; set; }
        public string ForecastMainWeather { get; set; } = string.Empty;
        public string ForecastDescription { get; set; } = string.Empty;
        public string ForecastIcon { get; set; } = string.Empty;
    }
}