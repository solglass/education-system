using EducationSystem.API.Models;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    internal class AttachmentTypeMapper
    {
        public AttachmentTypeDto ToDto(AttachmentTypeInputModel inputModel)
        {
            var attachmentTypeDto = new AttachmentTypeDto();
            attachmentTypeDto.Id = inputModel.Id;
            if (string.IsNullOrEmpty(inputModel.Name) == false)
            { attachmentTypeDto.Name = inputModel.Name; }
            return attachmentTypeDto;
        }
    }
}