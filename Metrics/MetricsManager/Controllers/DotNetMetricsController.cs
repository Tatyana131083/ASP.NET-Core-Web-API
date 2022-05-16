using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsManager.Models;

namespace MetricsManager.Controllers
{
    [Route("api/dotnet")]
    [ApiController]
    public class DotNetMetricsController : Controller
    {
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok();
        }
    }
}
