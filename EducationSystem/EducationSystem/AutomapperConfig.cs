﻿using AutoMapper;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
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
            CreateMap<GroupInputModel, GroupDto>();
            CreateMap<GroupDto, GroupOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)));
            CreateMap<TagDto, TagOutputModel>();
            CreateMap<ThemeDto, ThemeOutputModel>();
            CreateMap<LessonInputModel,LessonDto>();
            CreateMap<LessonDto, LessonOutputModel>()
                .ForMember(dest => dest.LessonDate, opts => opts.MapFrom(src => src.Date.ToString(_dateFormat)));
            CreateMap<CourseDto, CourseOutputModel>();
            CreateMap<CourseInputModel, CourseDto>();
        }
    }
}
