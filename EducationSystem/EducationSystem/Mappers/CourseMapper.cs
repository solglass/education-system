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
            var groupMapper = new GroupMapper();
            var themeMapper = new ThemeMapper();
            return new CourseDto
            {
                Id=inputModel.Id,
                Name = inputModel.Name,
                Description=inputModel.Description,
                Duration=inputModel.Duration,
                IsDeleted=inputModel.IsDeleted,
               // Groups=groupMapper.ToDto(inputModel.Groups),
                Themes=themeMapper.ToDto(inputModel.Themes)
            };
        }

        public List<CourseDto> ToDto(List<CourseInputModel> inputModels)
        {
            List<CourseDto> courses = new List<CourseDto>();
            if (inputModels != null)
            {
                foreach (var model in inputModels)
                {
                    courses.Add(new CourseDto()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        Duration = model.Duration,
                        IsDeleted = model.IsDeleted
                    });
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

        public List<CourseOutputModel> FromDto(List<CourseDto> courseDtos)
        {
            List<CourseOutputModel> models = new List<CourseOutputModel>();
            if (courseDtos != null)
            {
                foreach (var course in courseDtos)
                {
                    models.Add(new CourseOutputModel()
                    {
                        //Id = course.Id,
                        //Name = course.Name,
                        //Duration = course.Duration,
                        //Description = course.Description
                    });
                }
            }
            return models;
        }
    }
}
