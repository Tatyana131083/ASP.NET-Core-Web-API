using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;
        private AgentPool _agentPool;

        public AgentsControllerTests()
        {
            _agentPool = new AgentPool();
            _agentsController = new AgentsController(_agentPool);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RegisterAgentTest(int agentId)
        {
            AgentInfo agentInfo = new AgentInfo() { AgentId = agentId, Enable = true };
            IActionResult actionResult = _agentsController.RegisterAgent(agentInfo);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

        [Fact]
        public void GetAgentsTest()
        {
            IActionResult actionResult = _agentsController.GetAllAgents();
            OkObjectResult result = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            Assert.NotNull(result.Value as IEnumerable<AgentInfo>);
        }
    }
}
