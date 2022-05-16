using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsAgentTests
    {
        private HddMetricsController _hddMetricsController;

        public HddMetricsAgentTests()
        {
            _hddMetricsController = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _hddMetricsController.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
