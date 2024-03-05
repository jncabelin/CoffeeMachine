using System.Net;
using CoffeeMachine.Api.Models.OpenWeatherMap;
using FluentResults;

namespace CoffeeMachine.Api.Services.Interfaces
{
	public interface IWeatherMapService
	{
        public Task<Result<CurrentWeather>> GetCurrentWeatherAsync(string location);

    }
}

