using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySecond.Data;

namespace MySecond.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest();
            }

            _context.WeatherForecasts.Add(forecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
        [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
        public async Task<IActionResult> Delete(int id)
        {
            var forecast = await _context.WeatherForecasts.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(forecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
