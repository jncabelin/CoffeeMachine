using System.ComponentModel;
using CoffeeMachine.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeeMachine.Tests.Api.UnitTests.Services
{
    public class DateTimeProviderService_Should
    {
        Mock<ILogger<DateTimeProviderService>> _logger;
        public DateTimeProviderService_Should()
        {
            _logger = new Mock<ILogger<DateTimeProviderService>>();
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new DateTimeProviderService(null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Verify if AprilFirst")]
        public void Verify_if_AprilFirst()
        {
            // Arrange
            var service = new DateTimeProviderService(_logger.Object);

            // Act
            var result = service.IsAprilFirst();

            // Assert
            Assert.True(result.IsSuccess);
            if (DateTime.Now.Day == 1 && DateTime.Now.Month == 4)
                Assert.True(result.Value);
            else
                Assert.False(result.Value);
        }

        [Fact]
        [DisplayName("Return ISO8601Standard")]
        public void Return_ISO801_Standard()
        {
            // Arrange
            var service = new DateTimeProviderService(_logger.Object);

            // Act
            var result = service.GetISO8601Now();

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}

