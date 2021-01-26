using Dapper;
using EducationSystem.Data.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class CourseRepository : BaseRepository
    {

        public CourseRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<CourseDto> GetCourses()
        {
            var courseDictionary = new Dictionary<int, CourseDto>();
            var groupDictionary = new Dictionary<int, GroupDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var courses = _connection
                .Query<CourseDto, ThemeDto, GroupDto, GroupStatusDto, CourseDto>(
                    "dbo.Course_SelectAll",
                    (course, theme, group, groupStatus) =>
                    {
                        if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                            courseEntry.Groups = new List<GroupDto>();
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                        }
                        if(group!=null && groupStatus!=null&& !groupDictionary.TryGetValue(group.Id, out GroupDto groupEntry))
                        {
                            groupEntry = group;
                            groupEntry.GroupStatus = groupStatus;
                            courseEntry.Groups.Add(groupEntry);
                            groupDictionary.Add(groupEntry.Id, groupEntry);
                        }
                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            courseEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return courseEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return courses;
        }

        public CourseDto GetCourseById(int id)
        {
            var groupDictionary = new Dictionary<int, GroupDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var courseEntry = new CourseDto();
            var course = _connection
                .Query<CourseDto, ThemeDto, GroupDto, GroupStatusDto, CourseDto>(
                    "dbo.Course_SelectById",
                    (course, theme, group, groupStatus) =>
                    {
                        if (courseEntry.Id == 0)
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                            courseEntry.Groups = new List<GroupDto>();
                        }
                        if(theme!=null && !themeDictionary.TryGetValue(theme.Id, out  ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            courseEntry.Themes.Add(theme);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        
                        if (group != null && groupStatus != null && !groupDictionary.TryGetValue(group.Id, out GroupDto groupEntry))
                        {
                            groupEntry = group;
                            group.GroupStatus = groupStatus;
                            courseEntry.Groups.Add(group);
                            groupDictionary.Add(groupEntry.Id, groupEntry);
                        }
                        return courseEntry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return course;
        }

        // should return id of inserted entity, use 'QuerySingle' method
        public int AddCourse(/*string name, string description, int duration*/CourseDto course)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Add",
                new
                {
                    name = course.Name,
                    description = course.Description,
                    duration = course.Duration
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int UpdateCourse(/*int id, string name, string description, int duration, bool isDeleted*/ CourseDto course)
        {
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {
                    id = course.Id,
                    name = course.Name,
                    description = course.Description,
                    duration = course.Duration,
                    isDeleted = course.IsDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int DeleteCourse(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }



        public List<ThemeDto> GetThemes()
        {
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var lessonDictionary = new Dictionary<int, LessonDto>();
            var courseDictionary = new Dictionary<int, CourseDto>();
            var themes = _connection
                .Query< ThemeDto, CourseDto ,HomeworkDto,TagDto, LessonDto, ThemeDto>(
                    "dbo.Theme_SelectAll",
                    (theme, course, homework,tag,lesson) =>
                    {
                        if (!themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            themeEntry.Courses = new List<CourseDto>();
                            themeEntry.Homeworks = new List<HomeworkDto>();
                            themeEntry.Tags = new List<TagDto>();
                            themeEntry.Lessons = new List<LessonDto>();
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        if (course != null && !courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            themeEntry.Courses.Add(course);
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                        }
                        if (homework != null  && !homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            themeEntry.Homeworks.Add(homework);
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                        }
                        if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                        {
                            tagEntry = tag;
                            themeEntry.Tags.Add(tag);
                            tagDictionary.Add(tagEntry.Id, tagEntry);
                        }
                        if (lesson != null && !lessonDictionary.TryGetValue(lesson.ID, out LessonDto lessonEntry))
                        {
                            lessonEntry = lesson;
                            themeEntry.Lessons.Add(lesson);
                            lessonDictionary.Add(lessonEntry.ID, lessonEntry);
                        }
                       
                        return themeEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return themes;
        }
        public ThemeDto GetThemeById(int id)
        {
            var themeEntry = new ThemeDto();
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var lessonDictionary = new Dictionary<int, LessonDto>();
            var courseDictionary = new Dictionary<int, CourseDto>();
            var theme = _connection
                .Query<ThemeDto, CourseDto, HomeworkDto, TagDto, LessonDto, ThemeDto>(
                    "dbo.Theme_SelectById",
                    (theme, course, homework, tag, lesson) =>
                    {
                        if (themeEntry.Id == 0)
                        {
                            themeEntry = theme;
                            themeEntry.Courses = new List<CourseDto>();
                            themeEntry.Homeworks = new List<HomeworkDto>();
                            themeEntry.Tags = new List<TagDto>();
                            themeEntry.Lessons = new List<LessonDto>();
                        }
                        if (course != null&& !courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            themeEntry.Courses.Add(course);
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                        }
                        if (homework != null && !homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            themeEntry.Homeworks.Add(homework);
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                        }
                        if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                        {
                            tagEntry = tag;
                            themeEntry.Tags.Add(tag);
                            tagDictionary.Add(tagEntry.Id, tagEntry);
                        }
                        if (lesson != null && !lessonDictionary.TryGetValue(lesson.ID, out LessonDto lessonEntry))
                        {
                            lessonEntry = lesson;
                            themeEntry.Lessons.Add(lesson);
                            lessonDictionary.Add(lessonEntry.ID, lessonEntry);
                        }
                        //if (course != null && !courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        //{
                        //    courseEntry = course;
                        //    themeEntry.Courses.Add(course);
                        //    courseDictionary.Add(courseEntry.Id, courseEntry);
                        //}
                        return themeEntry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return theme;
        }
        public int AddTheme(string name)
        {
            int result = _connection
                .QuerySingle<int>("dbo.Theme_Add",
                new
                {
                    name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int UpdateTheme(int id, string name)
        {
            var result = _connection
                .Execute("dbo.Theme_Update",
                new
                {
                    id,
                    name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteTheme(int id)
        {
            var result = _connection
                .Execute("dbo.Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }






        public int AddCourse_Theme(int courseId, int themeId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Theme_Add",
                new
                {
                    courseId,
                    themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteCourse_Theme(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public int AddCourse_Theme_Material(int courseThemeID, int materialID)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Material_Add",
                new
                {
                    courseThemeID,
                    materialID
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteCourse_Theme_Material(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Material_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
