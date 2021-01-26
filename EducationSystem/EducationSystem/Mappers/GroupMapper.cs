using System;
using System.Globalization;
using EducationSystem.API.Models;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    public class GroupMapper
    {
        public GroupDto ToDto(GroupInputModel inputModel) 
        {
            var groupStatusMapper = new GroupStatusMapper();
            return new GroupDto {
                StartDate =  DateTime.ParseExact(inputModel.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Course = new CourseDto { Id = inputModel.Course.Id },
                GroupStatus = groupStatusMapper.ToDto(inputModel.GroupStatus)
            };
        }

        //public GroupOutputModel FromDto(GroupDto dto) {}

        /*
            public List<GroupOutputModel> FromDtos(List<GroupDto> dtos) {
                List<GroupOutputModel> result;
                foreach (var item in dtos)
                {
                    result.Add(FromDto(item));
                }
                return result;
            }
        */
    }
}
