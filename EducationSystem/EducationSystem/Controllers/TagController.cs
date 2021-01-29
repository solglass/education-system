using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/tag/
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private TagRepository _repo;

        public TagController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new TagRepository();
        }

        // https://localhost:50221/api/tag/add
        [HttpPost("add")]
        public ActionResult Add([FromBody] dynamic user)
        {
            _repo.TagAdd(user);
            return Ok("Тег добавлен");
        }

        // https://localhost:50221/api/tag
        [HttpGet]
        public ActionResult GetTags()
        {
            var tags = _repo.GetTags();
            return Ok(tags);
        }

        // https://localhost:50221/api/tag/3
        [HttpGet("{id}")]
        public dynamic GetTag(int id)
        {
            var tag = _repo.GetTagById(id);
            return Ok(tag);
        }

        // https://localhost:50221/api/tag/3
        [HttpPut("{id}")]
        public ActionResult UpdateTag(int id, [FromBody] dynamic data) 
        {
            _repo.TagUpdate(data);
            return Ok("success");
        }

        // https://localhost:50221/api/tag/3
        [HttpDelete("{id}")]
        public ActionResult DeleteTag(int id) 
        {
            _repo.TagDelete(id);
            return Ok("success");
        }
    }
}
