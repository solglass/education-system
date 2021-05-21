using EducationSystem.Data.Models;
using System;
using System.Globalization;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class GroupReportMockGetter
    {
        public static GroupReportDto GetGroupReportDtoMock(int id)
        {
            return id switch
            {
                1 => new GroupReportDto
                {
                    EndDate = DateTime.ParseExact("07.01.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StartDate = DateTime.ParseExact("06.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                2 => new GroupReportDto
                {
                    EndDate = DateTime.ParseExact("07.02.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StartDate = DateTime.ParseExact("06.07.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                3 => new GroupReportDto
                {
                    EndDate = DateTime.ParseExact("07.03.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StartDate = DateTime.ParseExact("06.08.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                4 => new GroupReportDto
                {
                    EndDate = DateTime.ParseExact("07.04.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StartDate = DateTime.ParseExact("06.09.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                5 => new GroupReportDto
                {
                    EndDate = DateTime.ParseExact("07.05.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StartDate = DateTime.ParseExact("06.10.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                _ => null,
            };
        }
    }
}
