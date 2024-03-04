using FluentResults;

namespace CoffeeMachine.Api.Services.Interfaces
{
    public interface ICoffeeMachineService
    {
        public Task<Result<int>> BrewProduct(int productId = 1);
    }
}