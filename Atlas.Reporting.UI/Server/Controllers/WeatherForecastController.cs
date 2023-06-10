using Atlas.Reporting.UI.Shared;
using Atlas.Reporting.DAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.Reporting.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IReportingService _reportingService;
        public WeatherForecastController(IReportingService reportingService, ILogger<WeatherForecastController> logger)
        {
            _reportingService = reportingService;
            _logger = logger;
        }
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        [HttpGet("Report")]
        public async Task<IActionResult> GetReport()
        {
            return Ok(/*await _reportingService.GetReportBy(DateTime.Now, DateTime.Now, true, true)*/);
        }

        [HttpGet]
        public void Get()
        {
            //return 1; Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}