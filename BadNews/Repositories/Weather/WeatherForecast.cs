using System.Linq;

namespace BadNews.Repositories.Weather
{
    public class WeatherForecast
    {
        private const string defaultWeatherImageUrl = "/images/cloudy.png";

        public int TemperatureInCelsius { get; set; }
        public string IconUrl { get; set; }

        public static WeatherForecast CreateFrom(OpenWeatherForecast forecast)
        {
            return new WeatherForecast
            {
                TemperatureInCelsius = (int)forecast.Main.Temp,
                IconUrl = forecast.Weather.FirstOrDefault()?.IconUrl ?? defaultWeatherImageUrl
            };
        }
    }
}
