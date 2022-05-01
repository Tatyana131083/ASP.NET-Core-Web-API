using Microsoft.AspNetCore.Mvc;
using System;


namespace WeatherManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherCrudController : ControllerBase
    {
        private  WeatherHistory _history; 

        public WeatherCrudController(WeatherHistory history)
        {
            _history = history;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string dateTime, [FromQuery] int temperature)
        {
            DateTime dateTimeParse = DateTime.Parse(dateTime);
            _history.Add(dateTimeParse, temperature);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            DateTime dateTimeFrom = DateTime.Parse(dateFrom);
            DateTime dateTimeTo = DateTime.Parse(dateTo);
            return Ok(_history.Get(dateTimeFrom, dateTimeTo));
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string dateTime, [FromQuery] int newTemperature)
        {
            DateTime dateTimeParse = DateTime.Parse(dateTime);
            for(int i = 0; i < _history.Values.Count; i++)
            {
                if(_history.Values[i].DateTime == dateTimeParse)
                {
                    Value tmp = _history.Values[i];
                    tmp.Temperature = newTemperature;
                    _history.Values[i] = tmp;    
                }
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            DateTime dateTimeFrom = DateTime.Parse(dateFrom);
            DateTime dateTimeTo = DateTime.Parse(dateTo);
            _history.Remove(dateTimeFrom, dateTimeTo);
            return Ok();
        }
    }
}
