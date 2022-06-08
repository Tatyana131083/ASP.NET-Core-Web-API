using System;

namespace MetricsLib.Models.Request
{
    public class NetworkMetricsAllRequest
    {

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
