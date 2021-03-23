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

        private IMapper _mapper;
        private IMaterialService _service;

        public MaterialController(IMapper mapper, IMaterialService materialService)
        {
            _mapper = mapper;
            _service = materialService;
        }

        // https://localhost:44365/api/material/
        /// <summary>Get all materials </summary>
        /// <returns>List of all materials</returns>
        [ProducesResponseType(typeof(List<MaterialOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist, Student")]
        public ActionResult<List<MaterialOutputModel>> GetMaterials()
        {
            return Ok(_mapper.Map<List<MaterialOutputModel>>(_service.GetMaterials()));

        }

        // https://localhost:44365/api/material/by-group/340
        /// <summary>Get all materials related to group</summary>
        /// <param name="id">Id of group, which materials is needed</param>
        /// <returns>List of attached materials to group</returns>
        [ProducesResponseType(typeof(List<MaterialOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-group/{id}")]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist, Student")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByGroupId(int id)
        {
            return Ok(_mapper.Map<List<MaterialOutputModel>>(_service.GetMaterialsByGroupId(id)));

        }

        // https://localhost:44365/api/material/by-tag/340
        /// <summary>Get all materials related to tag</summary>
        /// <param name="id">Id of tag, which materials is needed</param>
        /// <returns>List of attached materials to tag</returns>
        [ProducesResponseType(typeof(List<MaterialOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-tag/{id}")]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist, Student")]
        public ActionResult<List<MaterialOutputModel>> GetMaterialsByTagId(int id)
        {
            return Ok(_mapper.Map<List<MaterialOutputModel>>(_service.GetMaterialsByTagId(id)));
        }

        // https://localhost:44365/api/material/2
        /// <summary>Get material by Id</summary>
        /// <param name="id">Id of material</param>
        /// <returns>Material</returns>
        [ProducesResponseType(typeof(MaterialOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist, Student")]
        public ActionResult<MaterialOutputModel> GetMaterialById(int id)
        {
            return Ok(_mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material
        /// <summary>Add new material to DataBase</summary>
        /// <param name="materialInputModel">Info about added material (Source link, Description of content)</param>
        /// <returns>Added material</returns>
        [ProducesResponseType(typeof(MaterialOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist")]
        public ActionResult<MaterialOutputModel> AddNewMaterial([FromBody] MaterialInputModel materialInputModel)
        {
            var id = _service.AddMaterial(_mapper.Map<MaterialDto>(materialInputModel));
            return Ok(_mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id)));
        }

        // https://localhost:44365/api/material/8
        /// <summary>Update material</summary>
        /// <param name="id">Id of update material</param>
        /// <param name="material">All info about update material</param>
        /// <returns>Update material</returns>
        [ProducesResponseType(typeof(MaterialOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Teacher, Tutor, Methodist")]
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
        /// <returns>Update material, which is deleted</returns>
        [ProducesResponseType(typeof(MaterialOutputModel), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Teacher, Tutor")]
        public ActionResult<MaterialOutputModel> DeleteMaterial(int id)
        {
             _service.DeleteMaterial(id);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/material/id/recovery
        /// <summary>Change value of parametr "IsDeleted" to 0(Not deleted)</summary>
        /// <param name="id">Id of material</param>
        /// <returns>Update material, which is recovered</returns>
        [ProducesResponseType(typeof(MaterialOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Admin, Teacher, Tutor")]
        public ActionResult<MaterialOutputModel> RecoverMaterial(int id)
        {
            _service.RecoverMaterial(id);
            var result = _mapper.Map<MaterialOutputModel>(_service.GetMaterialById(id));
            return Ok(result);
        }


    }
}
