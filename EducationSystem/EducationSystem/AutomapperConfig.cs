using AutoMapper;
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
        }
    }
}
