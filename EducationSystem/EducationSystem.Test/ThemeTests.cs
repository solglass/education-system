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

        //[Test, Order(2)]
        //public void TestUpdateTheme()
        //{
        //    _themeFromDb = _courseRepo.GetThemes();
        //    ThemeDto theme;
        //    foreach (int themeId in _themeIdList)
        //    {
        //        theme = _courseRepo.GetThemeById(themeId);
        //        theme.Name = $"New Name {themeId}";
        //        _courseRepo.UpdateTheme(theme);
        //        if (!theme.Equals(_courseRepo.GetThemeById(themeId)))
        //        {
        //            Assert.Fail($"Theme {themeId} was changed incorrectly or not changed");
        //        }
        //    }
        //    Assert.Pass();
        //}
        //[Test, Order(3)]
        //public void TestDeleteTheme()
        //{
        //    _themeFromDb = _courseRepo.GetThemes();
        //    int deletedId;
        //    foreach (int themeId in _themeIdList)
        //    {
        //        deletedId = _courseRepo.DeleteTheme(themeId);

        //        List<ThemeDto> newThemeFromDb = _courseRepo.GetThemes();

        //        if (_themeFromDb.Count == newThemeFromDb.Count)
        //        {

        //            Assert.Fail("Nothing was deleted");
        //        }
        //        else
        //        {
        //            _themeFromDb = newThemeFromDb;
        //        }
        //        if (_courseRepo.GetThemeById(themeId) != null)
        //        {
        //            Assert.Fail("Something wrong was deleted");
        //        }
        //    }
        //    Assert.Pass();

        //}
        //public ThemeDto GetThemeMock(int n)
        //{
        //    ThemeDto result = new ThemeDto();
        //    switch (n)
        //    {
        //        case 1:
        //            return result;
        //        case 2:
        //            result = (new ThemeDto { Name = "Test theme 1" });
        //            return result;
        //        case 3:
        //            result = new ThemeDto { Name = "Test theme 2" };
        //            return result;
        //        case 4:
        //            result = new ThemeDto { Name = "Test theme 3" };
        //            return result;
        //        default:
        //            return result;
        //    }
        //}
    }
}
