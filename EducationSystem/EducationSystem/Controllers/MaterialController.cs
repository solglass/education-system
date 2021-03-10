using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
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

        private IMapper _mapper;
        private IMaterialService _service;

        public MaterialController(IMapper mapper, IMaterialService materialService)
        {
            _mapper = mapper;
            _service = materialService;
        }

        // https://localhost:44365/api/material/by-group/340
        [HttpGet("by-group/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByGroupId(int id)
        {
            return Ok(_mapper.Map<List<MaterialOutputModel>>(_service.GetMaterialsByGroupId(id)));

        }

        // https://localhost:44365/api/material/by-tag/340
        [HttpGet("by-tag/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByTagId(int id)
        {
            return Ok(_mapper.Map<List<MaterialOutputModel>>(_service.GetMaterialsByTagId(id)));
        }

        // https://localhost:44365/api/material/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<MaterialOutputModel> GetMaterialById(int id)
        {
            return Ok(_mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult<MaterialOutputModel> AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            _service.AddMaterial(_mapper.Map<MaterialDto>(materialInputModel));
            return Ok("Материалы добавлены");
        }

        // https://localhost:44365/api/material/8
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult<MaterialOutputModel> UpdateMaterial(int id, [FromBody] MaterialInputModel material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            dto.Id = id;
            _service.UpdateMaterial(dto);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<MaterialOutputModel> DeleteMaterial(int id)
        {
             _service.DeleteMaterial(id);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<MaterialOutputModel> RecoverMaterial(int id)
        {
            _service.RecoverMaterial(id);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }


    }
}
