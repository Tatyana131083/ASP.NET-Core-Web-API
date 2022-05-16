using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsManagerTests
    {
        private RamMetricsController _ramMetricsController;

        public RamMetricsManagerTests()
        {
            _ramMetricsController = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _ramMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        [Fact]
        public void GetMetricsFromCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _ramMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
