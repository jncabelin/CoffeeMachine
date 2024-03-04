using System.ComponentModel;
using System.Net;
using CoffeeMachine.Api;
using CoffeeMachine.Api.Messages;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CoffeeMachine.Tests.Api.IntegrationTests
{
    public class BrewCoffee_Should : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BrewCoffee_Should(WebApplicationFactory<Program> factory)
        {
            _factory = factory;          
        }

        [Fact]
		[DisplayName("UseCase001_ReturnStatusOK")]
		public async Task UseCase001_ReturnStatusOK()
		{
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/brew-coffee");

            // Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("message", responseBody);
            Assert.Contains("prepared", responseBody);
            Assert.Contains(ResponseMessage.OK,responseBody);
        }

        [Fact]
        [DisplayName("UseCase002_ReturnStatus503")]
        public async Task UseCase002_ReturnStatus503()
        {
            // Arrange
            var client = _factory.CreateClient();
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            for (var i=0; i <5; i++)
                response = await client.GetAsync("/brew-coffee");

            // Assert
            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.Equal("{}", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        [DisplayName("UseCase003_ReturnStatus418")]
        public async Task UseCase003_ReturnStatus418()
        {
            // Arrange
            var client = _factory.CreateClient();
            var DateNow = DateTime.UtcNow;

            // Act
            var response = await client.GetAsync("/brew-coffee");

            // Assert
            if (DateNow.Day == 1 && DateNow.Month == 4)
            {
                Assert.Equal((HttpStatusCode)418, response.StatusCode);
                Assert.Contains("I'm a teapot", response.ReasonPhrase);
            }
            else
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}

