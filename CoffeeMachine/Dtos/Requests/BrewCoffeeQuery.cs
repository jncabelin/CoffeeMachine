using CoffeeMachine.Api.Dtos.Responses;
using MediatR;
using FluentResults;

namespace CoffeeMachine.Api.Dtos.Requests
{
    public class BrewCoffeeQuery : IRequest<Result<(int, BrewCoffeeResponse)>>
    {
        public BrewCoffeeQuery()
        {
        }
    }
}

