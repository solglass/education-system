using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class ThemeTests
    {

        private ICourseRepository _courseRepo;
        private List<int> _themeIdList;
        private List<ThemeDto> _themeFromDb;
        [OneTimeSetUp]
        public void SetUpTest(ICourseRepository courseRepository)
        {

            _courseRepo = courseRepository;

            _themeFromDb = new List<ThemeDto>();

            _themeIdList = new List<int>();
        }

        [Test, Order(1)]
        public void TestAddTheme()
        {
            _themeFromDb = _courseRepo.GetThemes();
            ThemeDto theme; 
            for (int i = 2; i < 4; ++i)
            {
                theme = GetThemeMock(i);
                _themeFromDb.Add(theme);
                _themeIdList.Add(_courseRepo.AddTheme(theme));
            }

            List<ThemeDto> actualThemeRepo = _courseRepo.GetThemes();
            if(actualThemeRepo.Count != _themeFromDb.Count)
            {
                Assert.Fail();
            }
            for(int i = _themeIdList[0]; i < actualThemeRepo.Count; ++i)
            {
                if (!_themeFromDb[i].Equals(actualThemeRepo[i]))
                {
                    Assert.Fail();
                }
            }
            Assert.Pass();

        }

        [Test, Order(2)]
        public void TestUpdateTheme()
        {
            _themeFromDb = _courseRepo.GetThemes();
            ThemeDto theme;
            foreach (int themeId in _themeIdList)
            {
                theme = _courseRepo.GetThemeById(themeId);
                theme.Name = $"New Name {themeId}";
                _courseRepo.UpdateTheme(theme);
                if (!theme.Equals(_courseRepo.GetThemeById(themeId)))
                {
                    Assert.Fail($"Theme {themeId} was changed incorrectly or not changed");
                }
            }
            Assert.Pass();
        }
        [Test, Order(3)]
        public void TestDeleteTheme()
        {
            _themeFromDb = _courseRepo.GetThemes();
            int deletedId;
            foreach (int themeId in _themeIdList)
            {
                deletedId = _courseRepo.DeleteTheme(themeId);

                List<ThemeDto> newThemeFromDb = _courseRepo.GetThemes();

                if (_themeFromDb.Count == newThemeFromDb.Count)
                {

                    Assert.Fail("Nothing was deleted");
                }
                else
                {
                    _themeFromDb = newThemeFromDb;
                }
                if (_courseRepo.GetThemeById(themeId) != null)
                {
                    Assert.Fail("Something wrong was deleted");
                }
            }
            Assert.Pass();

        }
        public ThemeDto GetThemeMock(int n)
        {
            ThemeDto result = new ThemeDto();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result = (new ThemeDto { Name = "Test theme 1" });
                    return result;
                case 3:
                    result = new ThemeDto { Name = "Test theme 2" };
                    return result;
                case 4:
                    result = new ThemeDto { Name = "Test theme 3" };
                    return result;
                default:
                    return result;
            }
        }
    }
}
