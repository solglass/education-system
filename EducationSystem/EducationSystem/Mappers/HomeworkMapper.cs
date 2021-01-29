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

            return new HomeworkDto
            {
                Id = inputModel.Id,
                Description = inputModel.Description,
                Group =  new GroupDto { Id = inputModel.GroupId }, //TODO: Use GroupMapper here
                IsOptional = inputModel.IsOptional,
                StartDate = startDate,
                DeadlineDate = deadlineDate
            };
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
