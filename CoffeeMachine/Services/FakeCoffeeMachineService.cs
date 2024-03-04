using CoffeeMachine.Api.Models.CoffeeMachine;
using CoffeeMachine.Api.Services.Interfaces;
using FluentResults;

namespace CoffeeMachine.Api.Services
{
    public class FakeCoffeeMachineService : ICoffeeMachineService
    {
        private static List<CoffeeMachineProduct> _products;
        private ILogger<FakeCoffeeMachineService> _logger;

        public FakeCoffeeMachineService(ILogger<FakeCoffeeMachineService> logger)
        {
            if (logger == null)
                throw new ArgumentNullException("Logger cannot be null.");
            _logger = logger;
            _products = new List<CoffeeMachineProduct>
            {
                new CoffeeMachineProduct { Id = 1, OrderCount = 0 },
                new CoffeeMachineProduct { Id = 2, OrderCount = 0 },
                new CoffeeMachineProduct { Id = 3, OrderCount = 0 },
            };
        }

        // Returns the order count for specific product
        public async Task<Result<int>> BrewProduct(int productId = 1)
        {
            await Task.CompletedTask;
            var findProduct = _products.Find(x => x.Id == productId);
            if (findProduct == null)
            {
                _logger.LogInformation($"Product ID: {productId} was not found.");
                return Result.Fail($"Product ID: {productId} was not found.");
            }

            findProduct.OrderCount++;
            _logger.LogInformation($"Machine is brewing {productId}... Order Count: {findProduct.OrderCount}");
            return Result.Ok(findProduct.OrderCount);
        }
    }
}