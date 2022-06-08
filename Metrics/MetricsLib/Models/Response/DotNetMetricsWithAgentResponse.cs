using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class DotNetMetricsWithAgentResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
        public int AgentId { get; set; }
    }
}
