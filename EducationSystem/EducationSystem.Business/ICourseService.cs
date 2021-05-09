using EducationSystem.Business.Model;
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ICourseService
    {
        int AddCourse(CourseDto course);
        int AddTagToTheme(int themeId, int tagId);
        int RemoveTagFromTheme(int themeId, int tagId);
        int AddTheme(ThemeDto theme);
        int AddMaterialToCourse(int courseId, int materialId);
        int DeleteTheme(int id);
        int RecoverTheme(int id);
        CourseDto GetCourseById(int id);
        CourseWithProgram GetCourseWithProgramById(int id);
        List<CourseDto> GetCourses();
        ThemeDto GetThemeById(int id);
        List<ThemeDto> GetThemes();
        List<ThemeDto> GetUncoveredThemesByGroupId(int id);
        int DeleteMaterialFromCourse(int courseId, int materialId);
        int UpdateCourse(CourseDto course);
        int DeleteCourse(int id);
        int RecoverCourse(int id);
        int AddUpdateCourseProgram(int courseId, List<CourseThemeDto> program);
    }
}