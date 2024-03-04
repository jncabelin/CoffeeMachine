using Newtonsoft.Json;

namespace CoffeeMachine.Api.Dtos.Responses
{
	public class BrewCoffeeResponse
	{
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("prepared")]
        public DateTimeOffset Prepared { get; set; }

		public BrewCoffeeResponse()
		{
		}

		public BrewCoffeeResponse(string statusMessage, DateTimeOffset time)
		{
			this.Message = statusMessage;
			this.Prepared = time;
		}
	}
}

