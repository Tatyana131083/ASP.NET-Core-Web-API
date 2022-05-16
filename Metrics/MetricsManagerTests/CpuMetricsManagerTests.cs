using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsManagerTests
    {
        private CpuMetricsController _cpuMetricsController;
        private Mock<ILogger<CpuMetricsController>> _mockILogger;

        public CpuMetricsManagerTests()
        {
            _mockILogger = new Mock<ILogger<CpuMetricsController>>();
            _cpuMetricsController = new CpuMetricsController(_mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result =  _cpuMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _cpuMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
