using AutoMapper;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API
{
    public class AutomapperConfig : Profile
    {
        private const string _dateFormat = "dd.MM.yyyy";
        public AutomapperConfig()
        {
            CreateMap<UserInputModel, UserDto>();
            CreateMap<UserDto, UserOutputModel>()
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.BirthDate.ToString(_dateFormat)));
            CreateMap<AttendanceUpdateInputModel, AttendanceDto>();
            CreateMap<HomeworkDto, HomeworkOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)));
            CreateMap<HomeworkDto, HomeworkSearchOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)))
                .ForMember(dest=> dest.GroupId, opts => opts.MapFrom(src => src.Group.Id));
            CreateMap<GroupInputModel, GroupDto>();
            CreateMap<GroupDto, GroupOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src=>FriendlyNames.GetFriendlyGroupStatusName(src.GroupStatus)));
            CreateMap<TagDto, TagOutputModel>();
            CreateMap<ThemeDto, ThemeOutputModel>();
            CreateMap<LessonInputModel,LessonDto>();
            CreateMap<LessonDto, LessonOutputModel>()
                .ForMember(dest => dest.LessonDate, opts => opts.MapFrom(src => src.Date.ToString(_dateFormat)));
            CreateMap<CourseDto, CourseOutputModel>();
            CreateMap<CourseInputModel, CourseDto>();
            CreateMap<AttendanceReportDto, AttendanceReportOutputModel>();
            CreateMap<MaterialInputModel, MaterialDto>();
        }
    }
}
