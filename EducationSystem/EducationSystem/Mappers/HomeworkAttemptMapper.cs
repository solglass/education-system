using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class HomeworkAttemptMapper
    {
        public HomeworkAttemptDto ToDto(HomeworkAttemptInputModel inputModel)
        {
            return new HomeworkAttemptDto
            {
                Id = inputModel.Id,
                Comment = inputModel.Comment,
                Author = new UserDto { Id = inputModel.AuthorId },
                Homework = new HomeworkDto { Id = inputModel.HomeworkId },
                HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = inputModel.Id }
            };
        }

        public HomeworkAttemptOutputModel FromDto(HomeworkAttemptDto dto)
        {
            return new HomeworkAttemptOutputModel
            {
                Id = dto.Id,
                Comment = dto.Comment,
                Author = new UserOutputModel { Id = dto.Author.Id },
                HomeworkAttemptStatus = new HomeworkAttemptStatusOutputModel { Id = dto.HomeworkAttemptStatus.Id },
                IsDeleted = dto.IsDeleted
            };
        }

        public List<HomeworkAttemptOutputModel> FromDtos(List<HomeworkAttemptDto> dtos)
        {
            var outputModels = new List<HomeworkAttemptOutputModel>();
            foreach(var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
    }
}
