using Dapper;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Test
{
    public class CourseRepositoryTest : ITest
    {

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(89)]
        public void DeleteCourseTest(int id)
        {
            var courseDictionary = new Dictionary<int, CourseDto>();

            var expected = _connection
                 .Query<CourseDto, ThemeDto, CourseDto>(
                     "dbo.Course_SelectAll",
                     (course, theme) =>
                     {
                         if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                         {
                             courseEntry = course;
                             courseEntry.Themes = new List<ThemeDto>();
                             courseDictionary.Add(courseEntry.Id, courseEntry);
                         }

                         courseEntry.Themes.Add(theme);
                         return courseEntry;
                     },
                     splitOn: "Id",
                     commandType: System.Data.CommandType.StoredProcedure)
                 .Distinct()
                 .ToList();


            var deleted = expected.Where(x => x.Id == id).ToList();
            

            expected.RemoveAll(x => x.Id == id);



            var result = _connection
                .Execute("dbo.Course_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);

            var actual = _connection
                 .Query<CourseDto, ThemeDto, CourseDto>(
                     "dbo.Course_SelectAll",
                     (course, theme) =>
                     {
                         if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                         {
                             courseEntry = course;
                             courseEntry.Themes = new List<ThemeDto>();
                             courseDictionary.Add(courseEntry.Id, courseEntry);
                         }

                         courseEntry.Themes.Add(theme);
                         return courseEntry;
                     },
                     splitOn: "Id",
                     commandType: System.Data.CommandType.StoredProcedure)
                 .Distinct()
                 .ToList();


            for (int i = 0; i < deleted.Count; ++i)
            {
                result = _connection
                .QuerySingle<int>("dbo.Course_Add",
                new
                {
                    deleted[i].Name,
                    deleted[i].Description,
                    deleted[i].Duration
                },
                commandType: System.Data.CommandType.StoredProcedure);
            }

            Assert.AreEqual(expected,actual);
        }


    }
}