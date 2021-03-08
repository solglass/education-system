using EducationSystem.API.Models.InputModels;
using EducationSystem.Data.Models;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Utils;
using EducationSystem.API.Models.OutputModels;

namespace EducationSystem.API.Mappers
{
    public class LessonMapper
    {
        public LessonDto ToDto(LessonInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.LessonDate))
            {
                throw new Exception("Ошибка! Не было передано значение LessonDate");
            }

            var (isLessonDateParsed, LessonData) = Converters.StrToDateTime(inputModel.LessonDate);

            if (!isLessonDateParsed)
            {
                throw new Exception("Ошибка! Неверный формат LessonDate");
            }

            return new LessonDto
            {
                Id = inputModel.ID,
                GroupId = inputModel.GroupID,
                Comment = inputModel.Comment,
                Date = DateTime.ParseExact(inputModel.LessonDate, "dd.MM.yyyy", CultureInfo.InvariantCulture)
            };
        }

        public List<LessonDto> ToDtos(List<LessonInputModel> inputModels)
        {
            List<LessonDto> lessons = new List<LessonDto>();
            foreach (LessonInputModel inputModel in inputModels)
            {

                lessons.Add(ToDto(inputModel));
            }

            return lessons;
        }

        public LessonOutputModel FromDto(LessonDto dto)
        {

            return new LessonOutputModel
            {
                ID = dto.Id,
                GroupID = dto.GroupId,
                Comment = dto.Comment,
                LessonDate = Converters.DateTimeToStr(dto.Date)
            };
        
        }

        public List<LessonOutputModel> FromDtos(List<LessonDto> dtos)
        {
            var outputModels = new List<LessonOutputModel>();

            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
    }
}
