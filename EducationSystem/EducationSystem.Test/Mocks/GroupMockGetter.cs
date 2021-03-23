using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;

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
                StartDate = DateTime.Today
              },
              2 => new GroupDto
              {
                GroupStatus = GroupStatus.Finished,
                StartDate = DateTime.Today
              },
              3 => new GroupDto
              {
                GroupStatus = GroupStatus.ReadyToStart,
                StartDate = DateTime.Today
              },
              4 => new GroupDto
              {
                GroupStatus = GroupStatus.InProgress,
                StartDate = DateTime.Today
              },
              _ => null,
            };
        }
    }
}

