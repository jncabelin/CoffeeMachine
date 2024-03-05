using FluentResults;

namespace CoffeeMachine.Api.Services.Interfaces
{
	public interface IDateTimeProviderService
	{
		public Result<bool> IsAprilFirst();
		public Result<string> GetISO8601Now();
	}
}

