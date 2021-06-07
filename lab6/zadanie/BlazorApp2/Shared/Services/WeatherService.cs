using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorApp2.Shared.Services
{
	public class WeatherService : WeatherForecasts.WeatherForecastsBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public override Task<WeatherReply> GetWeather(WeatherForecast request, ServerCallContext context)
		{
			var reply = new WeatherReply();
			var rng = new Random();

			reply.Forecasts.Add(Enumerable.Range(1, 100).Select(index => new WeatherForecast
			{
				DateTimeStamp = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
				TemperatureC = rng.Next(20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			}));

			return Task.FromResult(reply);
		}
	}
}
