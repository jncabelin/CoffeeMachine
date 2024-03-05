using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
    public class Weather
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}

