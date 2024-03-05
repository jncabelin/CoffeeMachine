using System.ComponentModel;
using Newtonsoft.Json;
using CoffeeMachine.Tests.Api.UnitTests.TestHelpers;
using CoffeeMachine.Api.Dtos.Responses;
using CoffeeMachine.Api.Messages;

namespace CoffeeMachine.Tests.Api.UnitTests.Dtos
{
    public class JsonConvert_should
    {
        [Fact]
        [DisplayName("Serialize and Deserialize BrewCoffeeResponse")]
        public void Serialize_and_Deserialize_BrewCoffeeResponse()
        {
            // Arrange
            var response = new BrewCoffeeResponse(ResponseMessage.OK, DateTimeOffset.UtcNow.ToString());

            // Act
            var jsonResult = JsonConvert.SerializeObject(response);
            var objResult = JsonConvert.DeserializeObject<BrewCoffeeResponse>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"message\":", jsonResult);
            Assert.Contains($"\"prepared\":", jsonResult);
            Assert.True(HelperMethods.Compare<BrewCoffeeResponse>(response, objResult));

        }
    }
}

