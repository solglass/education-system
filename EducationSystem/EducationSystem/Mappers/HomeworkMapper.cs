using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class HomeworkMapper
    {
        public HomeworkDto ToDto(HomeworkInputModel inputModel)
        {
            if(string.IsNullOrEmpty(inputModel.StartDate) || string.IsNullOrEmpty(inputModel.DeadlineDate))
            {
                throw new Exception("Ошибка! Не было передано значение StartDate или DeadlineDate");
            }

            var (isStartDateParsed, startDate) = Converters.StrToDateTime(inputModel.StartDate);
            var (isDeadlineDateParsed, deadlineDate) = Converters.StrToDateTime(inputModel.DeadlineDate);

            if(!isStartDateParsed || !isDeadlineDateParsed)
            {
                throw new Exception("Ошибка! Неверный формат StartDate или DeadlineDate");
            }

            var dto = new HomeworkDto
            {
                Id = inputModel.Id,
                Description = inputModel.Description,
                Group = new GroupDto { Id = inputModel.GroupId }, //TODO: Use GroupMapper here
                IsOptional = inputModel.IsOptional,
                StartDate = startDate,
                DeadlineDate = deadlineDate
            };

            dto.Themes = new List<ThemeDto>();
            inputModel.ThemeIds.ForEach(themeId =>
            {
                dto.Themes.Add(new ThemeDto() { Id = themeId });
            });

            dto.Tags = new List<TagDto>();
            inputModel.ThemeIds.ForEach(tagId =>
            {
                dto.Tags.Add(new TagDto() { Id = tagId });
            });

            return dto;
        }
        public List<HomeworkDto> ToDtos(List<HomeworkInputModel> inputModels)
        {
            List<HomeworkDto> homeworks = new List<HomeworkDto>();
            foreach (HomeworkInputModel inputModel in inputModels)
            {
               
                homeworks.Add(ToDto(inputModel));
            }

            return homeworks;
        }

        public HomeworkOutputModel FromDto(HomeworkDto dto)
        {

            return new HomeworkOutputModel
            {
                Id = dto.Id,
                Description = dto.Description,
                Group = new GroupOutputModel { Id = dto.Group.Id },
                IsOptional = dto.IsOptional,
                StartDate = Converters.DateTimeToStr(dto.StartDate),
                DeadlineDate = Converters.DateTimeToStr(dto.DeadlineDate)
            };
        }

        public List<HomeworkOutputModel> FromDtos(List<HomeworkDto> dtos)
        {
            var outputModels = new List<HomeworkOutputModel>();
            
            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }

    }
}
