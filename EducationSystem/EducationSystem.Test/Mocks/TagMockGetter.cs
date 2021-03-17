using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class TagMockGetter
    {
        public static TagDto GetTagDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new TagDto
                    {
                        Name = "Test Tag",
                        Id = 1
                    }; break;
                case 2:
                    return new TagDto
                    {
                        Name = "Test Tag2"
                    }; break;
                case 3:
                    return new TagDto
                    {
                        Name = "Test Tag3"
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}
