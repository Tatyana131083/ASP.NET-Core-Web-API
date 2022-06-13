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
    [Route("api/dotnet")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с метриками DotNet")]
    public class DotNetMetricsController : Controller
    {
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение DotNet метрик от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetDotNetMetricsFromAgent")]
        [ProducesResponseType(typeof(DotNetMetricsWithAgentResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId,
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            DotNetMetricsWithAgentResponse response = _metricsAgentClient.GetDotNetMetrics(new DotNetMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics DotNet agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        /// <summary>
        /// Получение DotNet метрик от кластера
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetDotNetMetricsFromCluster")]
        [ProducesResponseType(typeof(DotNetMetricsAllResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAllCluster([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            DotNetMetricsAllResponse response = _metricsAgentClient.GetDotNetMetricsFromAllAgents(new DotNetMetricsAllRequest()
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics DotNet cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok(response);
        }
    }
}
