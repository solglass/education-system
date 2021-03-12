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
                IsDeleted = CheckOddNumber(caseId)
            };
        }

        public static bool CheckOddNumber(int caseId)
        {
            if (caseId % 2 == 0)
            {
                return false;
            }
            return true;
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