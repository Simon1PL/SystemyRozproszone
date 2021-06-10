﻿using System;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorApp2.Shared.Services
{
	public class GrpcService : WeatherForecast2.WeatherForecast2Base
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public override Task<WeatherReply> GetWeather2(WeatherRequest request, ServerCallContext context)
		{
			Console.WriteLine("Grpc endpoint(not web)");
			var reply = new WeatherReply();
			var rng = new Random();

			reply.Name = request.Name;
			reply.Forecasts.Add(Enumerable.Range(1, 100).Select(index => new WeatherInfo // ciekawostka: repeated z proto jest readonly
			{
				DateTimeStamp = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
				TemperatureC = rng.Next(20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			}));

			return Task.FromResult(reply);
		}
	}
}
