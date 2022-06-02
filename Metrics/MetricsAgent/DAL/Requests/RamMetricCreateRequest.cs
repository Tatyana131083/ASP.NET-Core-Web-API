using System;

namespace MetricsAgent.DAL.Requests
{
    public class RamMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
