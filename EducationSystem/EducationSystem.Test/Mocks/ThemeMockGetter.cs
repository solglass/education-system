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
            return id switch
            {
                1 => new ThemeDto
                {
                    Name = "Test theme 1"
                },
                2 => new ThemeDto
                {
                    Name = "Test theme 2"
                },
                3 => new ThemeDto
                {
                    Name = "Test theme 3"
                },
                4 => new ThemeDto
                {
                    Name = "Test theme 4"
                },
                6 => new ThemeDto
                {
                    Name = "Test theme 6"
                },
                7 => new ThemeDto
                {
                    Name = "Test theme 7"
                },
                _ => null,
            };
        }
    }
}
