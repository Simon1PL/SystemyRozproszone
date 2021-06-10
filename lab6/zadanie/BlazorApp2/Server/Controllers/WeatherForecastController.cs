﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;
using BlazorApp2.Shared;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("GetWeather/{name}")]
        public WeatherReplyModel GetWeather(string name)
        {
            Console.WriteLine("Json endpoint");
            var rng = new Random();
            WeatherReplyModel result = new WeatherReplyModel
            {
                Name = string.IsNullOrEmpty(name) ? "no name" : name,
                Forecasts = Enumerable.Range(1, 100).Select(index => new WeatherInfoModel
                {
                    Date = DateTime.UtcNow.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToList()
            };
            return result;
        }
    }
}
