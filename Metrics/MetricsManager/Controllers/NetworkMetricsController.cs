using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsLib.Models.Response;
using MetricsManager.Services;
using MetricsLib.Models.Request;
using MetricsManager.Models;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : Controller
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
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

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
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
