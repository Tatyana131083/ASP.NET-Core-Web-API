using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        private Mock<IDotNetMetricsRepository> _mockIDotNetMetricsRepository;
        private Mock<ILogger<DotNetMetricsController>> _mockILogger;

        public DotNetMetricsAgentTests()
        {
            _mockIDotNetMetricsRepository = new Mock<IDotNetMetricsRepository>();
            _mockILogger = new Mock<ILogger<DotNetMetricsController>>();
            _dotNetMetricsController = new DotNetMetricsController(_mockIDotNetMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _dotNetMetricsController.GetMetricsByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
