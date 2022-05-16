using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentTests
    {
        private DotNetMetricsController _dotNetMetricsController;

        public DotNetMetricsAgentTests()
        {
            _dotNetMetricsController = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {

            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _dotNetMetricsController.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
