using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class CourseMapper
    {
        public CourseDto ToDto(CourseInputModel inputModel)
        {
            var themeMapper = new ThemeMapper();
            return new CourseDto
            {
                Id=inputModel.Id,
                Name = inputModel.Name,
                Description=inputModel.Description,
                Duration=inputModel.Duration                //TODO list of theme ids and validation, duration
            };
        }

        public List<CourseDto> ToDtos(List<CourseInputModel> inputModels)
        {
            List<CourseDto> courses = new List<CourseDto>();
            if (inputModels != null)
            {
                foreach (var model in inputModels)
                {
                    courses.Add(ToDto(model));
                }
            }
            return courses;
        }

        public CourseOutputModel FromDto(CourseDto CourseDto)
        {
            return new CourseOutputModel()
            {

            };
        }

        public List<CourseOutputModel> FromDtos(List<CourseDto> courseDtos)
        {
            List<CourseOutputModel> models = new List<CourseOutputModel>();
            if (courseDtos != null)
            {
                foreach (var course in courseDtos)
                {
                    models.Add(FromDto(course));
                }
            }
            return models;
        }
    }
}
