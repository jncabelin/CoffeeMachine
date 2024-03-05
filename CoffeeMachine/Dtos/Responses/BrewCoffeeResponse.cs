using Newtonsoft.Json;

namespace CoffeeMachine.Api.Dtos.Responses
{
	public class BrewCoffeeResponse
	{
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("prepared")]
        public string Prepared { get; set; }

		public BrewCoffeeResponse()
		{
		}

		public BrewCoffeeResponse(string statusMessage, string time)
		{
			this.Message = statusMessage;
			this.Prepared = time;
		}
	}
}

