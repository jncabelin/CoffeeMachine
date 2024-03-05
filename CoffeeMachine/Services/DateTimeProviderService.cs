using CoffeeMachine.Api.Services.Interfaces;
using FluentResults;

namespace CoffeeMachine.Api.Services
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        private ILogger<DateTimeProviderService> _logger;

        public DateTimeProviderService (ILogger<DateTimeProviderService> logger)
        {
            if (logger == null)
                throw new ArgumentNullException("Logger cannot be null.");
            _logger = logger;
        }

        // Compare timeNow if April 1st
        public Result<bool> IsAprilFirst()
        {
            var Now = DateTime.Now;
            return Result.Ok(Now.Day == 1 && Now.Month == 4);
        }

        // Get ISO-8601 Standard string
        public Result<string> GetISO8601Now()
        {
            return Result.Ok(DateTime.Now.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}

