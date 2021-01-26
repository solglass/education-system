using EducationSystem.API.Models;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    public class GroupStatusMapper
    {
        public GroupStatusDto ToDto(GroupStatusInputModel inputModel) 
        {
            return new GroupStatusDto {
                Id = inputModel.Id,
                Name = inputModel.Name
            };
        }
    }
}