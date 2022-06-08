using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;
        private Mock<IAgentRepository> _mockIAgentRepository;
        private Mock<IMapper> _mockIMapper;

        public AgentsControllerTests()
        {
            _mockIAgentRepository = new Mock<IAgentRepository>();
            _mockIMapper = new Mock<IMapper>();
            _agentsController = new AgentsController(_mockIAgentRepository.Object, _mockIMapper.Object);
        }

        [Theory]
        [InlineData("https://localhost:44354/")]
        [InlineData("https://localhost:44355/")]
        [InlineData("https://localhost:44356/")]
        public void RegisterAgent(string url)
        {
            AgentCreateRequest agent = new AgentCreateRequest() { Url = url};
            IActionResult actionResult = _agentsController.RegisterAgent(agent);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

    }
}
