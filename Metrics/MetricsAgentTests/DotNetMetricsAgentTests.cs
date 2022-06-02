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
    public class DotNetMetricsAgentTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        private Mock<IDotNetMetricsRepository> _mockIDotNetMetricsRepository;
        private Mock<ILogger<DotNetMetricsController>> _mockILogger;
        private Mock<IMapper> _mockMapper;

        public DotNetMetricsAgentTests()
        {
            _mockIDotNetMetricsRepository = new Mock<IDotNetMetricsRepository>();
            _mockILogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _dotNetMetricsController = new DotNetMetricsController(_mockMapper.Object, _mockIDotNetMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            _mockIDotNetMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<DotNetMetric>());

            _dotNetMetricsController.GetMetricsByTimePeriod(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockIDotNetMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
