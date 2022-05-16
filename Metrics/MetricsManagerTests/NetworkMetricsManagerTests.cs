using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;


namespace MetricsManagerTests
{
    public class NetworkMetricsManagerTests
    {
        private NetworkMetricsController _networkMetricsController;
        private Mock<ILogger<NetworkMetricsController>> _mockILogger;

        public NetworkMetricsManagerTests()
        {
            _mockILogger = new Mock<ILogger<NetworkMetricsController>>();
            _networkMetricsController = new NetworkMetricsController(_mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _networkMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _networkMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
