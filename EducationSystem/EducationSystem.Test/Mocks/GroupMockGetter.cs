using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class GroupMockGetter
    {
        public static GroupDto GetGroupDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new GroupDto
                    {
                        GroupStatus = GroupStatus.InProgress,
                        StartDate = DateTime.Now                    
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

