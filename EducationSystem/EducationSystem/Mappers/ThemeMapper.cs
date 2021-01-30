using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class ThemeMapper
    {
        public ThemeDto ToDto(ThemeInputModel inputModel)
        {
           if(string.IsNullOrEmpty(inputModel.Name))
            {
                throw new Exception("Ошибка! Не было передано значение Name!");
            }
            return new ThemeDto()
            {
                Id=inputModel.Id,
                Name=inputModel.Name
            };
        }

        public List<ThemeDto> ToDtos(List<ThemeInputModel> inputModels)
        {
            List<ThemeDto> themes = new List<ThemeDto>();
            if (inputModels != null)
            {
                throw new Exception("Ошибка! Темы не обнаружены!");
            }
            foreach (var model in inputModels)
            {
                themes.Add(ToDto(model));
            }

            return themes;
        }

        public ThemeOutputModel FromDto(ThemeDto themeDto)
        {
            return new ThemeOutputModel()
            {
                Id=themeDto.Id,
                Name=themeDto.Name
            };
        }

        public List<ThemeOutputModel> FromDtos(List<ThemeDto> themeDtos)
        {
            List<ThemeOutputModel> models = new List<ThemeOutputModel>();
            if (themeDtos != null)
            {
                throw new Exception("Ошибка! Темы не обнаружены!");
            }
            foreach (var theme in themeDtos)
            {
                models.Add(FromDto(theme));
            }
            return models;
        }
    }
}
