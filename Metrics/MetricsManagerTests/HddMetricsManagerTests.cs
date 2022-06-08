using MetricsManager.Controllers;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;


namespace MetricsManagerTests
{
    public class HddMetricsManagerTests
    {
        private HddMetricsController _hddMetricsController;
        private Mock<ILogger<HddMetricsController>> _mockILogger;
        private Mock<IMetricsAgentClient> _mockIMetricsAgentClient;

        public HddMetricsManagerTests()
        {
            _mockILogger = new Mock<ILogger<HddMetricsController>>();
            _mockIMetricsAgentClient = new Mock<IMetricsAgentClient>();
            _hddMetricsController = new HddMetricsController(_mockILogger.Object, _mockIMetricsAgentClient.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _hddMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _hddMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
