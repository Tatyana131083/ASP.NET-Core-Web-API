using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob

    {
        private readonly IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            
    }

    public Task Execute(IJobExecutionContext context)
        {
            var ramFreeInMBytes = Convert.ToInt32(_ramCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DAL.Models.RamMetric
            {
                Time = time.TotalSeconds,
                Value = ramFreeInMBytes
            });
            return Task.CompletedTask;

        }
    }
}
