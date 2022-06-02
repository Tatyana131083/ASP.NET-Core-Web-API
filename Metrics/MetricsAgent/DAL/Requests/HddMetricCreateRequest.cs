using System;

namespace MetricsAgent.DAL.Requests
{
    public class HddMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
