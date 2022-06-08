using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentsRepository;
        private readonly IMapper _mapper;

        public AgentsController(IAgentRepository agentsRepository, IMapper mapper)
        {
            _agentsRepository = agentsRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult RegisterAgent([FromBody] AgentCreateRequest request)
        {
            _agentsRepository.Create(_mapper.Map<AgentInfo>(request));

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult UpdateAgent([FromBody] AgentInfo request)
        {
            _agentsRepository.Update(request);

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteAgent([FromQuery] int id)
        {
            _agentsRepository.Delete(id);

            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetAllAgents()
        {
            var agents = _agentsRepository.GetAll();
            var response = new AgentsResponse()
            {
                Agents = new List<AgentInfo>()
            };

            foreach (var agent in agents)
                response.Agents.Add(agent);

            return Ok(response);
        }

        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            AgentInfo response = _agentsRepository.GetById(id);          
            return Ok(response);
        }
    }
}
