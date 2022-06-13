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
    [Route("api/network")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с метриками Network")]
    public class NetworkMetricsController : Controller
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение Network метрик от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetNetworkMetricsFromAgent")]
        [ProducesResponseType(typeof(NetworkMetricsWithAgentResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId,
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            NetworkMetricsWithAgentResponse response = _metricsAgentClient.GetNetworkMetrics(new NetworkMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Network agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        /// <summary>
        /// Получение Network метрик от кластера
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetNetworkMetricsFromCluster")]
        [ProducesResponseType(typeof(NetworkMetricsAllResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAllCluster([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            NetworkMetricsAllResponse response = _metricsAgentClient.GetNetworkMetricsFromAllAgents(new NetworkMetricsAllRequest()
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Network cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok(response);
        }

    }
}
