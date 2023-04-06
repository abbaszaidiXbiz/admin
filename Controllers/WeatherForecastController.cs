using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : BaseController
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

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> FileUpload(IFormFile file)
    {
        // JArray data = new JArray();
        // using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
        // {
        //     ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
        //     //Process, read from excel here and populate jarray
        // }
        return Ok();
    }
}
