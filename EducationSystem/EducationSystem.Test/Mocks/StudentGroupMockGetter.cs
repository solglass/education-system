using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class StudentGroupMockGetter
    {
        public static StudentGroupDto GetStudentGroupDtoMock(int mockId)
        {
            StudentGroupDto studentGroupDto = mockId switch
            {
                1 => new StudentGroupDto()
                {
                    User = UserMockGetter.GetUserDtoMock(1),
                    ContractNumber = 99,
                    Group = GroupMockGetter.GetGroupDtoMock(1)
                },
                2 => new StudentGroupDto()
                {
                    User = UserMockGetter.GetUserDtoMock(1),
                    ContractNumber = 11,
                    Group = GroupMockGetter.GetGroupDtoMock(2)
                },
                _ => throw new NotImplementedException()
            };
            return studentGroupDto;


        }
    }
}
