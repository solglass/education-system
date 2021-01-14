using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public dynamic Get()
        {
            var repo = new HomeworkRepository();
            return repo.GetHomeworks();
        }
    }
}
