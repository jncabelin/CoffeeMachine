using Moq;
using CoffeeMachine.Api.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using CoffeeMachine.Api.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using CoffeeMachine.Api.Dtos.Responses;
using CoffeeMachine.Api.Messages;
using Microsoft.AspNetCore.Mvc;
using CoffeeMachine.Tests.Api.UnitTests.TestHelpers;

namespace CoffeeMachine.Tests.Api.UnitTests.Controllers
{
	public class CoffeeMachineController_Should
	{
        Mock<ILogger<CoffeeMachineController>> _logger;
        Mock<IMediator> _mediator;
		CoffeeMachineController sut;

        public CoffeeMachineController_Should()
		{
			_logger = new Mock<ILogger<CoffeeMachineController>>();
			_mediator = new Mock<IMediator>();
			sut = new CoffeeMachineController(_mediator.Object, _logger.Object);
		}

        [Fact]
        [DisplayName("Throw Exception if Null IMediator")]
        public void Throw_Exception_if_Null_IMediator()
        {
            // Arrange
            Action testConstructor = () => new CoffeeMachineController(null, _logger.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Throw Exception if Null ILogger")]
        public void Throw_Exception_if_Null_ILogger()
        {
            // Arrange
            Action testConstructor = () => new CoffeeMachineController(_mediator.Object, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(testConstructor);
        }

        [Fact]
        [DisplayName("Be SuccessfullyCreated")]
        public void Be_SuccessfullyCreated()
        {
            // Act
            var sut = new CoffeeMachineController(_mediator.Object, _logger.Object);

            // ActAssert
            Assert.NotNull(sut);
        }

        [Fact]
		[DisplayName("Return 200 using BrewCoffee")]
		public async Task Return_200_using_BrewCoffee()
		{
            // Arrange
            var okResponse = new BrewCoffeeResponse(ResponseMessage.OK, DateTimeOffset.UtcNow);
            _mediator.Setup(m => m.Send(It.IsAny<BrewCoffeeQuery>(),CancellationToken.None))
                .ReturnsAsync((StatusCodes.Status200OK, okResponse));

            // Act
            var actionResult = await sut.BrewCoffee();
            var objResult = actionResult as JsonResult;

            // Assert
            Assert.NotNull(objResult);
            Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
            var response = objResult.Value as BrewCoffeeResponse;
            Assert.NotNull(response);
            Assert.True(HelperMethods.Compare(okResponse, response));
		}

        [Fact]
        [DisplayName("Return 200 Refreshing using BrewCoffee")]
        public async Task Return_200_Refreshing_using_BrewCoffee()
        {
            // Arrange
            var okResponse = new BrewCoffeeResponse(ResponseMessage.REFRESHING_WEATHER, DateTimeOffset.UtcNow);
            _mediator.Setup(m => m.Send(It.IsAny<BrewCoffeeQuery>(), CancellationToken.None))
                .ReturnsAsync((StatusCodes.Status200OK, okResponse));

            // Act
            var actionResult = await sut.BrewCoffee();
            var objResult = actionResult as JsonResult;

            // Assert
            Assert.NotNull(objResult);
            Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
            var response = objResult.Value as BrewCoffeeResponse;
            Assert.NotNull(response);
            Assert.True(HelperMethods.Compare(okResponse, response));
        }

        [Fact]
        [DisplayName("Return 503 using BrewCoffee")]
        public async Task Return_503_using_BrewCoffee()
        {
            // Arrange
            var badResponse = new BrewCoffeeResponse();
            _mediator.Setup(m => m.Send(It.IsAny<BrewCoffeeQuery>(), CancellationToken.None))
                .ReturnsAsync((StatusCodes.Status503ServiceUnavailable, badResponse));

            // Act
            var actionResult = await sut.BrewCoffee();
            var objResult = actionResult as JsonResult;

            // Assert
            Assert.NotNull(objResult);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, objResult?.StatusCode);
            Assert.True(HelperMethods.Compare(objResult, new Object()));
        }

        [Fact]
        [DisplayName("Return 418 using BrewCoffee")]
        public async Task Return_418_using_BrewCoffee()
        {
            // Arrange
            var badResponse = new BrewCoffeeResponse();
            _mediator.Setup(m => m.Send(It.IsAny<BrewCoffeeQuery>(), CancellationToken.None))
                .ReturnsAsync((StatusCodes.Status418ImATeapot, badResponse));

            // Act
            var actionResult = await sut.BrewCoffee();
            var objResult = actionResult as JsonResult;

            // Assert
            Assert.NotNull(objResult);
            Assert.NotNull(objResult?.Value);
            Assert.Equal(StatusCodes.Status418ImATeapot, objResult?.StatusCode);
            Assert.True(HelperMethods.Compare(objResult, new Object()));
        }
    }
}

