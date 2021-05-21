using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using EducationSystem.Data.Tests.Mocks;
using System;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class CourseTests:BaseTest
    {
        private ICourseRepository _courseRepo;
        private IMaterialRepository _materialRepo;

        private List<int> _courseIds;
        private List<int> _themeIds;
        private List<int> _materialIds;
        private List<(int, int)> _courseThemes;
        private List<(int, int)> _courseMaterials;

        [SetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository(_options);
            _materialRepo = new MaterialRepository(_options);
            _themeIds = new List<int>();
            _materialIds = new List<int>();
            _courseThemes = new List<(int, int)>();
            _courseMaterials = new List<(int, int)>();
            _courseIds = new List<int>();
        }

        [TestCase(new int[] { 1, 2, 3 }, new int[] { })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1 }, new int[] { 1, 2 })]
        public void GetAllCoursesTest(int[] courseMockIds, int[] themeMockIds)
        {
            //Given
            var expected = _courseRepo.GetCourses();
            foreach (var courseId in courseMockIds)
            {
                var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(courseId).Clone();
                course.Id = _courseRepo.AddCourse(course);
                Assert.Greater(course.Id, 0);
                _courseIds.Add(course.Id);
                expected.Add(course);
            }
            //When
            var actual = _courseRepo.GetCourses();

            //Them
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestCase(1, new int[] { 1,2}, new int[] { 1, 2 })]
        [TestCase(2, new int[] { 2,1}, new int[] { 2, 1 })]
        public void GetCourseByIdPositiveTest(int mockId, int[] themeIds, int[] materialIds)
        {
            //Given
            var expected = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            expected.Id = _courseRepo.AddCourse(expected);
            Assert.Greater(expected.Id, 0);
            _courseIds.Add(expected.Id);

            foreach (var item in materialIds)
            {
                var material = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(item).Clone();
                material.Id = _materialRepo.AddMaterial(material);
                Assert.Greater(expected.Id, 0);
                _materialIds.Add(material.Id);

                var result = _courseRepo.AddCourse_Material(expected.Id, material.Id);
                Assert.Greater(result, 0);
                _courseMaterials.Add((expected.Id, material.Id));
                expected.Materials.Add(material);
            }

            //When
            var actual = _courseRepo.GetCourseById(expected.Id);

            //Then
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Materials, actual.Materials);
        }

        [Test]
        public void GetCourseByIdNegativeTestCourseNotExist()
        {
            //Given

            //When
            var course = _courseRepo.GetCourseById(-1);
            //Then
            Assert.IsNull(course);
        }

        [TestCase(1)]
        public void AddCoursePositiveTest(int mockId)
        {
            //Given
            var expected = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            expected.Id = _courseRepo.AddCourse(expected);
            Assert.Greater(expected.Id, 0);
            _courseIds.Add(expected.Id);

            //When
            var actual = _courseRepo.GetCourseById(expected.Id);

            //Then
            Assert.AreEqual(expected, actual);
        }
       
        [TestCase(6)]
        public void AddCourseNegativeTestEmptyProprties(int mockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();

            //When
            try
            {
                course.Id = _courseRepo.AddCourse(course);
                _courseIds.Add(course.Id);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddCourseNegativeTestNullEntity()
        {
            //Given
           
            //When
            try
            {
              var  courseId = _courseRepo.AddCourse(null);
                _courseIds.Add(courseId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(2)]
        public void UpdateCoursePositiveTest(int mockId)
        {
            //Given
            var expected = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            expected.Id = _courseRepo.AddCourse(expected);
            Assert.Greater(expected.Id, 0);
            _courseIds.Add(expected.Id);
            expected.Description = "Updated course description";
            expected.Name = "Updated course name";
            expected.Duration = 4;
            var courseToUpdate = (CourseDto)expected.Clone();
            courseToUpdate.Id = expected.Id;
            courseToUpdate.IsDeleted = !expected.IsDeleted;

            //When, Then
            var result = _courseRepo.UpdateCourse(courseToUpdate);
            Assert.AreEqual(1,result);

            var actual = _courseRepo.GetCourseById(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        public void UpdateCourseNegativeTestEntityNotExists(int mockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();

            //When
            var result = _courseRepo.UpdateCourse(course);

            //Then
            Assert.AreEqual(0, result);
        }

        [TestCase(1,6)]
        public void UpdateCourseNegativeTestEmptyProperties(int mockToAddId, int mockToUpdate)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockToAddId).Clone();
            var courseId = _courseRepo.AddCourse(course);
            _courseIds.Add(courseId);
            //When
            try
            {
                course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockToUpdate).Clone();
                course.Id = courseId;
                _courseRepo.UpdateCourse(course);
                
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void UpdateCourseNegativeTestNullEntity()
        {
            //Given
            
            //When
            try
            {
                _courseRepo.UpdateCourse(null);
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(3, true)]
        [TestCase(4, false)]
        public void DeleteOrRecoverCoursePositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var expected = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            expected.Id = _courseRepo.AddCourse(expected);
            Assert.Greater(expected.Id, 0);
            _courseIds.Add(expected.Id);
            expected.IsDeleted = isDeleted;
            var result = _courseRepo.DeleteOrRecoverCourse(expected.Id, isDeleted);
            Assert.AreEqual(1, result);

            //When
            var actual = _courseRepo.GetCourseById(expected.Id);

            //Then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(-1, true)]
        [TestCase(-1, false)]
        public void DeleteOrRecoverCourseNegativeTestEntityNotExist(int id, bool isDeleted)
        {
            //Given

            //When
            var result = _courseRepo.DeleteOrRecoverCourse(id, isDeleted);
            //Then
            Assert.AreEqual(0, result);
        }

       

        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(2, new int[] { 3, 2, 1 })]
        [TestCase(1, new int[] { })]
        public void AddCourseMaterialPositiveTest(int mockId, int[] materialMockIds)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            var expected = new List<MaterialDto>();
            foreach (var materialMockId in materialMockIds)
            {
                var material = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(materialMockId).Clone();
                material.Id = _materialRepo.AddMaterial(material);
                Assert.Greater(material.Id, 0);
                expected.Add(material);
                _materialIds.Add(material.Id);
                var result = _courseRepo.AddCourse_Material(course.Id, material.Id);
                Assert.Greater(result, 0);
                _courseMaterials.Add((course.Id, material.Id));
            }

            //When
            var actual = _courseRepo.GetCourseById(course.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual.Materials);
        }

        [TestCase(1)]
        public void AddCourseMaterialNegativeTestNotExistCourse(int materialMockId)
        {
            //Given
            var material = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(materialMockId).Clone();
            material.Id = _materialRepo.AddMaterial(material);
            Assert.Greater(material.Id, 0);
            _themeIds.Add(material.Id);

            //When
            try
            {
                var result = _courseRepo.AddCourse_Material(-1, material.Id);
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(1)]
        public void AddCourseMaterialNegativeTestNotExistMaterial(int courseMockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(courseMockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);

            //When
            try
            {
                var result = _courseRepo.AddCourse_Material(course.Id, -1);
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(1, 1)]
        public void AddCourseMaterialNegativeTestCreateNotUniqueEntity(int courseMockId, int materialMockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(courseMockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);

            var material = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(materialMockId).Clone();
            material.Id = _materialRepo.AddMaterial(material);
            Assert.Greater(material.Id, 0);
            _materialIds.Add(material.Id);

            var result = _courseRepo.AddCourse_Material(course.Id, material.Id);
            Assert.Greater(result, 0);
            _courseMaterials.Add((course.Id, material.Id));

            //When
            try
            {
                result = _courseRepo.AddCourse_Material(course.Id, material.Id);
            }
            //then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(1, new int[] { 2 })]
        [TestCase(1, new int[] { })]
        public void DeleteCourseMaterialPositiveTest(int mockId, int[] materialMockIds)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            var expected = new List<MaterialDto>();

            for (int i = 0; i < materialMockIds.Length; i++)
            {
                var material = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(materialMockIds[i]).Clone();
                material.Id = _materialRepo.AddMaterial(material);
                Assert.Greater(material.Id, 0);
                _materialIds.Add(material.Id);
                var result = _courseRepo.AddCourse_Material(course.Id, material.Id);
                Assert.Greater(result, 0);

                _courseMaterials.Add((course.Id, material.Id));


                if (i < materialMockIds.Length / 2)
                {
                    expected.Add(material);
                }
                else
                {
                    result = _courseRepo.DeleteCourse_Material(course.Id, material.Id);
                    Assert.AreEqual(1, result);
                }
            }
            //When
            var actual = _courseRepo.GetCourseById(course.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual.Materials);
        }

        [Test]
        public void DeleteCourseMaterialNegativeTestRelationNotExists()
        {
            //Given

            //When
            var result = _courseRepo.DeleteCourse_Material(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }
       

        

        [TearDown]
        public void TearDown()
        {
            DeleteCourseMaterials();
            DeleteMaterials();
            DeleteCourseThemes();
            DeleteThemes();
            DeleteCourses();
        }
        private void DeleteCourseMaterials()
        {
            foreach (var ids in _courseMaterials)
            {
                _courseRepo.DeleteCourse_Material(ids.Item1, ids.Item2);
            }
        }
        private void DeleteCourseThemes()
        {
            foreach (var courseId in _courseIds)
            {
                _courseRepo.DeleteCourse_Program(courseId);
            }
        }
        private void DeleteMaterials()
        {
            foreach (var id in _materialIds)
            {
                _materialRepo.HardDeleteMaterial(id);
            }
        }
        private void DeleteThemes()
        {
            foreach (var id in _themeIds)
            {
                _courseRepo.HardDeleteTheme(id);
            }
        }
        private void DeleteCourses()
        {
            foreach(var id in _courseIds)
            {
                _courseRepo.HardDeleteCourse(id);
            }
        }
    }
}