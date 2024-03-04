using CoffeeMachine.Api.Models.OpenWeatherMap;

namespace CoffeeMachine.Tests.Api.UnitTests.TestData
{
	public class OpenWeatherMap_TestObjects
	{
		public static readonly Clouds TestObjects_Clouds = new Clouds { All = 8.0 };

		public static readonly Coordinates TestObjects_Coordinates = new Coordinates { Latitude = 8.0, Longitude = 8.0 };

		public static readonly MainWeatherData TestObjects_MainWeatherData = new MainWeatherData
		{
			CurrentTemperature = 25.0,
			FeelsLike = 25.0,
			TemperatureMax = 26.0,
			TemperatureMin = 24.0,
			Pressure = 25.0,
			Humidity = 25.0
		};

        public static readonly MainWeatherData TestObjects_HotMainWeatherData = new MainWeatherData
        {
            CurrentTemperature = 40.0,
            FeelsLike = 25.0,
            TemperatureMax = 26.0,
            TemperatureMin = 24.0,
            Pressure = 25.0,
            Humidity = 25.0
        };

        public static readonly Sys TestObjects_Sys = new Sys
		{
			ID = 25,
			Country = "AU",
			Type = 25,
			Sunrise = 25,
			Sunset = 25
		};

		public static readonly Weather TestObjects_Weather = new Weather
		{
			ID = 25,
			Description = "Test",
			Main = "Test",
			Icon = "25SH"
		};

		public static readonly Wind TestObjects_Wind = new Wind
		{
			Speed = 25.0,
			Degree = 120.0
		};

		public static readonly CurrentWeather TestObjects_CurrentWeather = new CurrentWeather
		{
			Coordinates = TestObjects_Coordinates,
			Weather = new List<Weather> { TestObjects_Weather },
			Base = "Test",
			MainWeatherData = TestObjects_MainWeatherData,
			Visibility = 1000.0,
			Wind = TestObjects_Wind,
			Clouds = TestObjects_Clouds,
			DT = 25,
            Sys = TestObjects_Sys,
			Timezone = 25,
			ID = 25,
			Name = "Test",
			Cod = 25
        };

        public static readonly CurrentWeather TestObjects_HotCurrentWeather = new CurrentWeather
        {
            Coordinates = TestObjects_Coordinates,
            Weather = new List<Weather> { TestObjects_Weather },
            Base = "Test",
            MainWeatherData = TestObjects_HotMainWeatherData,
            Visibility = 1000.0,
            Wind = TestObjects_Wind,
            Clouds = TestObjects_Clouds,
            DT = 25,
            Sys = TestObjects_Sys,
            Timezone = 25,
            ID = 25,
            Name = "Test",
            Cod = 25
        };
    }
}

