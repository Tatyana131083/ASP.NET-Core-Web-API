using System;

namespace MetricsLib.Models.Request
{
    public class DotNetMetricsAllRequest
    {

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
