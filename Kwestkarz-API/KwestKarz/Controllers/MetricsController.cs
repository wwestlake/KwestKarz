using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KwestKarz.Services;

namespace KwestKarz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("total-earnings")]
        public async Task<IActionResult> GetTotalEarnings([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (start > end)
                return BadRequest("Start date must be before end date.");

            var total = await _metricsService.GetTotalEarningsAsync(start, end);

            return Ok(new
            {
                start = start.ToString("yyyy-MM-dd"),
                end = end.ToString("yyyy-MM-dd"),
                totalEarnings = total
            });
        }

        [HttpGet("utilization")]
        public async Task<IActionResult> GetUtilization([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (start > end)
                return BadRequest("Start date must be before end date.");

            var results = await _metricsService.GetUtilizationByVehicleAsync(start, end);
            return Ok(results);
        }
    }
}
