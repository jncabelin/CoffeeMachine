using System;
using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
	public class Wind
	{
        [JsonProperty("speed")]
        public Double Speed { get; set; }

        [JsonProperty("deg")]
        public Double Degree { get; set; }
    }
}

