using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface ICourseRepository
    {
        int AddCourse(CourseDto course);
        int AddCourse_Material(int courseId, int materialId);
        int AddTheme(ThemeDto theme);
        List<ThemeDto> GetThemesByHomeworkId(int id);
        int DeleteCourse_Material(int courseId, int materialId);
        int DeleteOrRecoverCourse(int id, bool isDeleted);
        int DeleteOrRecoverTheme(int id, bool isDeleted);
        CourseDto GetCourseById(int id);
        List<CourseDto> GetCourses();
        ThemeDto GetThemeById(int id);
        List<ThemeDto> GetThemes();
        List<ThemeDto> GetUncoveredThemesByGroupId(int id);
        int HardDeleteCourse(int id);
        int UpdateCourse(CourseDto course);
        int UpdateTheme(ThemeDto theme);
        int HardDeleteTheme(int themeId);

        int DeleteCourse_Program(int courseId);
        List<OrderedThemeDto> GetCourse_Program(int courseId);
        int AddCourse_Program(int courseId, List<OrderedThemeDto> courseThemes);
    }
}