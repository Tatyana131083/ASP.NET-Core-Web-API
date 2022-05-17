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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(IMapper mapper, INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            NetworkMetric networkMetric = _mapper.Map<NetworkMetric>(request);

            _repository.Create(networkMetric);


            if (_logger != null)
                _logger.LogDebug("Успешно добавили новую network метрику: {0}", networkMetric);

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new NetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Ok(response);
        }
 
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByTimePeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(LogEvents.GetMetricsFromAgent, "Getting metrics from {fromTime} to {toTime}", fromTime, toTime);
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);
            var response = new NetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
                }
            }
               
            return Ok(response);
        }
    }
}
