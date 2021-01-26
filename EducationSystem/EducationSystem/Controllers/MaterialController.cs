using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    // https://localhost:50221/api/material/
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialContoller : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private MaterialRepository _repo;

        public MaterialContoller()
        {
            _repo = new MaterialRepository();
        }

        [HttpGet]
        public ActionResult GetMaterial()
        {
            var courses = _repo.GetMaterials();
            return Ok(courses);
        }

        // https://localhost:50221/api/material/2
        [HttpGet("{id}")]
        public ActionResult GetMaterialById(int id)
        {
            var course = _repo.GetMaterialById(id);
            return Ok(course);
        }

        [HttpPost("new-material")]
        public ActionResult AddNewMaterial([FromBody] MaterialDto newGroup)
        {
            _repo.GetMaterialsAdd(newGroup);
            return Ok("Материалы добавлены");
        }

        // https://localhost:50221/material/2
        [HttpPut("{id}")]
        public ActionResult UpdateMaterial(MaterialDto group)
        {
            _repo.UpdateMaterial(group);
            return Ok("success");
        }

        // https://localhost:50221/api/material/2
        [HttpDelete("{id}")]
        public ActionResult DeleteMaterial(int id)
        {
            _repo.DeleteMaterialById(id);
            return Ok("success");
        }
    }
}
