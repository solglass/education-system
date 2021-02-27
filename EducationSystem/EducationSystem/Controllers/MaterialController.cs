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
    [Authorize]
    public class MaterialController : ControllerBase
    {

        private MaterialService _service;
        private MaterialMapper _mapper;

        public MaterialController()
        {
            _mapper = new MaterialMapper();
            _service = new MaterialService();
        }

        // https://localhost:44365/api/material/340/group
        [HttpGet("{id}/group")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialsByGroupId(int id)
        {
            return Ok (_mapper.FromDtos(_service.GetMaterialsByGroupId(id)));
            
        }

        // https://localhost:44365/api/material/340/tag
        [HttpGet("{id}/tag")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialsByTagId(int id)
        {
            return Ok (_mapper.FromDtos( _service.GetMaterialsByTagId(id)));
        }

        // https://localhost:44365/api/material/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialById(int id)
        {
            return Ok(_mapper.FromDto(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            _service.AddMaterial(_mapper.ToDto(materialInputModel));
            return Ok("Материалы добавлены");
        }

        // https://localhost:44365/material/2
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult UpdateMaterial(int id, [FromBody] MaterialInputModel material)
        {
            _service.UpdateMaterial(id, _mapper.ToDto(material));
            return Ok("Данные обновлены");
        }

        // https://localhost:44365/api/material/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult DeleteMaterial(int id)
        {
            _service.DeleteMaterialById(id);
            return Ok("Материалы удалены");
        }
    }
}
