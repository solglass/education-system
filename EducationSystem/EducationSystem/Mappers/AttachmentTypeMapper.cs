using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace EducationSystem.API.Mappers
{
    public class AttachmentTypeMapper
    {
        public AttachmentTypeDto ToDto(AttachmentTypeInputModel inputModel)
        {
            var attachmentTypeDto = new AttachmentTypeDto();
            attachmentTypeDto.Id = inputModel.Id;
            if (string.IsNullOrEmpty(inputModel.Name) == false)
            { attachmentTypeDto.Name = inputModel.Name; }
            return attachmentTypeDto;
        }

        public List<AttachmentTypeDto> ToDtos(List<AttachmentTypeInputModel> inputModels)
        {
            List<AttachmentTypeDto> result = new List<AttachmentTypeDto>();
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

        public AttachmentTypeOutputModel FromDto(AttachmentTypeDto attachmentTypeDto)
        {

            if (attachmentTypeDto == null)
            {
                throw new Exception("Ошибка! Тип вложения не найден!");
            }
            return new AttachmentTypeOutputModel()
            {
                Id = attachmentTypeDto.Id,
                Name = attachmentTypeDto.Name
            };
        }
        public List<AttachmentTypeOutputModel> FromDtos(List<AttachmentTypeDto> attachmentTypeDtos)
        {

            List<AttachmentTypeOutputModel> models = new List<AttachmentTypeOutputModel>();
            if (attachmentTypeDtos == null || attachmentTypeDtos.Count == 0)
            {
                throw new Exception("Ошибка! Вложения не найдены!");
            }
            foreach (var attachmentType in attachmentTypeDtos)
            {
                models.Add(FromDto(attachmentType));
            }
            return models;
        }
    }
}