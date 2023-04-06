using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> logger;
        private readonly FlightService service;

        public FlightController(ILogger<FlightController> logger,
                                FlightService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> GetAirportsAsync()
        {
            var result = await service.GetAirports();
            return result.Success ? Ok(result) : BadRequest(result);

        }

        [HttpPost]
        [Route("airports/search")]
        public async Task<IActionResult> GetAirportsAsync([FromBody] SearchRequest request)
        {
            var result = await service.GetAirportByName(request.SearchText);
            return Ok(result);

        }

    }
}