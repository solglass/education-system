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
        private HomeworkAttemptMapper _homeworkAttemptMapper;

        public HomeworkController()
        {
            _repo = new HomeworkRepository();
            _homeworkMapper = new HomeworkMapper();
            _homeworkAttemptMapper = new HomeworkAttemptMapper();
        }

        [HttpPost]
        public ActionResult CreateHomework([FromBody] HomeworkInputModel inputModel)
        {
            HomeworkDto homework;
            try
            {
                homework = _homeworkMapper.ToDto(inputModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var result = _repo.AddHomework(homework);
            return Ok($"Домашнее задание №{result} добавлено!");

        }

        [HttpGet("id")]
        public ActionResult GetHomeworkAttemptsByHomeworkId(int id)
        {
            var outputModel = _homeworkAttemptMapper.FromDtos(_repo.GetHomeworkAttemptsByHomeworkId(id));
            return Ok(outputModel);
        }

    }

}