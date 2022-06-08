using MetricsLib.Models.Request;
using MetricsLib.Models.Response;

namespace MetricsManager.Services
{
    public interface IMetricsAgentClient
    {
        CpuMetricsWithAgentResponse GetCpuMetrics(CpuMetricsRequest request);
        CpuMetricsAllResponse GetCpuMetricsFromAllAgents(CpuMetricsAllRequest request);

        DotNetMetricsWithAgentResponse GetDotNetMetrics(DotNetMetricsRequest request);
        DotNetMetricsAllResponse GetDotNetMetricsFromAllAgents(DotNetMetricsAllRequest request);

        HddMetricsWithAgentResponse GetHddMetrics(HddMetricsRequest request);
        HddMetricsAllResponse GetHddMetricsFromAllAgents(HddMetricsAllRequest request);

        NetworkMetricsWithAgentResponse GetNetworkMetrics(NetworkMetricsRequest request);
        NetworkMetricsAllResponse GetNetworkMetricsFromAllAgents(NetworkMetricsAllRequest request);

        RamMetricsWithAgentResponse GetRamMetrics(RamMetricsRequest request);
        RamMetricsAllResponse GetRamMetricsFromAllAgents(RamMetricsAllRequest request);
    }
}
