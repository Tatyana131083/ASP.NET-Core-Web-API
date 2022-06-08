using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class CpuMetricsWithAgentResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
        public int AgentId { get; set; }
    }
}
