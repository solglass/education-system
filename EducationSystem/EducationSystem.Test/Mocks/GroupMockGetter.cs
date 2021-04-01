using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Globalization;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class GroupMockGetter
    {
        public static GroupDto GetGroupDtoMock(int id)
        {
            return id switch
            {
              1 => new GroupDto
              {
                GroupStatus = GroupStatus.InProgress,
                StartDate =  new DateTime(2000, 5, 6),
              },
              2 => new GroupDto
              {
                GroupStatus = GroupStatus.Finished,
                StartDate =  new DateTime(2000, 7, 6),
              },
              3 => new GroupDto
              {
                GroupStatus = GroupStatus.ReadyToStart,
                StartDate = new DateTime(2000, 8, 6),
              },
              4 => new GroupDto
              {
                GroupStatus = GroupStatus.InProgress,
                StartDate = new DateTime(2000, 9, 6),
              },
                5 => new GroupDto
                {
                    GroupStatus = GroupStatus.InProgress,
                    StartDate = new DateTime(2000, 10, 6),
                },
                _ => null,
            };
        }
    }
}

