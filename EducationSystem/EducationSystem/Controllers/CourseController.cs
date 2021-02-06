using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
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
    // https://localhost:50221/api/course/
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private CourseRepository _repo;
        private CourseMapper _courseMapper;
        private ThemeMapper _themeMapper;
        private CourseService _courseService;
        public CourseController()
        {
            _repo = new CourseRepository();
            _courseMapper = new CourseMapper();
            _themeMapper = new ThemeMapper();
            _courseService = new CourseService();
        }

        [HttpGet]
        public ActionResult GetCourses()
        {
            List<CourseOutputModel> courses;
            try
            {
                courses = _courseMapper.FromDtos(_courseService.GetCourses());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(courses);
        }

        [HttpGet("{id}")]
       public ActionResult GetCourse(int id)       
        {
            CourseOutputModel course;
            try
            {
                 course = _courseMapper.FromDto(_courseService.GetCourseById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(course);
        }


        [HttpPost]
        public ActionResult CreateCourse([FromBody] CourseInputModel course)    
        {
            int result;
            try
            {
                result = _courseService.AddCourse(_courseMapper.ToDto(course));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result > 0)
                return Ok($"Курс №{result} добавлен!");
            else if (result == -1)
                return Problem("Ошибка! Не получилось добавить курс!");
            else
                return Problem($"Ошибка! К созданному курсу не удалось привязать темы!") ;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCourseInfo(int id, [FromBody] CourseInputModel course)
        {
            int result;
            try
            {
                result = _courseService.UpdateCourse(_courseMapper.ToDto(course));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result == 0)
                return Ok("Курс обновлен!");
            else if (result == -1)
                return Problem("Ошибка! Не получилось обновить курс!");
            else
                return Problem($"Ошибка! К обновленному курсу не удалось привязать темы!"); 
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveCourseInfo(int id)
        {
            var result = _courseService.RemoveCourse(id);
            if (result ==1)
                return Ok("Курс удален!");
            else
                return Problem("Ошибка! Не получилось удалить выбранный курс!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpPost("{courseId}/theme/{themeId}")]
        public ActionResult AddThemeToCourse(int courseId, int themeId)
        {
            int result = _courseService.AddThemeToCourse(courseId, themeId);
            if (result > 0) 
                return Ok(result);
            else 
                return Problem("Ошибка! Не получилось добавить к курсу тему!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpDelete("{courseId}/theme/{themeId}")]
        public ActionResult RemoveThemeFromCourse(int courseId, int themeId)
        {
            var result = _repo.DeleteCourse_Theme(courseId, themeId);
            if (result > 0)
                return Ok(result);
            else
                return Problem("о-ё-ё-й");
        }


        [HttpGet("themes")]
        public ActionResult GetThemes()
        {
            List<ThemeOutputModel> themes;
            try
            {
                themes = _themeMapper.FromDtos(_repo.GetThemes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(themes);
        }


        [HttpGet("theme/{id}")]
        public ActionResult GetTheme(int id)
        {
            ThemeOutputModel theme;
            try
            {
                theme = _themeMapper.FromDto(_repo.GetThemeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(theme);

        }

        [HttpPost("theme")]
        public ActionResult CreateTheme([FromBody] ThemeInputModel inputModel)
        {
            ThemeDto themeDto;
            try
            {
               themeDto  = _themeMapper.ToDto(inputModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var result = _repo.AddTheme(themeDto.Name);
            if (result > 0)
                return Ok("Тема добавлена!");
            else
                return Problem("о-ё-ё-й!");
        }


        [HttpDelete("theme/{id}")]
        public ActionResult RemoveTheme(int id)
        {
            var result = _repo.DeleteTheme(id);
            if (result > 0)
                return Ok("Тема удалена!");
            else
                return Problem("Тема не обнаружена!");
        }
    
    }
}
