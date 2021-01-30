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
           
            return new ThemeDto()
            {
                Id=inputModel.Id,
                Name=inputModel.Name
            };
        }

        public List<ThemeDto> ToDto(List<ThemeInputModel> inputModels)
        {
            List<ThemeDto> themes = new List<ThemeDto>();
            if (inputModels != null)
            {
                foreach (var model in inputModels)
                {
                    themes.Add(new ThemeDto()
                    {
                        Id = model.Id,
                        Name = model.Name
                    });
                }
            }
            return themes;
        }

        public ThemeOutputModel FromDto(ThemeDto themeDto)
        {
            return new ThemeOutputModel()
            {

            };
        }

        public List<ThemeOutputModel> FromDto(List<ThemeDto> themeDtos)
        {
            List<ThemeOutputModel> models = new List<ThemeOutputModel>();
            if (themeDtos != null)
            {
                foreach (var theme in themeDtos)
                {
                    models.Add(new ThemeOutputModel()
                    {
                        //Id = theme.Id,
                        //Name = theme.Name
                    });
                }
            }
            return models;
        }
    }
}
