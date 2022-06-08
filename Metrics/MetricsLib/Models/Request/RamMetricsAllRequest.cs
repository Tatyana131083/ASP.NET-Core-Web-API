using System;

namespace MetricsLib.Models.Request
{
    public class RamMetricsAllRequest
    {

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
