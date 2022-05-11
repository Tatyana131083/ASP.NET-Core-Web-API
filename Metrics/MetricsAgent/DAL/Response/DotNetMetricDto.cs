using System;

namespace MetricsAgent.DAL.Response
{
    public class DotNetMetricDto
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
