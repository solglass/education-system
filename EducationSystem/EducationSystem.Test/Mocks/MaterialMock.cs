using EducationSystem.Data.Models;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class MaterialMock
    {
        public static MaterialDto GetMaterialMock(int caseId)
        {
            return caseId switch
            {
                0 => new MaterialDto(),
                1 => new MaterialDto
                {
                    Description = "Decriprion test",
                    Link = "Link test",
                    IsDeleted = false
                },
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