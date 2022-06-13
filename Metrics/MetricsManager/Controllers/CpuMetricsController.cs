using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsLib.Models.Response;
using MetricsManager.Services;
using MetricsLib.Models.Request;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с метриками CPU")]
    public class CpuMetricsController : Controller
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;


        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение CPU метрик от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetCpuMetricsFromAgent")]
        [ProducesResponseType(typeof(CpuMetricsWithAgentResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            CpuMetricsWithAgentResponse response = _metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Cpu agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        /// <summary>
        /// Получение CPU метрик от кластера
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("GetCpuMetricsFromCluster")]
        [ProducesResponseType(typeof(CpuMetricsAllResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAllCluster([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            CpuMetricsAllResponse response = _metricsAgentClient.GetCpuMetricsFromAllAgents(new CpuMetricsAllRequest()
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Cpu cluster from {fromTime} to {toTime}", fromTime, toTime);
            return Ok(response);
        }
    }
}
