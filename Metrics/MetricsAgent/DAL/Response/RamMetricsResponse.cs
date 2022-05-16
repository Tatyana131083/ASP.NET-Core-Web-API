using System.Collections.Generic;

namespace MetricsAgent.DAL.Response
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
