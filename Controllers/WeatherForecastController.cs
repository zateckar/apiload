using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apiload.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public static string possibleChars = "abcdefghijklmnopqrstuvwxyz";
// optimized (?) what's available
public static char[] possibleCharsArray = possibleChars.ToCharArray();
// optimized (precalculated) count
public static int possibleCharsAvailable = possibleChars.Length;
// shared randomization thingy
public static Random random = new Random();


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi=true)]
        public string randomString(int num) {
        var result = new char[num];
        while(num-- > 0) {
            result[num] = possibleCharsArray[random.Next(possibleCharsAvailable)];
        }
            return new string(result);
        }

        [HttpGet("{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            var rng = new Random();
            return Enumerable.Range(1, id).Select(index => new WeatherForecast
            {
                
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Description = randomString(1000)
            })
            .ToArray();
        }

       



    }
}
