using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class DotNetMetricsAllResponse
    {
        public List<DotNetMetricsWithAgentResponse> Metrics { get; set; }

        public DotNetMetricsAllResponse()
        {
            Metrics = new List<DotNetMetricsWithAgentResponse>();
        }
    }
}
