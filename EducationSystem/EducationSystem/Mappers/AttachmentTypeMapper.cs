using EducationSystem.API.Models;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    internal class AttachmentTypeMapper
    {
        public AttachmentTypeDto ToDto(AttachmentTypeInputModel inputModel)
        {
            var attachmentTypeMapper = new AttachmentTypeMapper();
            return new AttachmentTypeDto
            {
                Name = inputModel.Name
            };
        }
    }
}