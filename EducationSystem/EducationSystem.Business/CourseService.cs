using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
   public class CourseService
    {
        private CourseRepository _courseRepo;
        private GroupRepository _groupRepo;
        public CourseService()
        {
            _courseRepo = new CourseRepository();
            _groupRepo = new GroupRepository();
        }

        public List<CourseDto> GetCourses()
        {
            var courses = _courseRepo.GetCourses();
            if(courses!=null)
            {
                foreach (var course in courses)
                {
                    course.Themes = _courseRepo.GetThemesByCourseId(course.Id);
                    course.Groups = _groupRepo.GetGroupsByCourseId(course.Id);
                }
            }
            return courses;
        }


    }
}
