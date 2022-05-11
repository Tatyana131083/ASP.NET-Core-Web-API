using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentTests
    {
        private NetworkMetricsController _networkMetricsController;
        private Mock<INetworkMetricsRepository> _mockINetworkMetricsRepository;
        private Mock<ILogger<NetworkMetricsController>> _mockILogger;

        public NetworkMetricsAgentTests()
        {
            _mockINetworkMetricsRepository = new Mock<INetworkMetricsRepository>();
            _mockILogger = new Mock<ILogger<NetworkMetricsController>>();
            _networkMetricsController = new NetworkMetricsController(_mockINetworkMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _networkMetricsController.GetMetricsByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
