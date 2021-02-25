using EducationSystem.API.Models.InputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Utils;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Core.Enums;

namespace EducationSystem.API.Mappers
{
    public class FeedbackMapper
    {
        public FeedbackDto ToDto(FeedbackInputModel inputModel)
        {   
            return new FeedbackDto
            {
                ID = inputModel.ID,
                UserID = inputModel.UserID,
                Messege = inputModel.Messege,
                LessonID = inputModel.LessonID,
                UnderstandingLevelID = inputModel.UnderstandingLevelID,
            };
        }

        public List<FeedbackDto> ToDtos(List<FeedbackInputModel> inputModels)
        {
            List<FeedbackDto> lessons = new List<FeedbackDto>();
            foreach (FeedbackInputModel inputModel in inputModels)
            {

                lessons.Add(ToDto(inputModel));
            }

            return lessons;
        }

        public FeedbackOutputModel FromDto(FeedbackDto dto)
        {

            return new FeedbackOutputModel
            {
                ID = dto.ID,
                UserID = dto.UserID,
                Messege = dto.Messege,
                LessonID = dto.LessonID,
                UnderstandingLevel = FriendlyNames.GetFriendlyUnderstandingLevelName(dto.UnderstandingLevel)
            };

        }

        public List<FeedbackOutputModel> FromDtos(List<FeedbackDto> dtos)
        {
            var outputModels = new List<FeedbackOutputModel>();

            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
    }
}

