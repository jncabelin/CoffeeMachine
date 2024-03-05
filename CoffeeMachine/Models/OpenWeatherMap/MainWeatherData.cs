using System;
using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
    public class MainWeatherData
    {
        [JsonProperty("temp")]
        public double CurrentTemperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TemperatureMin { get; set; }

        [JsonProperty("temp_max")]
        public double TemperatureMax { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }
}