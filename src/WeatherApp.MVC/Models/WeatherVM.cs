namespace WeatherApp.MVC.Models
{
    public class WeatherVM
    {
        public string CityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string WeatherDescription { get; set; } = string.Empty;
        public string MainWeather { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public DateTime DateTime { get; set; }
        public List<WeatherForecastVM> Forecasts { get; set; } = new List<WeatherForecastVM>();
    }

}