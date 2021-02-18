using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
//using EducationSystem.Controllers;
//using EducationSystem.Data;
//using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    // https://localhost:50221/api/course/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private CourseMapper _courseMapper;
        private ThemeMapper _themeMapper;
        private CourseService _courseService;
        public CourseController()
        {
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
        [Authorize(Roles ="Админ, Менеджер, Методист")]
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
                return Problem($"Ошибка! К созданному курсу #{-(result+2)} не удалось привязать темы! ") ;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
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
                return Ok($"Курс #{id} обновлен!");
            else 
                return Problem($"Ошибка! Не получилось обновить курс #{id}!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult RemoveCourseInfo(int id)
        {
            var result = _courseService.RemoveCourse(id);
            if (result == 1)
                return Ok($"Курс #{id} удален!");
            else
                return Problem($"Ошибка! Не получилось удалить курс #{id}!") ;
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpPost("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult AddThemeToCourse(int courseId, int themeId)
        {
            int result = _courseService.AddThemeToCourse(courseId, themeId);
            if (result > 0) 
                return Ok($"Тема {themeId} добавлена к курсу #{courseId}");
            else 
                return Problem($"Ошибка! Не получилось добавить к курсу #{courseId} тему #{themeId}!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpDelete("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult RemoveThemeFromCourse(int courseId, int themeId)
        {
            var result = _courseService.RemoveThemeFromCourse(courseId, themeId);
            if (result== 0)
                return Ok($"Тема {themeId} отвязана от курса #{courseId}");
            else
                return Problem($"Ошибка! Не получилось отвязать тему #{themeId} от курса #{courseId}!");
        }
       

        [HttpGet("themes")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetThemes()
        {
            List<ThemeOutputModel> themes;
            try
            {
                themes = _themeMapper.FromDtos(_courseService.GetThemes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(themes);
        }

       
        [HttpGet("theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetTheme(int id)
        {
            ThemeOutputModel theme;
            try
            {
                theme = _themeMapper.FromDto(_courseService.GetThemeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(theme);

        }
        
        [HttpPost("theme")]
        [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult CreateTheme([FromBody] ThemeInputModel inputModel)
        {
            int result;
            try
            {
               result = _courseService.AddTheme(_themeMapper.ToDto(inputModel));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result > 0)
                return Ok($"Тема №{result} добавлена!");
            else
                return Problem($"Ошибка! К созданной теме #{-(result + 2)} не удалось привязать теги! ");
        }
        [HttpPost("theme/{themeId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель, Тьютор")]
        public ActionResult AddTagToTheme(int themeId, int tagId)
        {
            var result = _courseService.AddTagToTheme(themeId, tagId);
            if (result > 0)
                return Ok($"Тег #{tagId} добавлен к теме #{themeId}!");
            else
                return Problem($"Ошибка! Не получилось добавить тег  #{tagId} к теме #{themeId}!");
        }

       


        [HttpDelete("theme/{id}")]
        [Authorize(Roles = "Админ, Методист")]
        public ActionResult RemoveTheme(int id)
        {
            var result = _courseService.DeleteTheme(id);
            if (result > 0)
                return Ok($"Тема удалена #{id}!");
            else
                return Problem($"Ошибка! Не получилось удалить тему #{id}!");
        }

     
    }
}
