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
            return _courseRepo.GetCourses();
        }

        public CourseDto GetCourseById(int id)
        {
            return _courseRepo.GetCourseById(id);
        }

        public int UpdateCourse (CourseDto course)
        {
            int index=_courseRepo.UpdateCourse(course);
            if (index <= 0)
                return -1;
            if (course.Themes != null && course.Themes.Count > 0)
            {
               
                foreach (var theme in course.Themes)
                {
                    if (_courseRepo.AddCourse_Theme(index, theme.Id) <= 0) //если такая запись уже имеется??
                    {
                        //метод для удаления тем из списка курса        
                        return -2;
                    }
                }
               
            }
            return 0;
        }
        public int AddCourse(CourseDto course)
        {
            int index = _courseRepo.AddCourse(course);
            if (index <= 0)
                return -1;
            if(course.Themes!=null && course.Themes.Count>0)
            {
                foreach(var theme in course.Themes)
                {
                    if (_courseRepo.AddCourse_Theme(index, theme.Id) <= 0)
                    {
                        //метод для удаления тем из списка курса
                        return -2;
                    }
                }
            }
            return index;
        }

        public int RemoveCourse(int id)
        {
           return _courseRepo.DeleteCourse(id);
             
        }
    }
}
