using MetricsManager.Controllers;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;


namespace MetricsManagerTests
{
    public class RamMetricsManagerTests
    {
        private RamMetricsController _ramMetricsController;
        private Mock<ILogger<RamMetricsController>> _mockILogger;
        private Mock<IMetricsAgentClient> _mockIMetricsAgentClient;

        public RamMetricsManagerTests()
        {
            _mockILogger = new Mock<ILogger<RamMetricsController>>();
            _mockIMetricsAgentClient = new Mock<IMetricsAgentClient>();
            _ramMetricsController = new RamMetricsController(_mockILogger.Object, _mockIMetricsAgentClient.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _ramMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _ramMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
