using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class MaterialMock
    {
        public static MaterialDto GetMaterialMock(int caseId)
        {
            return caseId switch
            {
                1 => new MaterialDto
                {
                    Description = "Decriprion test",
                    Link = "Link test",
                    IsDeleted = false,
                    Tags=new List<TagDto>()
                },
                _ => null,
            };
                0 => new MaterialDto(),
                2 => new MaterialDto
                {
                    Link = "Link test",
                    IsDeleted = false
                },
                3 => new MaterialDto
                {
                    Description = "Decriprion test",
                    IsDeleted = false
                },
                _ => null,
            };
        }
    }
}