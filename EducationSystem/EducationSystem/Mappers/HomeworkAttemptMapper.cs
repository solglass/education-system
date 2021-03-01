using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Core.Enums;
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
                HomeworkAttemptStatus = (HomeworkAttemptStatus)inputModel.HomeworkAttemptStatusId
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
                Author = new AuthorOutputModel { Id = dto.Author.Id},
                Comment = dto.Comment,
                HomeworkAttemptStatus = FriendlyNames.GetFriendlyHomeworkAttemptStatusName(dto.HomeworkAttemptStatus) 
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
        public HomeworkAttemptWithCountOutputModel FromDto(HomeworkAttemptWithCountDto dto)
        {
          return new HomeworkAttemptWithCountOutputModel
          {
            Id = dto.Id,
            Comment = dto.Comment,
            StatusId = dto.StatusId,
            CountComments = dto.CountComments,
            CountAttachments = dto.CountAttachments,
            HomeworkAttemptStatus = FriendlyNames.GetFriendlyHomeworkAttemptStatusName(dto.HomeworkAttemptStatus),
              Homework = new HomeworkOutputModel { Id = dto.Homework.Id, Description = dto.Homework.Description, Group = new GroupOutputModel { Id = dto.Homework.Group.Id } },
            AuthorAttachment = new AuthorOutputModel { Id = dto.Author.Id, FirstName = dto.Author.FirstName, LastName = dto.Author.LastName, UserPic = dto.Author.UserPic }
            
          };
        }
        
        public List<HomeworkAttemptWithCountOutputModel> FromDtos(List<HomeworkAttemptWithCountDto> dtos)
        {
          var outputModels = new List<HomeworkAttemptWithCountOutputModel>();
        
          foreach (var dto in dtos)
          {
            outputModels.Add(FromDto(dto));
          }
          return outputModels;
        }

  }
}
