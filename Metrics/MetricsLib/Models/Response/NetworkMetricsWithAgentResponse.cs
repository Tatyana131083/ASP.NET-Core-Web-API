using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class NetworkMetricsWithAgentResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
        public int AgentId { get; set; }
    }
}
