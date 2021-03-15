using EducationSystem.Data.Models;

namespace EducationSystem.Data.Tests
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

/* for caseId 1-5
 * Description, Link, IsDeleted
 * Description1, Link1, 1
 * Description2, Link2, 0
 * Description3, Link3, 1
 * Description4, Link4, 0
 * Description5, Link5, 1
 * 
 */