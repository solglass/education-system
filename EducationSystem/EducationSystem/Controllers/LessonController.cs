using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/lesson/
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private LessonRepository _repo;

        public LessonController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new LessonRepository();
        }

        // https://localhost:50221/api/lesson/
        [HttpPost]
        public ActionResult AddNewLesson(LessonDto lessonDto)
        {
            _repo.AddLesson(lessonDto);
            return Ok("Add success");
        }

        // https://localhost:50221/api/lesson/
        [HttpGet]
        public ActionResult GetLesson()
        {
            var result = _repo.GetLessons();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpGet("{id}")]
        public ActionResult GetLessonById(int id)
        {
            var result = _repo.GetLessonById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpDelete("{id}")]
        public ActionResult DeleteLesson(int id)
        {
            _repo.DeleteLesson(id);
            return Ok("Delete success");
        }
        // https://localhost:50221/api/lesson/
        [HttpPost("update")]
        public ActionResult UpdateLesson([FromBody]LessonDto lessonDto)
        {
            _repo.UpdateLesson(lessonDto);
            return Ok("Update success");
        }

        // https://localhost:50221/api/feedback/
        [HttpGet]
        public ActionResult GetFeedbacks()
        {
            var result = _repo.GetFeedbacks();
            return Ok(result);
        }

        // https://localhost:50221/api/feedback/3
        [HttpGet("{id}")]
        public ActionResult GetFeedbackById(int id)
        {
            var result = _repo.GetFeedbackById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/feedback/
        [HttpPost]
        public ActionResult AddNewFeedback(FeedbackDto feedbackDto)
        {
            _repo.AddFeedback(feedbackDto);
            return Ok("Add success");
        }

        // https://localhost:50221/api/feedback/update
        [HttpPost("update")]
        public ActionResult UpdateFeedback([FromBody]FeedbackDto feedbackDto)
        {
            _repo.UpdateFeedback(feedbackDto);
            return Ok("Update Success");
        }

        // https://localhost:50221/api/feedback/3
        [HttpDelete("{id}")]
        public ActionResult DeleteFeedback(int id)
        {
            _repo.DeleteFeedback(id);
            return Ok("Delete success");
        }

        // https://localhost:50221/api/understandingLevel/
        [HttpGet]
        public ActionResult GetUnderstandingLevels()
        {
            var result = _repo.GetUnderstandingLevels();
            return Ok(result);
        }

        // https://localhost:50221/api/understandingLevel/3
        [HttpGet("{id}")]
        public ActionResult GetUnderstandingLevelById(int id)
        {
            var result = _repo.GetUnderstandingLevelById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/understandingLevel/
        [HttpPost]
        public ActionResult AddNewUnderstandingLevel(UnderstandingLevelDto understandingLevel)
        {
            _repo.AddUnderstandingLevel(understandingLevel);
            return Ok("Add success");
        }

        // https://localhost:50221/api/understandingLevel/update
        [HttpPost("update")]
        public ActionResult UpdateUnderstandingLevel([FromBody]UnderstandingLevelDto understandingLevelDto)
        {
            _repo.UpdateUnderstandingLevel(understandingLevelDto);
            return Ok("Update success");
        }

        // https://localhost:50221/api/UnderstandingLevel/3
        [HttpDelete("{id}")]
        public ActionResult DeleteUnderstandingLevel(int id)
        {
            _repo.DeleteFeedback(id);
            return Ok("Delete success");
        }

        // https://localhost:50221/api/attendance/
        [HttpGet]
        public ActionResult GetAttendances()
        {
            var result = _repo.GetAttendances();
            return Ok(result);
        }

        // https://localhost:50221/api/attendance/3
        [HttpGet("{id}")]
        public ActionResult GetAttendanceById(int id)
        {
            var result = _repo.GetAttendanceById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/attendance/
        [HttpPost]
        public ActionResult AddNewAttendance(AttendanceDto attendance)
        {
            _repo.AddAttendance(attendance);
            return Ok("Add success");
        }

        // https://localhost:50221/api/attendance/update
        [HttpPost("update")]
        public ActionResult UpdateAttendance([FromBody]AttendanceDto attendance)
        {
            _repo.UpdateAttendance(attendance);
            return Ok("Update success");
        }

        // https://localhost:50221/api/attendance/3
        [HttpDelete("{id}")]
        public ActionResult DeleteAttendance(int id)
        {
            _repo.DeleteAttendance(id);
            return Ok("Delete success");
        }

        // https://localhost:50221/api/lesson-theme/
        [HttpGet]
        public ActionResult GetLessonTheme()
        {
            var result = _repo.GetLessonThemes();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpGet("{id}")]
        public ActionResult GetLessonThemeById(int id)
        {
            var result = _repo.GetLessonThemeById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson-theme/
        [HttpPost]
        public ActionResult AddNewLessonTheme(LessonThemeDto lessontheme)
        {
            _repo.AddLessonTheme(lessontheme);
            return Ok("Add success");
        }

        // https://localhost:50221/api/lesson-theme/update
        [HttpPost("update")]
        public ActionResult UpdateLessonTheme([FromBody]LessonThemeDto lessonTheme)
        {
            _repo.UpdateLessonTheme(lessonTheme);
            return Ok("Update success");
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpDelete("{id}")]
        public ActionResult DeleteLessonTheme(int id)
        {
            _repo.DeleteAttendance(id);
            return Ok("Delete success");
        }
    }
}
