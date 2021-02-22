using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Core.Enums;
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
            var attachmentDto = new AttachmentDto();
            attachmentDto.Id = inputModel.Id;
            attachmentDto.Path = inputModel.Path;
            attachmentDto.AttachmentType = 
                (AttachmentType) inputModel.AttachmentTypeId ;
            return attachmentDto;
        }

        public List<AttachmentDto> ToDtos(List<AttachmentInputModel> inputModels)
        {
            List<AttachmentDto> result = new List<AttachmentDto>();

            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }

        public AttachmentOutputModel FromDto(AttachmentDto attachmentDto)
        {

            return new AttachmentOutputModel()
            {
                Id = attachmentDto.Id,
                Path = attachmentDto.Path,
                AttachmentType = attachmentDto.AttachmentType
            };
        }


        public List<AttachmentOutputModel> FromDtos(List<AttachmentDto> attachmentDtos)
        {
            List<AttachmentOutputModel> models = new List<AttachmentOutputModel>();

            foreach (var attachment in attachmentDtos)
            {
                models.Add(FromDto(attachment));
            }
            return models;
        }

    }
}
