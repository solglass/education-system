using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
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
        private MaterialService _service;
        private MaterialMapper _mapper;

        public MaterialContoller()
        {
            _mapper = new MaterialMapper();
            _service = new MaterialService();
        }

        [HttpGet]
        public ActionResult GetMaterials()
        {
            var courses = _service.GetMaterials();
            var result = _mapper.FromDtos(courses);
            return Ok(result);
        }

        // https://localhost:50221/api/material/2
        [HttpGet("{id}")]
        public ActionResult GetMaterialById(int id)
        {
            var course = _service.GetMaterialById(id);
            var result = _mapper.FromDto(course);
            return Ok(result);
        }

        [HttpPost("new-material")]
        public ActionResult AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            var newMaterial = _mapper.ToDto(materialInputModel);
            _service.AddMaterial(newMaterial);
            return Ok("Материалы добавлены");
        }

        // https://localhost:50221/material/2
        [HttpPut("{id}")]
        public ActionResult UpdateMaterial(int id, [FromBody] MaterialInputModel material)
        {
            _service.UpdateMaterial(id, _mapper.ToDto(material));
            return Ok("success");
        }

        // https://localhost:50221/api/material/2
        [HttpDelete("{id}")]
        public ActionResult DeleteMaterial(int id)
        {
            _service.DeleteMaterialById(id);
            return Ok("success");
        }
    }
}
