using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface ICourseRepository
    {
        int AddCourse(CourseDto course);
        int AddCourse_Theme(int courseId, int themeId);
        int AddTheme(ThemeDto theme);
        int DeleteCourse(int id);
        int DeleteCourse_Theme(int courseId, int themeId);
        int DeleteTheme(int id);
        CourseDto GetCourseById(int id);
        List<CourseDto> GetCourses();
        List<Course_ThemeDto> GetCourseThemeByThemeId(int id);
        ThemeDto GetThemeById(int id);
        List<ThemeDto> GetThemes();
        List<ThemeDto> GetThemesByCourseId(int id);
        List<ThemeDto> GetUncoveredThemesByGroupId(int id);
        int HardDeleteCourse(int id);
        int UpdateCourse(CourseDto course);
        int UpdateTheme(ThemeDto theme);
    }
}