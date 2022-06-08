using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class RamMetricsWithAgentResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
        public int AgentId { get; set; }
    }
}
