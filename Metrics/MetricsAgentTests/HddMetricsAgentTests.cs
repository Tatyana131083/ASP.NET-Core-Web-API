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
    public class HddMetricsAgentTests
    {
        private HddMetricsController _hddMetricsController;
        private Mock<IHddMetricsRepository> _mockIHddMetricsRepository;
        private Mock<ILogger<HddMetricsController>> _mockILogger;
        private Mock<IMapper> _mockMapper;

        public HddMetricsAgentTests()
        {
            _mockIHddMetricsRepository = new Mock<IHddMetricsRepository>();
            _mockILogger = new Mock<ILogger<HddMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _hddMetricsController = new HddMetricsController(_mockMapper.Object, _mockIHddMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            _mockIHddMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<HddMetric>());

            _hddMetricsController.GetMetricsByTimePeriod(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockIHddMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
