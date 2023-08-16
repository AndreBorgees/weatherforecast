using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public WeatherModel Get()
        {
            HttpClient client = new HttpClient();
            var url = "https://api.hgbrasil.com/weather?locale=pt";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;
            var weather = response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WeatherModel>(weather.Result);

            return result;
        }
    }

    public class WeatherModel
    {
        public string By { get; set; }
        public string Valid_key { get; set; }
        public Results Results { get; set; } 
        
    }

    public class Results
    {
        public string Temp { get; set; }
        public string City { get; set; }
        public List<Forecasts> Forecast { get; set; } = new List<Forecasts>();


        public override string ToString()
        {
            return $"Cidade: {City} - Temperatura: {Temp}°C";
        }
    }

    public class Forecasts
    {
        public string Date { get; set; }
        public string Weekday { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }

    }
}
