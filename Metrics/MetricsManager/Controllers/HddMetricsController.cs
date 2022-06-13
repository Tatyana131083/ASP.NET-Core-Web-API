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
    [Route("api/hdd")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с метриками HDD")]
    public class HddMetricsController : Controller
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;

        public HddMetricsController (ILogger<HddMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение Hdd метрик от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetHddMetricsFromAgent")]
        [ProducesResponseType(typeof(HddMetricsWithAgentResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId,
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            HddMetricsWithAgentResponse response = _metricsAgentClient.GetHddMetrics(new HddMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Hdd agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        /// <summary>
        /// Получение Hdd метрик от кластера
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetHddMetricsFromCluster")]
        [ProducesResponseType(typeof(HddMetricsAllResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAllCluster([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            HddMetricsAllResponse response = _metricsAgentClient.GetHddMetricsFromAllAgents(new HddMetricsAllRequest()
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Hdd cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok(response);
        }
    }
}
