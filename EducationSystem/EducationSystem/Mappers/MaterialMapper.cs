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

        public MaterialOutputModel FromDto(MaterialDto materialDto)
        {
            return new MaterialOutputModel()
            {
                Id = materialDto.Id,
                Link = materialDto.Link,
                Description = materialDto.Description
            };
        }


        public List<MaterialOutputModel> FromDtos(List<MaterialDto> materialDtos)
        {
            List<MaterialOutputModel> models = new List<MaterialOutputModel>();

            foreach (var material in materialDtos)
            {
                models.Add(FromDto(material));
            }
            return models;
        }

    }
}
