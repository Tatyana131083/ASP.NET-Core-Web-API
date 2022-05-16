using System.Collections.Generic;

namespace MetricsAgent.DAL.Response
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
