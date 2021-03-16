using EducationSystem.Data.Models;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class MaterialMock
    {
        public static MaterialDto GetMaterialMock(int caseId)
        {
            switch (caseId)
            {
                case 1:
                    return new MaterialDto
                    {
                        Description = "Decriprion test",
                        Link = "Link test",
                        IsDeleted = false
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}