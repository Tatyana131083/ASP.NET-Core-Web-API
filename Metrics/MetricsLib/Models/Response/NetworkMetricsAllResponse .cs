using System.Collections.Generic;

namespace MetricsLib.Models.Response
{
    public class NetworkMetricsAllResponse
    {
        public List<NetworkMetricsWithAgentResponse> Metrics { get; set; }

        public NetworkMetricsAllResponse()
        {
            Metrics = new List<NetworkMetricsWithAgentResponse>();
        }
    }
}
