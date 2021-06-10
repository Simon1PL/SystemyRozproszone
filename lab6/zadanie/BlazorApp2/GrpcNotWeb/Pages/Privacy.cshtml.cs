using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp2;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GrpcNotWeb.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public TimeSpan Time { get; set; }
        public string Reply { get; set; }

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new WeatherForecast2.WeatherForecast2Client(channel);
            Stopwatch sw = new Stopwatch();
            WeatherReply reply = null;
            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                reply = await client.GetWeather2Async(new WeatherRequest { Name = "GreeterClient" });
                sw.Stop();
                Time += sw.Elapsed;
            }
            Time /= 100;
            Reply = reply.ToString();
        }
    }
}
