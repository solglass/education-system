﻿using EducationSystem.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
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
                5 => new ThemeDto
                {
                    Name = "Test theme 5"
                },
                6 => new ThemeDto
                {
                    Name = "Test theme 6"
                },
                7=>new ThemeDto(),
                _ => null,
            };
        }
    }
}
