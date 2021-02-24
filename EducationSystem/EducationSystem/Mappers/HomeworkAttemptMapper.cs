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
                Author = new UserDto { Id = inputModel.AuthorId},
                Comment = inputModel.Comment,
                Homework = new HomeworkDto { Id = inputModel.HomeworkId},
                HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = inputModel.HomeworkAttemptStatusId}
            }; 
        }
        public List<HomeworkAttemptDto> ToDtos(List<HomeworkAttemptInputModel> inputModels)
        {
            List<HomeworkAttemptDto> homeworkAttempts = new List<HomeworkAttemptDto>();

            foreach(HomeworkAttemptInputModel inputModel in inputModels)
            {
                homeworkAttempts.Add(ToDto(inputModel));
            }
            return homeworkAttempts;
        }
        public HomeworkAttemptOutputModel FromDto(HomeworkAttemptDto dto)
        {

            return new HomeworkAttemptOutputModel
            {
                Id = dto.Id,
                Author = new UserOutputModel { Id = dto.Author.Id},
                Comment = dto.Comment,
                HomeworkAttemptStatus = new HomeworkAttemptStatusOutputModel { Id = dto.HomeworkAttemptStatus.Id, Name = dto.HomeworkAttemptStatus.Name},
            };
        }

        public List<HomeworkAttemptOutputModel> FromDtos(List<HomeworkAttemptDto> dtos)
        {

            var outputModels = new List<HomeworkAttemptOutputModel>();

            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
        public UserHomeworkAttemptsOutputModel FromDto(HomeworkAttemptByUserDto dto)
        {
          return new UserHomeworkAttemptsOutputModel
          {
            Id = dto.Id,
            Comment = dto.Comment,
            IsDeleted = dto.IsDeleted,
            StatusId = dto.StatusId,
            CountAttachments = dto.CountAttachments,
            CountComments = dto.CountComments,
            AttemptStatusId = dto.AttemptStatusId,
            AttemptStatusName = dto.AttemptStatusName,
            Homework = new HomeworkOutputModel { Id = dto.Homework.Id, Description = dto.Homework.Description, Group = new GroupOutputModel { Id = dto.Homework.Group.Id } },
            Author = new AuthorOutputModel { Id = dto.Author.Id, FirstName = dto.Author.FirstName, LastName = dto.Author.LastName }
            
          };
        }
        
        public List<UserHomeworkAttemptsOutputModel> FromDtos(List<HomeworkAttemptByUserDto> dtos)
        {
          var outputModels = new List<UserHomeworkAttemptsOutputModel>();
        
          foreach (var dto in dtos)
          {
            outputModels.Add(FromDto(dto));
          }
          return outputModels;
        }

  }
}
