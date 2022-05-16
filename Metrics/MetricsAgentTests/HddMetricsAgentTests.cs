using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsAgentTests
    {
        private HddMetricsController _hddMetricsController;
        private Mock<IHddMetricsRepository> _mockIHddMetricsRepository;
        private Mock<ILogger<HddMetricsController>> _mockILogger;

        public HddMetricsAgentTests()
        {
            _mockIHddMetricsRepository = new Mock<IHddMetricsRepository>();
            _mockILogger = new Mock<ILogger<HddMetricsController>>();
            _hddMetricsController = new HddMetricsController(_mockIHddMetricsRepository.Object, _mockILogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _hddMetricsController.GetMetricsByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
