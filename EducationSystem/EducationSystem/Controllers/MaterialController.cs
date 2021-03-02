using AutoMapper;
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

        private MaterialMapper _matMapper;
        private IMapper _mapper;
        private IMaterialService _service;

        public MaterialController(IMapper mapper, IMaterialService materialService)
        {
            _matMapper = new MaterialMapper();
            _mapper = mapper;
            _service = materialService;
        }

        // https://localhost:44365/api/material/by-group/340
        [HttpGet("by-group/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialsByGroupId(int id)
        {
            return Ok(_matMapper.FromDtos(_service.GetMaterialsByGroupId(id)));

        }

        // https://localhost:44365/api/material/by-tag/340
        [HttpGet("by-tag/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialsByTagId(int id)
        {
            return Ok(_matMapper.FromDtos(_service.GetMaterialsByTagId(id)));
        }

        // https://localhost:44365/api/material/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetMaterialById(int id)
        {
            return Ok(_matMapper.FromDto(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            _service.AddMaterial(_matMapper.ToDto(materialInputModel));
            return Ok("Материалы добавлены");
        }

        // https://localhost:44365/api/material/8
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult UpdateMaterial(int id, [FromBody] MaterialInputModel material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            dto.Id = id;
            _service.UpdateMaterial(dto);
            return Ok("Данные обновлены");
        }

        // https://localhost:44365/api/material/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteMaterial(int id)
        {
            var result = _service.DeleteMaterial(id);
            if (result == 1)
                return Ok($"Материал #{id} удален!");
            else
                return Problem($"Ошибка! Не удалось удалить материал #{id}!");
        }

        // https://localhost:44365/api/material/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult RecoverMaterial(int id)
        {
            var result = _service.RecoverMaterial(id);
            if (result == 1)
                return Ok($"Материал #{id} восстановлен!");
            else
                return Problem($"Ошибка! Не удалось восстановить материал #{id}!");
        }


    }
}
