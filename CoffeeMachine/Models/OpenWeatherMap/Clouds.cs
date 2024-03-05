using System;
using Newtonsoft.Json;

namespace CoffeeMachine.Api.Models.OpenWeatherMap
{
	public class Clouds
	{
        [JsonProperty("all")]
        public double All { get; set; }
    }
}

