using System;
using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
	public class Sys
	{
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }
}

