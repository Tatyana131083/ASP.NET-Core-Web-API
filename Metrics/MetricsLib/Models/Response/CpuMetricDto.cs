using System;

namespace MetricsLib.Models.Response
{
    public class CpuMetricDto
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
