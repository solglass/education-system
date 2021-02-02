using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
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

        public List<AttachmentDto> ToDtos(List<AttachmentInputModel> inputModels)
        {
            List<AttachmentDto> result = new List<AttachmentDto>();
            if (inputModels == null || inputModels.Count == 0)
            {
                throw new Exception("Ошибка! Вложения не найдены!");
            }
            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }

        public AttachmentOutputModel FromDto(AttachmentDto attachmentDto)
        {
            var attachmentTypeMapper = new AttachmentTypeMapper();
            if (attachmentDto == null)
            {
                throw new Exception("Ошибка! Вложение не найдено!");
            }
            return new AttachmentOutputModel()
            {
                Id = attachmentDto.Id,
                Path = attachmentDto.Path,
                AttachmentType = attachmentTypeMapper.FromDto(attachmentDto.AttachmentType)
            };
        }


        public List<AttachmentOutputModel> FromDtos(List<AttachmentDto> attachmentDtos)
        {
            List<AttachmentOutputModel> models = new List<AttachmentOutputModel>();
            if (attachmentDtos == null || attachmentDtos.Count == 0)
            {
                throw new Exception("Ошибка! Вложения не найдены!");
            }
            foreach (var attachment in attachmentDtos)
            {
                models.Add(FromDto(attachment));
            }
            return models;
        }

    }
}
