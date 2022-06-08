using System;

namespace MetricsLib.Models.Request
{
    public class NetworkMetricsRequest
    {
        public int AgentId { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
