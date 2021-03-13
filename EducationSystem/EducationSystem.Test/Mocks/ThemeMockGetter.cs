using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class ThemeMockGetter
    {
        public static ThemeDto GetThemeDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new ThemeDto
                    {
                        Name = "Test theme 1",
                        IsDeleted = false
                    };
                    break;
                case 2:
                    return new ThemeDto
                    {
                        Name = "Test theme 2",
                        IsDeleted = false
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}
