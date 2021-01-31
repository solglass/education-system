using EducationSystem.API.Models;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    public class TagMapper
    {
        public TagDto ToDto(TagInputModel inputModel) 
        {
            return new TagDto {
                Id = inputModel.Id,
                Name = inputModel.Name
            };
        }
    }
}