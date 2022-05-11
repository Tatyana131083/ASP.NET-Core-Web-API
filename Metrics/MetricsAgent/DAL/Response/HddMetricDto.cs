using System;

namespace MetricsAgent.DAL.Response
{
    public class HddMetricDto
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
