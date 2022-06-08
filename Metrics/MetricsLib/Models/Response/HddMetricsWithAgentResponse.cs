using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class HddMetricsWithAgentResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
        public int AgentId { get; set; }
    }
}
