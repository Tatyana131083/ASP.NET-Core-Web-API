using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с агентами")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentsRepository;
        private readonly IMapper _mapper;

        public AgentsController(IAgentRepository agentsRepository, IMapper mapper)
        {
            _agentsRepository = agentsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Регистация нового агента
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(description: "Регистрация нового агента в системе мониторинга")]
        [SwaggerResponse(200, "Успешная операция")]
        public IActionResult RegisterAgent([FromBody] AgentCreateRequest request)
        {
            _agentsRepository.Create(_mapper.Map<AgentInfo>(request));

            return Ok();
        }

        /// <summary>
        /// Удаление агента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [SwaggerOperation(description: "Удаление агента в системе мониторинга")]
        [SwaggerResponse(200, "Успешная операция")]
        public IActionResult DeleteAgent([FromQuery] int id)
        {
            _agentsRepository.Delete(id);

            return Ok();
        }

        /// <summary>
        /// Получение всех агентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        [SwaggerOperation(description: "Получение всех зарегистрированных агентов в системе мониторинга")]
        [SwaggerResponse(200, "Успешная операция")]
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

        /// <summary>
        /// Получение агента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getById")]        
        [SwaggerOperation(description: "Получение агента по id в системе мониторинга")]
        [SwaggerResponse(200, "Успешная операция")]
        [ProducesResponseType(typeof(AgentInfo), StatusCodes.Status200OK)]
        public IActionResult GetById([FromQuery] int id)
        {
            AgentInfo response = new AgentInfo();
            response = _agentsRepository.GetById(id);          
            return Ok(response);
        }
    }
}
