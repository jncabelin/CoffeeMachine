using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
    public class Coordinates
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}

