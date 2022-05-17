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
    public class RamMetricsAgentTests
    {
        private RamMetricsController _ramMetricsController;
        private Mock<IRamMetricsRepository> _mockIRamMetricsRepository;
        private Mock<ILogger<RamMetricsController>> _mockILogger;
        private Mock<IMapper> _mockMapper;

        public RamMetricsAgentTests()
        {
            _mockIRamMetricsRepository = new Mock<IRamMetricsRepository>();
            _mockILogger = new Mock<ILogger<RamMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _ramMetricsController = new RamMetricsController(_mockMapper.Object, _mockIRamMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            _mockIRamMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<RamMetric>());

            _ramMetricsController.GetMetricsByTimePeriod(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockIRamMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
