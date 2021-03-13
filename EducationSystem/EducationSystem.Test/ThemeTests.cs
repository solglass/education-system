using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class ThemeTests : BaseTest
    {

        private ICourseRepository _courseRepo;
        private List<int> _themeIdList;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository(_options);

            _themeIdList = new List<int>();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ThemeAddPositiveTest(int mockId)
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

        [TestCase(1,2)]
        public void ThemeUpdatePositiveTest(int mockId, int updateMockId)
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

        [OneTimeTearDown]
        public void SampleTestTearDown()
        {
            DeleteThemes();
        }

        private void DeleteThemes()
        {
            foreach(var themeId in _themeIdList)
            {
                _courseRepo.HardDeleteTheme(themeId);
            }
        }

    }
}
