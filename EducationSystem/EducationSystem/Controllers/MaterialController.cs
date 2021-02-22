using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Controllers
{
    // https://localhost:44365/api/material/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MaterialController : ControllerBase
    {

        private MaterialService _service;
        private MaterialMapper _mapper;

        public MaterialController()
        {
            _mapper = new MaterialMapper();
            _service = new MaterialService();
        }

        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult GetMaterials()
        {
            var courses = _service.GetMaterials();
            var result = _mapper.FromDtos(courses);
            return Ok(result);
        }

        // https://localhost:44365/api/material/340/tag
        [HttpGet("{id}/tag")]
        [AllowAnonymous]
        public ActionResult GetmaterialsByTagId(int id)
        {
            var result =_mapper.FromDtos( _service.GetMaterialsByTagId(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialById(int id)
        {
            var course = _service.GetMaterialById(id);
            var result = _mapper.FromDto(course);
            return Ok(result);
        }

        [HttpPost("new-material")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            var newMaterial = _mapper.ToDto(materialInputModel);
            _service.AddMaterial(newMaterial);
            return Ok("Материалы добавлены");
        }

        // https://localhost:50221/material/2
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult UpdateMaterial(int id, [FromBody] MaterialInputModel material)
        {
            _service.UpdateMaterial(id, _mapper.ToDto(material));
            return Ok("success");
        }

        // https://localhost:50221/api/material/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult DeleteMaterial(int id)
        {
            _service.DeleteMaterialById(id);
            return Ok("success");
        }
    }
}
