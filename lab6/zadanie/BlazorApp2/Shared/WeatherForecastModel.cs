using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp2.Shared
{
    public class WeatherReplyModel
    {
        public string Name { get; set; }

        public List<WeatherInfoModel> Forecasts { get; set; }
    }

    public class WeatherInfoModel
    {
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
    }
}
