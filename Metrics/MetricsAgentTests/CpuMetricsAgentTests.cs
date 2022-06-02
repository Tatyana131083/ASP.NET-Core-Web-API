using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentTests
    {
        private CpuMetricsController _cpuMetricsController;
        private Mock<ICpuMetricsRepository> mockICpuMetricsRepository;
        private Mock<ILogger<CpuMetricsController>> mockILogger;
        private Mock<IMapper> mockMapper;

        public CpuMetricsAgentTests()
        {
            mockICpuMetricsRepository = new Mock<ICpuMetricsRepository>();
            mockILogger = new Mock<ILogger<CpuMetricsController>>();
            mockMapper = new Mock<IMapper>();
            _cpuMetricsController = new CpuMetricsController(mockMapper.Object, mockICpuMetricsRepository.Object, mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            mockICpuMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<CpuMetric>());

            _cpuMetricsController.GetMetricsByTimePeriod(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            mockICpuMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }

    }
}
