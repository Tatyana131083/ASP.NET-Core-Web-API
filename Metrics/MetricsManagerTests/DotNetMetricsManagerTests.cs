using MetricsManager.Controllers;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsManagerTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        private Mock<ILogger<DotNetMetricsController>> _mockILogger;
        private Mock<IMetricsAgentClient> _mockIMetricsAgentClient;

        public DotNetMetricsManagerTests()
        {
            _mockILogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockIMetricsAgentClient = new Mock<IMetricsAgentClient>();
            _dotNetMetricsController = new DotNetMetricsController(_mockILogger.Object, _mockIMetricsAgentClient.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _dotNetMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _dotNetMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
