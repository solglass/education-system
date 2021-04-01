using EducationSystem.Data.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class MaterialMockGetter
    {
        public static MaterialDto GetMaterialDtoMock(int caseId)
        {
            return caseId switch
            {
                0 => new MaterialDto(),
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
                4 => new MaterialDto
                {
                    Description = "Description test2",
                    IsDeleted = false,
                    Tags = new List<TagDto>()
                },

                5 => new MaterialDto
                {
                    Link = "Link test3",
                    IsDeleted = false,
                    Tags = new List<TagDto>()
                },
                _ => null,
            };
        }
    }
}