using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BadNews.Repositories.Weather
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private const string defaultWeatherImageUrl = "/images/cloudy.png";

        private readonly Random random = new Random();
        private readonly OpenWeatherClient weatherClient;
        public WeatherForecastRepository(IOptions<OpenWeatherOptions> weatherOptions)
        {
            var apiKey = weatherOptions?.Value.ApiKey;
            weatherClient = new OpenWeatherClient(apiKey);
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync()
        {
            var openWeatherForecast = await weatherClient.GetWeatherFromApiAsync();
            if (openWeatherForecast is null)
            {
                return BuildRandomForecast();
            }
            var weatherForecast = WeatherForecast.CreateFrom(openWeatherForecast);
            return weatherForecast;
        }

        private WeatherForecast BuildRandomForecast()
        {
            var temperature = random.Next(-20, 20 + 1);
            return new WeatherForecast
            {
                TemperatureInCelsius = temperature,
                IconUrl = defaultWeatherImageUrl
            };
        }
    }
}
