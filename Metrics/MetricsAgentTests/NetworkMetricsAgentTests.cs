using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using AutoMapper;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentTests
    {
        private NetworkMetricsController _networkMetricsController;
        private Mock<INetworkMetricsRepository> _mockINetworkMetricsRepository;
        private Mock<ILogger<NetworkMetricsController>> _mockILogger;
        private Mock<IMapper> _mockMapper;

        public NetworkMetricsAgentTests()
        {
            _mockINetworkMetricsRepository = new Mock<INetworkMetricsRepository>();
            _mockILogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _networkMetricsController = new NetworkMetricsController(_mockMapper.Object, _mockINetworkMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            _mockINetworkMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<NetworkMetric>());

            _networkMetricsController.GetMetricsByTimePeriod(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockINetworkMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
