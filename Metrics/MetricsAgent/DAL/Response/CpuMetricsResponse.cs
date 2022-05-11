using System.Collections.Generic;

namespace MetricsAgent.DAL.Response
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }

    }
}
