using System;

namespace MetricsLib.Models.Request
{
    public class HddMetricsRequest
    {
        public int AgentId { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
