using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class HddMetricsAllResponse
    {
        public List<HddMetricsWithAgentResponse> Metrics { get; set; }

        public HddMetricsAllResponse()
        {
            Metrics = new List<HddMetricsWithAgentResponse>();
        }
    }
}
