using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Diagnostics.CodeAnalysis;

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
                    StartDate = DateTime.Now
                },
                2 => new GroupDto
                {
                    GroupStatus = GroupStatus.InProgress,
                    StartDate = DateTime.Now.AddDays(+6)
                },
                3 => new GroupDto
                {
                    GroupStatus = GroupStatus.InProgress,
                    StartDate = DateTime.Now.AddDays(-6)
                },
                _ => null,
            };
        }
    }
}

