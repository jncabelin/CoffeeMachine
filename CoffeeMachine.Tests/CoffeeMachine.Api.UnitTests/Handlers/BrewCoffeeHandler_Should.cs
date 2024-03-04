using System.ComponentModel;
using CoffeeMachine.Api.Handlers;
using CoffeeMachine.Api.Services.Interfaces;
using Moq;
using System.Net;
using CoffeeMachine.Api.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using CoffeeMachine.Api.Messages;
using Microsoft.Extensions.Logging;

namespace CoffeeMachine.Tests.Api.UnitTests.Handlers
{
	public class BrewCoffeeHandler_Should
	{
        Mock<ICoffeeMachineService> _coffeeMachineClient;
        Mock<IDateTimeProviderService> _dataTimeClient;
        Mock<ILogger<BrewCoffeeHandler>> _logger;
        BrewCoffeeHandler _handler;

        public BrewCoffeeHandler_Should()
        {
            _coffeeMachineClient = new Mock<ICoffeeMachineService>();
            _dataTimeClient = new Mock<IDateTimeProviderService>();
            _logger = new Mock<ILogger<BrewCoffeeHandler>>();
            _handler = new BrewCoffeeHandler(_coffeeMachineClient.Object, _dataTimeClient.Object, _logger.Object);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ICoffeeMachineService")]
        public void Throw_Exception_if_Null_ICoffeeMachineService()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(null, _dataTimeClient.Object, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null IDateTimeService")]
        public void Throw_Exception_if_Null_IDateTimeService()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(_coffeeMachineClient.Object, null, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(_coffeeMachineClient.Object, _dataTimeClient.Object, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Be Created if Valid Parameters")]
        public void Be_Created_if_Valid_Parameters()
        {
            // Act
            var sut = new BrewCoffeeHandler(_coffeeMachineClient.Object, _dataTimeClient.Object, _logger.Object);

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        [DisplayName("Return OK")]
        public async void Return_OK()
        {
            // Arrange
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(1);
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(false);

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, result.Value.Item1);
            Assert.Equal(ResponseMessage.OK, result.Value.Item2.Message);
            Assert.Equal(DateTime.UtcNow.Date, result.Value.Item2.Prepared.Date);
        }

        [Fact]
        [DisplayName("Return 503")]
        public async void Return_503()
        {
            // Arrange
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(5);
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(false);

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, result.Value.Item1);
            Assert.Null(result.Value.Item2.Message);
        }

        [Fact]
        [DisplayName("Return 418")]
        public async void Return_418()
        {
            // Arrange
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(1);
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(true);

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status418ImATeapot, result.Value.Item1);
            Assert.Null(result.Value.Item2.Message);
        }
    }
}

