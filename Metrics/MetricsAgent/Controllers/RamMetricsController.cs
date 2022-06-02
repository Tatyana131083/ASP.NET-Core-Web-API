using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Response;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Requests;
using AutoMapper;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(IMapper mapper, IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            RamMetric ramMetric = _mapper.Map<RamMetric>(request);

            _repository.Create(ramMetric);


            if (_logger != null)
                _logger.LogDebug("Успешно добавили новую dotNet метрику: {0}", ramMetric);

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            return Ok(response);
        }
 

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByTimePeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(LogEvents.GetMetricsFromAgent, "Getting metrics from {fromTime} to {toTime}", fromTime, toTime);
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);
            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
                }
            }
                
            return Ok(response);
        }
    }
}
