using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Response;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _repository = repository;
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
                    response.Metrics.Add(new DotNetMetricDto
                    {
                        Time = metric.Time,
                        Value = metric.Value,
                        Id = metric.Id
                    });
                }
            }            
            return Ok(response);
        }
    }
}
