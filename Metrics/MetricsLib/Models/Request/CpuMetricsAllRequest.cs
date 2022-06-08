using System;

namespace MetricsLib.Models.Request
{
    public class CpuMetricsAllRequest
    {

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
