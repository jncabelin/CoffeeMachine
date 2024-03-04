using Microsoft.AspNetCore.Mvc;
using MediatR;
using CoffeeMachine.Api.Dtos.Requests;

namespace CoffeeMachine.Api.Controllers
{

    [ApiController]
    public class CoffeeMachineController : ControllerBase
    {
        private ILogger<CoffeeMachineController> _logger;
        private IMediator _mediator;

        public CoffeeMachineController(IMediator mediator, ILogger<CoffeeMachineController> logger)
        {
            if (mediator == null)
                throw new ArgumentNullException("IMediator cannot be null.");
            if (logger == null)
                throw new ArgumentNullException("ILogger cannot be null.");
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("/brew-coffee")]
        public async Task<IActionResult> BrewCoffee()
        {
            var result = await _mediator.Send(new BrewCoffeeQuery());
            if (result.IsFailed)
            {
                return new JsonResult(new object())
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            if (result.Value.Item1 == StatusCodes.Status503ServiceUnavailable)
            {
                _logger.LogInformation("Service Unavailable");
                return new JsonResult(new object()) {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                };
            }
            if (result.Value.Item1 == StatusCodes.Status418ImATeapot)
            {
                _logger.LogInformation("I'm a Teapot.");
                return new JsonResult(new object()) {
                    StatusCode = StatusCodes.Status418ImATeapot,
                };
            }

            _logger.LogInformation("Success", result.Value.Item2);
            return new JsonResult (result.Value.Item2) {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}

