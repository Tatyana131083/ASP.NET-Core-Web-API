using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsLib.Models.Response;
using MetricsManager.Services;
using MetricsLib.Models.Request;
using MetricsManager.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с метриками RAM")]
    public class RamMetricsController : Controller
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;

        public RamMetricsController(ILogger<RamMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение Ram метрик от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetRamMetricsFromAgent")]
        [ProducesResponseType(typeof(RamMetricsWithAgentResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId,
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            RamMetricsWithAgentResponse response = _metricsAgentClient.GetRamMetrics(new RamMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Ram agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        /// <summary>
        /// Получение Ram метрик от кластера
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetRamMetricsFromCluster")]
        [ProducesResponseType(typeof(RamMetricsAllResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAllCluster([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            RamMetricsAllResponse response = _metricsAgentClient.GetRamMetricsFromAllAgents(new RamMetricsAllRequest()
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Ram cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok(response);
        }
    }
}
