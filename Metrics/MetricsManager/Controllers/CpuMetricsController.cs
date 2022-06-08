using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsLib.Models.Response;
using MetricsManager.Services;
using MetricsLib.Models.Request;
using MetricsManager.Models;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : Controller
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;


        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime
            , [FromRoute] TimeSpan toTime)
        {
            CpuMetricsWithAgentResponse response =  _metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            _logger.LogInformation(LogEvents.GetMetrics, "Getting metrics Cpu agentId:{agentId} from {fromTime} to {toTime}", agentId, fromTime, toTime);
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
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
