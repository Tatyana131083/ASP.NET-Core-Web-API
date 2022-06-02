using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Response;
using System.Collections.Generic;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(IMapper mapper, IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
     

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Ok(response);
        }


        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByTimePeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(LogEvents.GetMetricsFromAgent, "Getting metrics from {fromTime} to {toTime}", fromTime, toTime);
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);
            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            if(metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
                }
            }            
            return Ok(response);
        }
    }
}
