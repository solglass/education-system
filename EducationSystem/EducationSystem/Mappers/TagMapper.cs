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
            return new TagDto {
                Id = inputModel.Id,
                Name = inputModel.Name
            };
        }
        public List<TagDto> ToDtos(List<TagInputModel> inputModels)
        {
            List<TagDto> result = new List<TagDto>();
            if (inputModels == null || inputModels.Count == 0)
            {
                throw new Exception("Tags не найдены!");
            }
            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }
        public TagOutputModel FromDto(TagDto tagDto)
        {
            if (tagDto == null)
            {
                throw new Exception("Tag не найден!");
            }
            return new TagOutputModel()
            {
                Id = tagDto.Id,
                Name = tagDto.Name
            };
        }
        public List<TagOutputModel> FromDtos(List<TagDto> tagDtos)
        {
            List<TagOutputModel> models = new List<TagOutputModel>();
            if (tagDtos == null || tagDtos.Count == 0)
            {
                throw new Exception("Tags не найдены!");
            }
            foreach (var tag in tagDtos)
            {
                models.Add(FromDto(tag));
            }
            return models;
        }
    }
}