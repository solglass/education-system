using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Test
{
    public class CourseRepositoryTest
    {


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(89)]
        public void DeleteCourseTest(int id)
        {
            CourseRepository courseRepository = new CourseRepository();


            var expected = courseRepository.GetCourses();


            var deleted = expected.Where(x => x.Id == id).ToList();
            

            expected.RemoveAll(x => x.Id == id);



            courseRepository.DeleteCourse(id);

            var actual = courseRepository.GetCourses();

            for (int i = 0; i < deleted.Count; ++i)
            {
              //  courseRepository.AddCourse(deleted[i].Name, deleted[i].Description, deleted[i].Duration);
            }

            Assert.AreEqual(expected,actual);
        }


    }
}