using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Globalization;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class GroupMockGetter
    {
        public static GroupDto GetGroupDtoMock(int id)
        {
            return id switch
            {
              1 => new GroupDto
              {
                GroupStatus = GroupStatus.InProgress,
                StartDate = DateTime.ParseExact("06.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
              },
              2 => new GroupDto
              {
                GroupStatus = GroupStatus.Finished,
                StartDate = DateTime.ParseExact("06.07.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
              },
              3 => new GroupDto
              {
                GroupStatus = GroupStatus.ReadyToStart,
                StartDate = DateTime.ParseExact("06.08.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
              },
              4 => new GroupDto
              {
                GroupStatus = GroupStatus.InProgress,
                StartDate = DateTime.ParseExact("06.09.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
              },
                5 => new GroupDto
                {
                    GroupStatus = GroupStatus.InProgress,
                    StartDate = DateTime.ParseExact("06.10.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                },
                _ => null,
            };
        }
    }
}

