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
            //var courseMapper = new CourseMapper();
            var groupStatusMapper = new GroupStatusMapper();
            return new GroupDto 
            {
                StartDate =  DateTime.ParseExact(inputModel.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Course = new CourseDto { Id = inputModel.Course.Id },       // delete
                //Course = courseMapper.ToDto(inputModel.Course),
                GroupStatus = groupStatusMapper.ToDto(inputModel.GroupStatus)
            };
        }

        public GroupOutputModel FromDto(GroupDto groupDto) 
        {
            //var courseMapper = new CourseMapper();
            //var groupStatusMapper = new GroupStatusMapper();
            return new GroupOutputModel
            {
                Id = groupDto.Id,
                StartDate = (groupDto.StartDate).ToString(),
                Course = groupDto.Course,                                    // delete
                GroupStatus = groupDto.GroupStatus                           // delete
                //Course = courseMapper.FromDto(groupDto.Course),
                //GroupStatus = groupStatusMapper.FromDto(groupDto.GroupStatus)
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
