using EducationSystem.API.Models.InputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Utils;
using EducationSystem.API.Models.OutputModels;

namespace EducationSystem.API.Mappers
{
    public class UnderstandingLevelMapper
    {
        public UnderstandingLevelDto ToDto(UnderstandingLevelInputModel inputModel)
        {

            return new UnderstandingLevelDto
            {
                ID = inputModel.ID,
                Name = inputModel.Name
            };
        }

        public List<UnderstandingLevelDto> ToDtos(List<UnderstandingLevelInputModel> inputModels)
        {
            List<UnderstandingLevelDto> lessons = new List<UnderstandingLevelDto>();
            foreach (UnderstandingLevelInputModel inputModel in inputModels)
            {

                lessons.Add(ToDto(inputModel));
            }

            return lessons;
        }

        public UnderstandingLevelOutputModel FromDto(UnderstandingLevelDto dto)
        {

            return new UnderstandingLevelOutputModel
            {
                ID = dto.ID,
                Name = dto.Name
            };

        }

        public List<UnderstandingLevelOutputModel> FromDtos(List<UnderstandingLevelDto> dtos)
        {
            var outputModels = new List<UnderstandingLevelOutputModel>();

            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
    }
}