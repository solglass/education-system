using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <summary>Get all materials related to group</summary>
        /// <param name="id">Id of group, which materials is needed</param>
        /// <returns>List of attached materials to group</returns>
        [HttpGet("by-group/{id}")]
        [ProducesResponseType(typeof(List<MaterialOutputModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByGroupId(int id)
        {
            return Ok(_matMapper.FromDtos(_service.GetMaterialsByGroupId(id)));
        }

        // https://localhost:44365/api/material/by-tag/340
        /// <summary>Get all materials related to tag</summary>
        /// <param name="id">Id of tag, which materials is needed</param>
        /// <returns>List of attached materials to tag</returns>
        [HttpGet("by-tag/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByTagId(int id)
        {
            return Ok(_matMapper.FromDtos(_service.GetMaterialsByTagId(id)));
        }

        // https://localhost:44365/api/material/2
        /// <summary>Get material by Id</summary>
        /// <param name="id">Id of material</param>
        /// <returns>Material</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<MaterialOutputModel> GetMaterialById(int id)
        {
            return Ok(_matMapper.FromDto(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material
        /// <summary>Add new material to DataBase</summary>
        /// <param name="materialInputModel">Info about added material (Source link, Description of content)</param>
        /// <returns>Added material</returns>
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult<MaterialOutputModel> AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            var id = _service.AddMaterial(_matMapper.ToDto(materialInputModel));
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/8
        /// <summary>Get all materials related to tag</summary>
        /// <param name="id">Id of update material</param>
        /// <param name="material">All info about update material</param>
        /// <returns>Update material</returns>
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
        /// <summary>Change value of parametr "IsDeleted" to 1(Deleted)</summary>
        /// <param name="id">Id of material</param>
        /// <returns>Update material</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<MaterialOutputModel> DeleteMaterial(int id)
        {
             _service.DeleteMaterial(id);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/id/recovery
        /// <summary>Change value of parametr "IsDeleted" to 0(Not deleted)</summary>
        /// <param name="id">Id of material</param>
        /// <returns>Update material</returns>
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
