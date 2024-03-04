using System.ComponentModel;
using CoffeeMachine.Api.Models.OpenWeatherMap;
using Newtonsoft.Json;
using CoffeeMachine.Tests.Api.UnitTests.TestData;
using CoffeeMachine.Tests.Api.UnitTests.TestHelpers;

namespace CoffeeMachine.Tests.Api.UnitTests.Models.OpenWeatherMap
{
    public class JsonConvert_should
    {
        [Fact]
        [DisplayName("Serialize and Deserialize Clouds")]
        public void Serialize_and_Deserialize_Clouds()
        {
            // Arrange
            var clouds = OpenWeatherMap_TestObjects.TestObjects_Clouds;

            // Act
            var jsonResult = JsonConvert.SerializeObject(clouds);
            var objResult = JsonConvert.DeserializeObject<Clouds>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"all\":", jsonResult);
            Assert.True(HelperMethods.Compare<Clouds>(clouds, objResult));

        }

        [Fact]
        [DisplayName("Serialize and Deserialize Coordinates")]
        public void Serialize_and_Deserialize_Coordinates()
        {
            // Arrange
            var coord = OpenWeatherMap_TestObjects.TestObjects_Coordinates;

            // Act
            var jsonResult = JsonConvert.SerializeObject(coord);
            var objResult = JsonConvert.DeserializeObject<Coordinates>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"lat\":", jsonResult);
            Assert.Contains($"\"lon\":", jsonResult);
            Assert.IsType<Coordinates>(objResult);
            Assert.True(HelperMethods.Compare<Coordinates>(coord, objResult));
        }

        [Fact]
        [DisplayName("Serialize and Deserialize MainWeatherData")]
        public void Serialize_and_Deserialize_MainWeatherData()
        {
            // Arrange
            var main = OpenWeatherMap_TestObjects.TestObjects_MainWeatherData;

            // Act
            var jsonResult = JsonConvert.SerializeObject(main);
            var objResult = JsonConvert.DeserializeObject<MainWeatherData>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"temp\":", jsonResult);
            Assert.Contains($"\"feels_like\":", jsonResult);
            Assert.Contains($"\"temp_min\":", jsonResult);
            Assert.Contains($"\"temp_max\":", jsonResult);
            Assert.Contains($"\"humidity\":", jsonResult);
            Assert.Contains($"\"pressure\":", jsonResult);
            Assert.IsType<MainWeatherData>(objResult);
            Assert.True(HelperMethods.Compare<MainWeatherData>(main, objResult));
        }

        [Fact]
        [DisplayName("Serialize and Deserialize Sys")]
        public void Serialize_and_Deserialize_Sys()
        {
            // Arrange
            var sys = OpenWeatherMap_TestObjects.TestObjects_Sys;

            // Act
            var jsonResult = JsonConvert.SerializeObject(sys);
            var objResult = JsonConvert.DeserializeObject<Sys>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"type\":", jsonResult);
            Assert.Contains($"\"id\":", jsonResult);
            Assert.Contains($"\"country\":", jsonResult);
            Assert.Contains($"\"sunrise\":", jsonResult);
            Assert.Contains($"\"sunset\":", jsonResult);
            Assert.IsType<Sys>(objResult);
            Assert.True(HelperMethods.Compare<Sys>(sys, objResult));
        }

        [Fact]
        [DisplayName("Serialize and Deserialize Weather")]
        public void Serialize_and_Deserialize_Weather()
        {
            // Arrange
            var weather = OpenWeatherMap_TestObjects.TestObjects_Weather;

            // Act
            var jsonResult = JsonConvert.SerializeObject(weather);
            var objResult = JsonConvert.DeserializeObject<Weather>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"main\":", jsonResult);
            Assert.Contains($"\"id\":", jsonResult);
            Assert.Contains($"\"description\":", jsonResult);
            Assert.Contains($"\"icon\":", jsonResult);
            Assert.IsType<Weather>(objResult);
            Assert.True(HelperMethods.Compare<Weather>(weather, objResult));
        }

        [Fact]
        [DisplayName("Serialize and Deserialize Wind")]
        public void Serialize_and_Deserialize_Wind()
        {
            // Arrange
            var wind = OpenWeatherMap_TestObjects.TestObjects_Wind;

            // Act
            var jsonResult = JsonConvert.SerializeObject(wind);
            var objResult = JsonConvert.DeserializeObject<Wind>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"speed\":", jsonResult);
            Assert.Contains($"\"deg\":", jsonResult);
            Assert.IsType<Wind>(objResult);
            Assert.True(HelperMethods.Compare<Wind>(wind, objResult));
        }

        [Fact]
        [DisplayName("Serialize and Deserialize CurrentWeather")]
        public void Serialize_and_Deserialize_CurrentWeather()
        {
            // Arrange
            var currentWeather = OpenWeatherMap_TestObjects.TestObjects_CurrentWeather;

            // Act
            var jsonResult = JsonConvert.SerializeObject(currentWeather);
            var objResult = JsonConvert.DeserializeObject<CurrentWeather>(jsonResult);

            // Assert
            Assert.NotNull(jsonResult);
            Assert.NotNull(objResult);
            Assert.Contains($"\"coord\":", jsonResult);
            Assert.Contains($"\"weather\":", jsonResult);
            Assert.Contains($"\"base\":", jsonResult);
            Assert.Contains($"\"main\":", jsonResult);
            Assert.Contains($"\"visibility\":", jsonResult);
            Assert.Contains($"\"wind\":", jsonResult);
            Assert.Contains($"\"clouds\":", jsonResult);
            Assert.Contains($"\"dt\":", jsonResult);
            Assert.Contains($"\"sys\":", jsonResult);
            Assert.Contains($"\"timezone\":", jsonResult);
            Assert.Contains($"\"id\":", jsonResult);
            Assert.Contains($"\"name\":", jsonResult);
            Assert.Contains($"\"cod\":", jsonResult);
            Assert.IsType<CurrentWeather>(objResult);
            Assert.True(HelperMethods.Compare<CurrentWeather>(currentWeather, objResult));
        }
    }
}

