using System.Net;
using CoffeeMachine.Api.Models.OpenWeatherMap;
using CoffeeMachine.Api.Services.Interfaces;
using Newtonsoft.Json;
using FluentResults;

namespace CoffeeMachine.Api.Services
{
    public class OpenWeatherMapService : IWeatherMapService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private ILogger<OpenWeatherMapService> _logger;


        public OpenWeatherMapService(IConfiguration configuration, ILogger<OpenWeatherMapService> logger)
        {
            if (configuration == null)
                throw new ArgumentNullException("Configuration cannot be null.");
            if (logger == null)
                throw new ArgumentNullException("Logger cannot be null.");

            _apiKey = configuration.GetSection("WeatherAPI:APIkey").Value;
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"http://api.openweathermap.org/data/2.5/");
        }

        public async Task<Result<(HttpStatusCode,CurrentWeather)>> GetCurrentWeatherAsync(string location)
        {
            if (location == null || location == string.Empty)
            {
                _logger.LogInformation("Location was invalid.");
                return Result.Fail("Location was invalid.");
            }

            // Query for Current Weather Condition
            var query = $"weather?q={location}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(query);


            // Check HttpStatus Code from Client
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    // Status is OK Deserialize Response
                    var currentWeatherString = await response.Content.ReadAsStringAsync();
                    var currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(currentWeatherString);
                    if (currentWeather == null)
                    {
                        _logger.LogError("Cannot deserialize CurrentWeather", currentWeatherString);
                        return Result.Fail("Cannot deserialize CurrentWeather");
                    }

                    _logger.LogInformation("Current Weather:", currentWeather);
                    return Result.Ok((response.StatusCode,currentWeather));

                default:
                    // API Returned Error
                    return Result.Fail(response.ReasonPhrase);    
            }
        }
    }
}