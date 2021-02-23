using System;
using System.Collections.Generic;
using System.Globalization;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    public class GroupMapper
    {
        public GroupDto ToDto(GroupInputModel inputModel) 
        {
            var groupStatusMapper = new GroupStatusMapper();
            return new GroupDto 
            {
                StartDate =  DateTime.ParseExact(inputModel.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                GroupStatus = groupStatusMapper.ToDto(inputModel.GroupStatus).groupStatus
            };
        }

        public GroupOutputModel FromDto(GroupDto groupDto) 
        {
            return new GroupOutputModel
            {
                Id = groupDto.Id,
                StartDate = (groupDto.StartDate).ToString("dd.MM.yyyy"),

            };

        }

        public List<GroupOutputModel> FromDtos(List<GroupDto> dtos)
        {
            List<GroupOutputModel> result = new List<GroupOutputModel>();
            foreach (var item in dtos)
            {
                result.Add(FromDto(item));
            }
            return result;
        }

        

            
    }
}
