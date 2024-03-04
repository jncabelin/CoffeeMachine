using CoffeeMachine.Api.Services;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeeMachine.Tests.Api.UnitTests.Services
{
	public class FakeCoffeeMachineService_Should
	{
        Mock<ILogger<FakeCoffeeMachineService>> _logger;

        public FakeCoffeeMachineService_Should()
        {
            _logger = new Mock<ILogger<FakeCoffeeMachineService>>();
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new FakeCoffeeMachineService(null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("BrewProduct_Once")]
        public async void BrewProduct_Once()
        {
            // Arrange
            var service = new FakeCoffeeMachineService(_logger.Object);

            // Act
            var result = await service.BrewProduct();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        [DisplayName("Update_ProductCounter")]
        public async void Update_ProductCounter()
        {
            // Arrange
            var service = new FakeCoffeeMachineService(_logger.Object);

            // Act and Assert
            for (var i = 1; i <= 5; i++)
            {
                var result = await service.BrewProduct();

                Assert.True(result.IsSuccess);
                Assert.Equal(i, result.Value);
            }
        }
    }
}

