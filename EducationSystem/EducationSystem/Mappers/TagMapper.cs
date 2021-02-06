using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class TagMapper
    {
        public TagDto ToDto(TagInputModel inputModel) 
        {
            if (string.IsNullOrEmpty(inputModel.Name))
            {
                throw new Exception("Ошибка! Не было передано значение Name");
            }
            return new TagDto {
                Id = inputModel.Id,
                Name = inputModel.Name
            };
        }
        public List<TagDto> ToDtos(List<TagInputModel> inputModels)
        {
            List<TagDto> result = new List<TagDto>();
            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }
        public TagOutputModel FromDto(TagDto tagDto)
        {
            return new TagOutputModel()
            {
                Id = tagDto.Id,
                Name = tagDto.Name
            };
        }
        public List<TagOutputModel> FromDtos(List<TagDto> tagDtos)
        {
            List<TagOutputModel> models = new List<TagOutputModel>();
            
            foreach (var tag in tagDtos)
            {
                models.Add(FromDto(tag));
            }
            return models;
        }
    }
}