using System.Collections.Generic;

namespace MetricsAgent.DAL.Response
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
