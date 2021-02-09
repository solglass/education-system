using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class RoleMapper
    {
        public RoleDto ToDto(RoleInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Name))
            {
                throw new Exception("Ошибка! Не было передано значение Name");
            }
            return new RoleDto
            {
                Id = inputModel.Id,
                Name = inputModel.Name
            };
        }
        public List<RoleDto> ToDtos(List<RoleInputModel> inputModels)
        {
            List<RoleDto> result = new List<RoleDto>();
            foreach (var model in inputModels)
            {
                result.Add(ToDto(model));
            }

            return result;
        }
        public RoleOutputModel FromDto(RoleDto roleDto)
        {
            return new RoleOutputModel()
            {
                Id = roleDto.Id,
                Name = roleDto.Name
            };
        }
        public List<RoleOutputModel> FromDtos(List<RoleDto> roleDtos)
        {
            List<RoleOutputModel> models = new List<RoleOutputModel>();

            foreach (var role in roleDtos)
            {
                models.Add(FromDto(role));
            }
            return models;
        }
    }

}

