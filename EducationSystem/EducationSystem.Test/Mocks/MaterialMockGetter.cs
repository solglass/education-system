using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class MaterialMockGetter
    {
        public static MaterialDto GetMaterialDtoMock(int caseId)
        {
            return caseId switch
            {
                1 => new MaterialDto
                {
                    Description = "Description test",
                    Link = "Link test1",
                    IsDeleted = false,
                    Tags=new List<TagDto>()
                },

                2 => new MaterialDto
                {
                    Description = "Description test2",
                    Link = "Link test2",
                    IsDeleted = false,
                    Tags = new List<TagDto>()
                },

                3 => new MaterialDto
                {
                    Description = "Description test3",
                    Link = "Link test3",
                    IsDeleted = false,
                    Tags = new List<TagDto>()
                },

                _ => null,
            };
        }
    }
}