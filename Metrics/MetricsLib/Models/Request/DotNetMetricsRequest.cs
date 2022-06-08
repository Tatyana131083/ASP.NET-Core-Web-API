using System;

namespace MetricsLib.Models.Request
{
    public class DotNetMetricsRequest
    {
        public int AgentId { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
