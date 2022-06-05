using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob

    {
        private readonly INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0];
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", instance);
        }



    public Task Execute(IJobExecutionContext context)
        {
            var networkUsage = Convert.ToInt32(_networkCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DAL.Models.NetworkMetric
            {
                Time = time.TotalSeconds,
                Value = networkUsage
            });
            return Task.CompletedTask;

        }
    }
}
