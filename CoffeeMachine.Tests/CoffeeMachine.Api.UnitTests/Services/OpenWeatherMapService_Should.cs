using System.ComponentModel;
using CoffeeMachine.Api.Models.OpenWeatherMap;
using CoffeeMachine.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeeMachine.Tests.Api.UnitTests.Services
{
	public class OpenWeatherMapService_Should
	{
		Mock<IConfiguration> _config;
        Mock<ILogger<OpenWeatherMapService>> _logger;

        public OpenWeatherMapService_Should()
		{
            Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns("38f140c1301b42888f0d30559cd8af3d");

            _config = new Mock<IConfiguration>();
            _config.Setup(c => c.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
            _logger = new Mock<ILogger<OpenWeatherMapService>>();
		}

        [Fact]
        [DisplayName("Throw Exception if Null IConfiguration")]
        public void Throw_Exception_if_Null_IConfiguration()
        {
            // Arrange
            Action testConstructor = () => new OpenWeatherMapService(null, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new OpenWeatherMapService(_config.Object, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if invalid APIKey")]
        public void Throw_Exception_if_Invalid_APIKey()
        {
            // Arrange
            Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns("");
            _config.Setup(c => c.GetSection(It.IsAny<string>())).Returns(mockSection.Object);

            Action testConstructor = () => new OpenWeatherMapService(_config.Object, _logger.Object);

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(testConstructor);
        }

        [Fact]
        [DisplayName("Get CurrentWeather")]
        public async void Get_CurrentWeather()
        {
            // Arrange
            var service = new OpenWeatherMapService(_config.Object, _logger.Object);

            // Act
            var result = await service.GetCurrentWeatherAsync("Sydney,AU");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.MainWeatherData);
            Assert.IsType<CurrentWeather>(result.Value);
            Assert.IsType<MainWeatherData>(result.Value.MainWeatherData);
            Assert.IsType<Double>(result.Value.MainWeatherData.CurrentTemperature);
        }

        [Fact]
        [DisplayName("Return 400 if InvalidLocation")]
        public async void Return_400_if_InvalidLocation()
        {
            // Arrange
            var service = new OpenWeatherMapService(_config.Object, _logger.Object);

            // Act
            var result = await service.GetCurrentWeatherAsync(String.Empty);

            // Assert
            Assert.True(result.IsFailed);
        }
    }
}