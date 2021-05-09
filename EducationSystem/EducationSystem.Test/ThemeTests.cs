using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class ThemeTests : BaseTest
    {
        private ICourseRepository _courseRepo;
        private IGroupRepository _groupRepo;
        private ILessonRepository _lessonRepo;
        private ITagRepository _tagRepo;
        private List<int> _themeIdList;
        private List<int> _courseIdList;
        private List<int> _groupIdList;
        private List<int> _lessonIdList;
        private List<int> _tagIdList;
        private List<(int, int)> _courseThemeList;
        private List<(int, int)> _lessonThemeList;
        private List<(int, int)> _tagThemeList;

        [SetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository(_options);
            _groupRepo = new GroupRepository(_options);
            _lessonRepo = new LessonRepository(_options);
            _tagRepo = new TagRepository(_options);

            _themeIdList = new List<int>();
            _courseIdList = new List<int>();
            _groupIdList = new List<int>();
            _lessonIdList = new List<int>();
            _tagIdList = new List<int>();
            _courseThemeList = new List<(int, int)>();
            _lessonThemeList = new List<(int, int)>();
            _tagThemeList = new List<(int, int)>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddThemePositiveTest(int mockId)
        {
            // Given
            var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();
            var addedThemeId = _courseRepo.AddTheme(dto);
            Assert.Greater(addedThemeId, 0);

            _themeIdList.Add(addedThemeId);
            dto.Id = addedThemeId;

            // When
            var actual = _courseRepo.GetThemeById(addedThemeId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void AddThemeNegativeTestNotUniqueEntity(int mockId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();
            theme.Id = _courseRepo.AddTheme(theme);
            _themeIdList.Add(theme.Id);
            //When
            try
            {
                var result = _courseRepo.AddTheme(theme);
                _themeIdList.Add(result);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(7)]
        public void AddThemeNegativeTestEmptyEntity(int nockId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(nockId).Clone();

            //When
            try
            {
                theme.Id = _courseRepo.AddTheme(theme);
                _themeIdList.Add(theme.Id);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddThemeNegativeTestNullEntity()
        {
            //Given

            //When
            try
            {
                var themeId = _courseRepo.AddTheme(null);
                _themeIdList.Add(themeId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
       
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        public void UpdateThemePositiveTest(int mockId, int updateMockId)
        {
            // Given
            var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();
            var addedThemeId = _courseRepo.AddTheme(dto);
            _themeIdList.Add(addedThemeId);

            dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(updateMockId).Clone();
            dto.Id = addedThemeId;
            var affectedRowsCount = _courseRepo.UpdateTheme(dto);


            // When
            var actual = _courseRepo.GetThemeById(addedThemeId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void UpdateThemeNegativeTestEntityNotExists(int mockId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();

            //When
            var result = _courseRepo.UpdateTheme(theme);

            //Then
            Assert.AreEqual(0, result);
        }

        [TestCase(1,7)]
        public void UpdateThemeNegativeTestEmptyProperties(int mockToAddId, int mockToUpdateId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockToAddId).Clone();
            var themeId = _courseRepo.AddTheme(theme);
            _themeIdList.Add(themeId);
            //When
            try
            {
                theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockToUpdateId).Clone();
                theme.Id = themeId;
                _courseRepo.UpdateTheme(theme);
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(1, 2)]
        public void UpdateThemeNegativeTestNotUniqueEntity(int mockToCompareId, int mockToUpdateId)
        {
            //Given
            var themeToCompare = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockToCompareId).Clone();
            themeToCompare.Id = _courseRepo.AddTheme(themeToCompare);
            _themeIdList.Add(themeToCompare.Id);

            var themeToUpdate = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockToUpdateId).Clone();
           var themeToUpdateId = _courseRepo.AddTheme(themeToUpdate);
            _themeIdList.Add(themeToUpdateId);

            themeToUpdate = (ThemeDto)themeToCompare.Clone();
            themeToUpdate.Id = themeToUpdateId;
            //When
            try
            {
                var result = _courseRepo.UpdateTheme(themeToUpdate);
            }
            //then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void UpdateThemeNegativeTestNullEntity()
        {
            //Given

            //Then
            try
            {
                _courseRepo.UpdateTheme(null);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void GetThemesPositiveTest(int[] mockIds)
        {
            // Given
            var expected = _courseRepo.GetThemes();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(dto);
                _themeIdList.Add(addedThemeId);
                dto.Id = addedThemeId;
                expected.Add(dto);
            }

            // When
            var actual = _courseRepo.GetThemes();

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void DeleteOrRecoverThemePositiveTest(int mockId, bool isDeleted)
        {

            //Given
            var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();
            var addedThemeId = _courseRepo.AddTheme(dto);
            _themeIdList.Add(addedThemeId);
            dto.Id = addedThemeId;
            dto.IsDeleted = isDeleted;

            // When
            var affectedRowsCount = _courseRepo.DeleteOrRecoverTheme(addedThemeId, isDeleted);

            var actual = _courseRepo.GetThemeById(addedThemeId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(-1, true)]
        [TestCase(-1, false)]
        public void DeleteOrRecoverThemeNegativeTestEntityNotExists(int id, bool isDeleted)
        {
            //Given

            //When
            var result = _courseRepo.DeleteOrRecoverTheme(id, isDeleted);
            //Then
            Assert.AreEqual(0, result);
        }



        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(1, new int[] { 3 })]
        [TestCase(1, new int[] { })]
        public void AddCourseProgramPositiveTest(int courseMockId, int[] themeMockIds)
        {
            //Given 
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(courseMockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            _courseIdList.Add(course.Id);
            var themes = new List<ThemeDto>();
            var courseThemes = new List<CourseThemeDto>();

            for (var i = 0; i < themeMockIds.Length; i++)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockIds[i]).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                themes.Add(theme);
                _themeIdList.Add(theme.Id);
                courseThemes.Add(new CourseThemeDto
                {
                    Course = course,
                    Theme = theme,
                    Order = i + 1
                });
                _courseThemeList.Add((course.Id, theme.Id));
            }
            //When
            var result = _courseRepo.AddCourse_Program(courseThemes);
            //Then
            Assert.AreEqual(courseThemes.Count, result);
            var actual = _courseRepo.GetCourse_Program(course.Id);
            Assert.AreEqual(courseThemes, actual);
        }



        [TestCase(1, 2, new int[] { 1, 2, 3, 4 })]
        [TestCase(1, 2, new int[] { 1, 3 })]
        [TestCase(1, 2, new int[] { })]
        public void GetCourseProgramPositiveTest(int firstCourseMockId, int secondCourseMockId, int[] themeMockIds)
        {
            //Given 
            var firstCourse = (CourseDto)CourseMockGetter.GetCourseDtoMock(firstCourseMockId).Clone();
            firstCourse.Id = _courseRepo.AddCourse(firstCourse);
            _courseIdList.Add(firstCourse.Id);

            var secondCourse = (CourseDto)CourseMockGetter.GetCourseDtoMock(secondCourseMockId).Clone();
            secondCourse.Id = _courseRepo.AddCourse(secondCourse);
            _courseIdList.Add(secondCourse.Id);

            var themes = new List<ThemeDto>();
            var expected = new List<CourseThemeDto>();
            var courseThemesToAvoid = new List<CourseThemeDto>();
            for (var i = 0; i < themeMockIds.Length; i++)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockIds[i]).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                themes.Add(theme);
                _themeIdList.Add(theme.Id);
                if (i % 2 == 0)
                {
                    expected.Add(new CourseThemeDto
                    {
                        Course = firstCourse,
                        Theme = theme,
                        Order = i + 1
                    });
                    _courseThemeList.Add((firstCourse.Id, theme.Id));
                }
                else
                {
                    courseThemesToAvoid.Add(new CourseThemeDto
                    {
                        Course = secondCourse,
                        Theme = theme,
                        Order = i + 1
                    });
                    _courseThemeList.Add((secondCourse.Id, theme.Id));
                }
            }
            //When
            var result = _courseRepo.AddCourse_Program(expected);
            Assert.AreEqual(expected.Count, result);
            result = _courseRepo.AddCourse_Program(courseThemesToAvoid);
            Assert.AreEqual(courseThemesToAvoid.Count, result);

            //Then
            var actual = _courseRepo.GetCourse_Program(firstCourse.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCourseProgramNegativeTestCourseNotExists()
        {
            //Given

            //When
            var actual = _courseRepo.GetCourse_Program(-1);

            //Then
            Assert.IsEmpty(actual);
        }



        [TestCase(new int[] { 1, 2, 3 }, new int[] { 4,5,6})]
        [TestCase(new int[] { 4, 5, 6 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 4, 5, 6 }, new int[] {  })]
        public void GetUncoveredThemesByGroupIdPositiveTest(int[] coveredMockIds, int[] uncoveredMockIds)
        {
            // Given
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = courseDto;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);
            groupDto.Id = addedGroupId;

            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Date = DateTime.Now.AddDays(10);
            lessonDto.Group = groupDto;
            var addedLessonId = _lessonRepo.AddLesson(lessonDto);
            _lessonIdList.Add(addedLessonId);

            var expected = AddThemeMocksToCourseAndLesson(uncoveredMockIds, addedCourseId, addedLessonId, coveredMockIds.Length);
           
            lessonDto.Date = DateTime.Now.AddDays(-10);
            addedLessonId = _lessonRepo.AddLesson(lessonDto);
            _lessonIdList.Add(addedLessonId);

            AddThemeMocksToCourseAndLesson(coveredMockIds, addedCourseId, addedLessonId,0);

            // When
            var actual = _courseRepo.GetUncoveredThemesByGroupId(addedGroupId);

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUncoveredThemesByGroupIdNegativeTestGroupNotExists()
        {
            //Given

            //When
            var actual = _courseRepo.GetUncoveredThemesByGroupId(-1);

            //Then
            Assert.IsEmpty(actual);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void AddThemeTagPositiveTest(int[] mockIds)
        {
            // Given
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);
            themeDto.Id = addedThemeId;

            // When
            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedTagId = _tagRepo.TagAdd(tagDto);
                _tagIdList.Add(addedTagId);
                tagDto.Id = addedTagId;
                expected.Add(tagDto);

                _tagRepo.ThemeTagAdd(addedThemeId, addedTagId);
                _tagThemeList.Add((addedThemeId, addedTagId));
            }

            var actual = _courseRepo.GetThemeById(addedThemeId).Tags;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        public void AddThemeTagNegativeTestThemeNotExists(int tagMockId)
        {
            //Given
            var tag = (TagDto)TagMockGetter.GetTagDtoMock(tagMockId).Clone();
            tag.Id = _tagRepo.TagAdd(tag);
            _tagIdList.Add(tag.Id);

            //When
            try
            {
                var result = _tagRepo.ThemeTagAdd(-1, tag.Id);
                _tagThemeList.Add((-1, tag.Id));
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddThemeTagNegativeTestTagNotExists(int themeMockId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
            theme.Id = _courseRepo.AddTheme(theme);
            _themeIdList.Add(theme.Id);

            //When
            try
            {
                var result = _tagRepo.ThemeTagAdd(theme.Id, -1);
                _tagThemeList.Add(( theme.Id,-1));
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1, 1)]
        public void AddThemeTagNegativeTestNotUniqueEntity(int themeMockId, int tagMockId)
        {
            //Given
            var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
            theme.Id = _courseRepo.AddTheme(theme);
            _themeIdList.Add(theme.Id);

            var tag = (TagDto)TagMockGetter.GetTagDtoMock(tagMockId).Clone();
            tag.Id = _tagRepo.TagAdd(tag);
            _tagIdList.Add(tag.Id);

            var result = _tagRepo.ThemeTagAdd(theme.Id, tag.Id);
            _tagThemeList.Add((theme.Id, tag.Id));
            //When
            try
            {
                result = _tagRepo.ThemeTagAdd(theme.Id, tag.Id);
                _tagThemeList.Add((theme.Id, tag.Id));
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void DeleteThemeTagPositiveTest(int[] mockIds)
        {
            // Given
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);
            themeDto.Id = addedThemeId;

            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedTagId = _tagRepo.TagAdd(tagDto);
                _tagIdList.Add(addedTagId);
                tagDto.Id = addedTagId;

                _tagRepo.ThemeTagAdd(addedThemeId, addedTagId);
                _tagThemeList.Add((addedThemeId, addedTagId));

                if (i<mockIds.Length/2)
                {
                    expected.Add(tagDto);
                }
                else
                {
                    var result = _tagRepo.ThemeTagDelete(themeDto.Id, tagDto.Id);
                    Assert.AreEqual(1, result);
                }
            }
            
            //When
            var actual = _courseRepo.GetThemeById(addedThemeId).Tags;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void DeleteThemeTagNegativeTestEntityNotExists()
        {
            //Given

            //When
            var result = _tagRepo.ThemeTagDelete(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }

        [TearDown]
        public void SampleTestTearDown()
        {
            DeleteCourseThemes();
            DeleteLessonThemes();
            DeleteTagThemes();
            DeleteTags();
            DeleteLessons();
            DeleteGroups();
            DeleteCourses();
            DeleteThemes();
        }

        private void DeleteTagThemes()
        {
            foreach (var tagTheme in _tagThemeList)
            {
                _tagRepo.ThemeTagDelete(tagTheme.Item1, tagTheme.Item2);
            }
        }

        private void DeleteTags()
        {
            foreach (var tagId in _tagIdList)
            {
                _tagRepo.TagDelete(tagId);
            }
        }

        private void DeleteLessons()
        {
            foreach (var lessonId in _lessonIdList)
            {
                _lessonRepo.HardDeleteLesson(lessonId);
            }
        }

        private void DeleteLessonThemes()
        {
            foreach (var lessonTheme in _lessonThemeList)
            {
                _lessonRepo.DeleteLessonTheme(lessonTheme.Item1, lessonTheme.Item2);
            }
        }

        private void DeleteGroups()
        {
            foreach (var groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }

        private void DeleteCourseThemes()
        {
            //foreach (var courseTheme in _courseThemeList)
            //{
            //    _courseRepo.DeleteCourse_Theme(courseTheme.Item1, courseTheme.Item2);
            //}
            foreach (var courseId in _courseIdList)
            {
                _courseRepo.DeleteCourse_Program(courseId);
            }
        }

        private void DeleteCourses()
        {
            foreach (var courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }

        private void DeleteThemes()
        {
            foreach(var themeId in _themeIdList)
            {
                _courseRepo.HardDeleteTheme(themeId);
            }
        }

        private List<ThemeDto> AddThemeMocksToCourseAndLesson(int[] mockIds, int courseId, int lessonId, int order)
        {

            var dtos = new List<ThemeDto>();
            var program = new List<CourseThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(dto);
                _themeIdList.Add(addedThemeId);
                dto.Id = addedThemeId;
                dtos.Add(dto);

                program.Add(new CourseThemeDto
                {
                    Course=new CourseDto { Id=courseId},
                    Theme=dto,
                    Order=i+order
                });
                _courseThemeList.Add((courseId, addedThemeId));

                _lessonRepo.AddLessonTheme(lessonId, addedThemeId);
                _lessonThemeList.Add((lessonId, addedThemeId));
            }
            _courseRepo.AddCourse_Program(program);
            return dtos;
        }
    }
}
