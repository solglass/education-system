using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class MaterialMapper
    {
        public MaterialDto ToDto(MaterialInputModel inputModel)
        {
            var materialDto = new MaterialDto();
            materialDto.Id = inputModel.Id;
            materialDto.Link = inputModel.Link;
            materialDto.Description = inputModel.Description;
            materialDto.IsDeleted = false;
            return materialDto;
        }

        public List<MaterialDto> ToDtos(List<MaterialInputModel> inputModels)
        {
            List<MaterialDto> result = new List<MaterialDto>();

            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }

        public AttachmentOutputModel FromDto(AttachmentDto attachmentDto)
        {
            var attachmentTypeMapper = new AttachmentTypeMapper();

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

            foreach (var attachment in attachmentDtos)
            {
                models.Add(FromDto(attachment));
            }
            return models;
        }

    }
}
