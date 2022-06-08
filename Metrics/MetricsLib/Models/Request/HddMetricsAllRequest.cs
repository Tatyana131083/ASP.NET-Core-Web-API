using System;

namespace MetricsLib.Models.Request
{
    public class HddMetricsAllRequest
    {

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
