using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class RamMetricsAllResponse
    {
        public List<RamMetricsWithAgentResponse> Metrics { get; set; }

        public RamMetricsAllResponse()
        {
            Metrics = new List<RamMetricsWithAgentResponse>();
        }
    }
}
