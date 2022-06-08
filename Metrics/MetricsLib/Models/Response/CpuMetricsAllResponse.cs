using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class CpuMetricsAllResponse
    {
        public List<CpuMetricsWithAgentResponse> Metrics { get; set; }

        public CpuMetricsAllResponse()
        {
            Metrics = new List<CpuMetricsWithAgentResponse>();
        }
    }
}
