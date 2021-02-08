using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class GroupReportMapper
    {

        public GroupReportOutputModel FromDto(GroupReportDto groupReportDto)
        {

            return new GroupReportOutputModel
            {
                GroupId = groupReportDto.GroupId,
                Name = groupReportDto.Name,
                StartDate = (groupReportDto.StartDate).ToString("dd.MM.yyyy"),
                EndDate = (groupReportDto.StartDate).ToString("dd.MM.yyyy"),
                StudentCount = groupReportDto.StudentCount,
                TutorCount = groupReportDto.TutorCount,
                TeacherCount = groupReportDto.TeacherCount

            };

        }

        public List<GroupReportOutputModel> FromDtos(List<GroupReportDto> dtos)
        {
            List<GroupReportOutputModel> result = new List<GroupReportOutputModel>();
            foreach (var item in dtos)
            {
                result.Add(FromDto(item));
            }
            return result;
        }
    }
}
