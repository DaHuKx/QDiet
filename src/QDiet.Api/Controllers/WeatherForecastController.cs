using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QDiet.Domain.Service;
using System.Security.Claims;

namespace QDiet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(this.User.Claims.First(c => c.Type == "Id").Value);
        }
    }
}
