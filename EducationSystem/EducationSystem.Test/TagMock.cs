using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public static class TagMock
    {
        public static TagDto GetTagMock(int caseId)
        {
            return new TagDto
            {
                Name = $"Tag name {caseId}"
            };
        }
    }
}
