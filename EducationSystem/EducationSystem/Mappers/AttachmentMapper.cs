using EducationSystem.API.Models;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class AttachmentMapper
    {
        public AttachmentDto ToDto(AttachmentInputModel inputModel)
        {
            var attachmentTypeMapper = new AttachmentTypeMapper();
            var attachmentDto = new AttachmentDto();
            attachmentDto.Id = inputModel.Id;
            attachmentDto.Path = inputModel.Path;
            attachmentDto.AttachmentType = attachmentTypeMapper.ToDto(inputModel.AttachmentType);
            return attachmentDto;
        }
    }
}
