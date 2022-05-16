using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsAgentTests
    {
        private RamMetricsController _ramMetricsController;
        private Mock<IRamMetricsRepository> _mockIRamMetricsRepository;
        private Mock<ILogger<RamMetricsController>> _mockILogger;

        public RamMetricsAgentTests()
        {
            _mockIRamMetricsRepository = new Mock<IRamMetricsRepository>();
            _mockILogger = new Mock<ILogger<RamMetricsController>>();
            _ramMetricsController = new RamMetricsController(_mockIRamMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _ramMetricsController.GetMetricsByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
