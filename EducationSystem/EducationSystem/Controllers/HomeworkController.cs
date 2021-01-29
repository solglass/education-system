using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
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
    [ApiController]
    [Route("api/[controller]")]
    public class HomeworkController : ControllerBase
    {

        private readonly ILogger<HomeworkController> _logger;
        private HomeworkRepository _repo;
        private HomeworkMapper _homeworkMapper;
        public HomeworkController()
        {
            _repo = new HomeworkRepository();
            _homeworkMapper = new HomeworkMapper();
        }

        [HttpPost]
        public ActionResult CreateHomework([FromBody] HomeworkInputModel inputModel)
        {
            HomeworkDto homework;
            try
            {
                homework = _homeworkMapper.ToDto(inputModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var result = _repo.AddHomework(homework);
            return Ok($"Домашнее задание №{result} добавлено!");

        }

    }
}
