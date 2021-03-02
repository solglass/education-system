using AutoMapper;
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
        private readonly IMapper _mapper;
        public ThemeDto ToDto(ThemeInputModel inputModel)
        {
           if(string.IsNullOrEmpty(inputModel.Name))
            {
                throw new Exception("Ошибка! Не было передано значение Name!");
            }
            List<TagDto> tags = new List<TagDto>();
            if (inputModel.TagIds != null && inputModel.TagIds.Count > 0)
            {
                foreach (var id in inputModel.TagIds)
                {
                    tags.Add(new TagDto { Id = id });
                }
            }
            return new ThemeDto()
            {
                Id=inputModel.Id,
                Name=inputModel.Name,
                Tags=tags
            };
        }

        public List<ThemeDto> ToDtos(List<ThemeInputModel> inputModels)
        {
            List<ThemeDto> themes = new List<ThemeDto>();
            if (inputModels == null || inputModels.Count==0)
            {
                throw new Exception("Ошибка! Темы не найдены!");
            }
            foreach (var model in inputModels)
            {
                themes.Add(ToDto(model));
            }

            return themes;
        }

        public ThemeOutputModel FromDto(ThemeDto themeDto)
        {

            if(themeDto==null)
            {
                throw new Exception("Ошибка! Тема не найдена!");
            }
            var tags = new List<TagOutputModel>();
            if (themeDto.Tags != null && themeDto.Tags.Count > 0)
            {
                tags = _mapper.Map<List<TagOutputModel>>(tags);
            }
            return new ThemeOutputModel()
            {
                Id=themeDto.Id,
                Name=themeDto.Name,
                Tags=tags
            };
        }

        public List<ThemeOutputModel> FromDtos(List<ThemeDto> themeDtos)
        {
            List<ThemeOutputModel> models = new List<ThemeOutputModel>();
            if (themeDtos == null || themeDtos.Count==0)
            {
                throw new Exception("Ошибка! Темы не найдены!");
            }
            foreach (var theme in themeDtos)
            {
                models.Add(FromDto(theme));
            }
            return models;
        }
    }
}
