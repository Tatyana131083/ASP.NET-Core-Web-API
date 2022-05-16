using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentTests
    {
        private CpuMetricsController _cpuMetricsController;
        private Mock<ICpuMetricsRepository> mockICpuMetricsRepository;
        private Mock<ILogger<CpuMetricsController>> mockILogger;

        public CpuMetricsAgentTests()
        {
            mockICpuMetricsRepository = new Mock<ICpuMetricsRepository>();
            mockILogger = new Mock<ILogger<CpuMetricsController>>();
            _cpuMetricsController = new CpuMetricsController(mockICpuMetricsRepository.Object, mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _cpuMetricsController.GetMetricsByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

    }
}
