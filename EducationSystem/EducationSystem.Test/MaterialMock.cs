using EducationSystem.Data.Models;

namespace EducationSystem.Data.Tests
{
    public static class MaterialMock
    {
        public static MaterialDto GetMaterialMock(int caseId)
        {
            return new MaterialDto
            {
                Description = $"Description {caseId}",
                Link = $"Link {caseId}",
                IsDeleted = false
            };
        }
    }
}
