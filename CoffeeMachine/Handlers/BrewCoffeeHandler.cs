using System.Net;
using CoffeeMachine.Api.Services.Interfaces;
using CoffeeMachine.Api.Dtos.Requests;
using CoffeeMachine.Api.Dtos.Responses;
using CoffeeMachine.Api.Messages;
using MediatR;
using FluentResults;

namespace CoffeeMachine.Api.Handlers
{
    public class BrewCoffeeHandler : IRequestHandler<BrewCoffeeQuery, Result<(int, BrewCoffeeResponse)>>
    {
        private ICoffeeMachineService _coffeeMachineClient;
        private IWeatherMapService _weatherMapClient;
        private IDateTimeProviderService _dateTimeProviderClient;
        private ILogger<BrewCoffeeHandler> _logger;

        public BrewCoffeeHandler(ICoffeeMachineService coffeeMachineClient, IWeatherMapService weatherMapClient,
            IDateTimeProviderService dateTimeClient, ILogger<BrewCoffeeHandler> logger)
        {
            if (coffeeMachineClient == null)
                throw new ArgumentNullException("Data Store cannot be null.");
            if (weatherMapClient == null)
                throw new ArgumentNullException("Weather Map Client cannot be null.");
            if (dateTimeClient == null)
                throw new ArgumentNullException("Date Time Client cannot be null.");
            if (logger == null)
                throw new ArgumentNullException("Logger cannot be null.");

            _coffeeMachineClient = coffeeMachineClient;
            _weatherMapClient = weatherMapClient;
            _dateTimeProviderClient = dateTimeClient;
            _logger = logger;
        }

        public async Task<Result<(int,BrewCoffeeResponse)>> Handle(BrewCoffeeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                return Result.Fail("Invalid request");

            var machineResult = await _coffeeMachineClient.BrewProduct();
            if (machineResult.IsFailed)
                return Result.Fail(machineResult.Errors);

            // Check if April 1st
            var dateTimeResult = _dateTimeProviderClient.IsAprilFirst();
            if (dateTimeResult.IsFailed)
                return Result.Fail(dateTimeResult.Errors);

            if (dateTimeResult.Value)
            {
                _logger.LogInformation("Machine is not brewing coffee today.");
                return Result.Ok((StatusCodes.Status418ImATeapot, new BrewCoffeeResponse()));
            }

            // Check for Request Count
            if (machineResult.Value > 0 && machineResult.Value % 5 != 0)
            {
                // Set default location to Sydney,AU
                var weatherResult = await _weatherMapClient.GetCurrentWeatherAsync("Sydney,AU");
                if (weatherResult.IsFailed)
                    return Result.Fail(weatherResult.Errors);

                // Return OK with Refreshing Message if temp is g.t. 25 deg
                if (weatherResult.Value != null
                    && weatherResult.Value.MainWeatherData.CurrentTemperature > 30.0)
                {
                    _logger.LogInformation(ResponseMessage.REFRESHING_WEATHER);
                    return Result.Ok((StatusCodes.Status200OK, new BrewCoffeeResponse(ResponseMessage.REFRESHING_WEATHER, DateTimeOffset.UtcNow)));
                }

                // Return OK without Refreshing Message
                _logger.LogInformation(ResponseMessage.OK);
                return Result.Ok((StatusCodes.Status200OK, new BrewCoffeeResponse(ResponseMessage.OK, DateTimeOffset.UtcNow)));
            }

            // Return 503 every 5th request
            else
            {
                _logger.LogInformation("Machine is out of coffee.");
                return Result.Ok((StatusCodes.Status503ServiceUnavailable, new BrewCoffeeResponse()));
            }
        }
    }
}

