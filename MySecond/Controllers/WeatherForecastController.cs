using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySecond.Data;

namespace MySecond.Controllers
{
    [ApiController]
    [Route("api/weatherforecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest();
            }

            var existingForecast = await _context.WeatherForecasts.FindAsync(forecast.Id);

            if (existingForecast != null)
            {
                existingForecast.Date = forecast.Date;
                existingForecast.TemperatureC = forecast.TemperatureC;
                existingForecast.Summary = forecast.Summary;

                _context.WeatherForecasts.Update(existingForecast);
                await _context.SaveChangesAsync();

                return Ok(existingForecast);
            }
            else
            {
                _context.WeatherForecasts.Add(forecast);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
            }
        }

        [HttpDelete("{id:int}")]
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
