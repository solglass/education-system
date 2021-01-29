using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Mappers
{
    public class ThemeMapper
    {
        public ThemeDto ToDto(ThemeInputModel inputModel)
        {
            var groupStatusMapper = new GroupStatusMapper();
            return new GroupDto
            {
                StartDate = DateTime.ParseExact(inputModel.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Course = new CourseDto { Id = inputModel.Course.Id },
                GroupStatus = groupStatusMapper.ToDto(inputModel.GroupStatus)
            };
        }
    }
}
