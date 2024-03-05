using System.ComponentModel;
using CoffeeMachine.Api.Handlers;
using CoffeeMachine.Api.Services.Interfaces;
using Moq;
using CoffeeMachine.Tests.Api.UnitTests.TestData;
using System.Net;
using CoffeeMachine.Api.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using CoffeeMachine.Api.Messages;
using Microsoft.Extensions.Logging;
using FluentResults;

namespace CoffeeMachine.Tests.Api.UnitTests.Handlers
{
	public class BrewCoffeeHandler_Should
	{
        Mock<IWeatherMapService> _weatherClient;
        Mock<ICoffeeMachineService> _coffeeMachineClient;
        Mock<IDateTimeProviderService> _dataTimeClient;
        Mock<ILogger<BrewCoffeeHandler>> _logger;
        BrewCoffeeHandler _handler;

        public BrewCoffeeHandler_Should()
        {
            _weatherClient = new Mock<IWeatherMapService>();
            _coffeeMachineClient = new Mock<ICoffeeMachineService>();
            _dataTimeClient = new Mock<IDateTimeProviderService>();
            _logger = new Mock<ILogger<BrewCoffeeHandler>>();
            _handler = new BrewCoffeeHandler(_coffeeMachineClient.Object, _weatherClient.Object, _dataTimeClient.Object, _logger.Object);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ICoffeeMachineService")]
        public void Throw_Exception_if_Null_ICoffeeMachineService()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(null, _weatherClient.Object, _dataTimeClient.Object, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null IWeatherService")]
        public void Throw_Exception_if_Null_IWeatherService()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(_coffeeMachineClient.Object, null, _dataTimeClient.Object, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null IDateTimeService")]
        public void Throw_Exception_if_Null_IDateTimeService()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(_coffeeMachineClient.Object, _weatherClient.Object, null, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new BrewCoffeeHandler(_coffeeMachineClient.Object, _weatherClient.Object, _dataTimeClient.Object, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Be Created if Valid Parameters")]
        public void Be_Created_if_Valid_Parameters()
        {
            // Act
            var sut = new BrewCoffeeHandler(_coffeeMachineClient.Object, _weatherClient.Object, _dataTimeClient.Object, _logger.Object);

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        [DisplayName("Return OK")]
        public async void Return_OK()
        {
            // Arrange
            var now = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Ok(now));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(1));
            _weatherClient.Setup(c => c.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(OpenWeatherMap_TestObjects.TestObjects_CurrentWeather));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(false));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, result.Value.Item1);
            Assert.Equal(ResponseMessage.OK, result.Value.Item2.Message);
            Assert.Equal(now, result.Value.Item2.Prepared);
        }


        [Fact]
        [DisplayName("Return 200 and Refreshing Message")]
        public async void Return_200_And_Refreshing_Message()
        {
            // Arrange
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(1));
            _weatherClient.Setup(c => c.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(OpenWeatherMap_TestObjects.TestObjects_HotCurrentWeather));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(false));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK, result.Value.Item1);
            Assert.Equal(ResponseMessage.REFRESHING_WEATHER, result.Value.Item2.Message);
            Assert.Equal(DateTime.UtcNow.Date, result.Value.Item2.Prepared.Date);
        }

        [Fact]
        [DisplayName("Return 503")]
        public async void Return_503()
        {
            // Arrange
            var now = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Ok(now));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(5));
            _weatherClient.Setup(c => c.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(OpenWeatherMap_TestObjects.TestObjects_CurrentWeather));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(false));

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
            var now = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Ok(now));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(1));
            _weatherClient.Setup(c => c.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(OpenWeatherMap_TestObjects.TestObjects_CurrentWeather));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(true));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusCodes.Status418ImATeapot, result.Value.Item1);
            Assert.Null(result.Value.Item2.Message);
        }

        [Fact]
        [DisplayName("Fail if CoffeeMachine Fails")]
        public async void Fail_if_CoffeeMachine_Fails()
        {
            // Arrange
            var now = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Ok(now));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Fail("Error"));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(false));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        [DisplayName("Fail if IsApril1st Fails")]
        public async void Fail_if_IsApril1st_Fails()
        {
            // Arrange
            var now = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Ok(now));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(1));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Fail("Error"));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        [DisplayName("Fail if GetISO8601 Fails")]
        public async void Fail_if_GetISO8601_Fails()
        {
            // Arrange
            _dataTimeClient.Setup(c => c.GetISO8601Now()).Returns(Result.Fail("Error"));
            _coffeeMachineClient.Setup(c => c.BrewProduct(It.IsAny<int>())).ReturnsAsync(Result.Ok(1));
            _dataTimeClient.Setup(c => c.IsAprilFirst()).Returns(Result.Ok(false));

            // Act
            var result = await _handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }
    }
}

