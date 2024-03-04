using CoffeeMachine.Api.Services.Interfaces;
using FluentResults;

namespace CoffeeMachine.Api.Services
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        private ILogger<DateTimeProviderService> _logger;
        private DateTime Now { get; } = DateTime.UtcNow;

        public DateTimeProviderService (ILogger<DateTimeProviderService> logger)
        {
            if (logger == null)
                throw new ArgumentNullException("Logger cannot be null.");
            _logger = logger;
        }

        // Compare timeNow if April 1st
        public Result<bool> IsAprilFirst()
        {
            _logger.LogInformation($"The Date today is {Now}");
            return Result.Ok(Now.Day == 1 && Now.Month == 4);
        }
    }
}

