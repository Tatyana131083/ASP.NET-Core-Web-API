using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace MetricsAgent.Jobs
{
    public class DotnetMetricJob : IJob

    {
        private readonly IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;

        public DotnetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");

    }

    public Task Execute(IJobExecutionContext context)
        {
            var dotNetHeapsInBytes = Convert.ToInt32(_dotNetCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DAL.Models.DotNetMetric
            {
                Time = time.TotalSeconds,
                Value = dotNetHeapsInBytes
            });
            return Task.CompletedTask;

        }
    }
}
