using AutoMapper;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;
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
            CreateMap<UserDto, AuthorOutputModel>();
            CreateMap<AttendanceUpdateInputModel, AttendanceDto>();

            CreateMap<HomeworkDto, HomeworkOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)));
            CreateMap<HomeworkDto, HomeworkSearchOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupId, opts => opts.MapFrom(src => src.Group.Id));
            CreateMap<GroupInputModel, GroupDto>();
            CreateMap<GroupDto, GroupOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyGroupStatusName(src.GroupStatus)));
            CreateMap<TagDto, TagOutputModel>();
            CreateMap<ThemeDto, ThemeOutputModel>();
            CreateMap<HomeworkAttemptDto, HomeworkAttemptOutputModel>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyHomeworkAttemptStatusName(src.HomeworkAttemptStatus)));
            CreateMap<AttachmentInputModel, AttachmentDto>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => (AttachmentType)src.AttachmentTypeId));

            CreateMap<LessonInputModel, LessonDto>();
            CreateMap<LessonDto, LessonOutputModel>()
                .ForMember(dest => dest.LessonDate, opts => opts.MapFrom(src => src.Date.ToString(_dateFormat)));
            CreateMap<CourseDto, CourseOutputModel>();
            CreateMap<CourseInputModel, CourseDto>();
            CreateMap<AttendanceReportDto, AttendanceReportOutputModel>();

            CreateMap<HomeworkInputModel, HomeworkDto>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.StartDate)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.DeadlineDate)))
                .ForMember(dest => dest.Group, opts => opts.MapFrom(src => new GroupDto() { Id = src.GroupId }))
               .ForMember(dest => dest.Themes, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<ThemeDto>(r => new ThemeDto() { Id = r })))
               .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<TagDto>(r => new TagDto() { Id = r })));

            CreateMap<HomeworkUpdateInputModel, HomeworkDto>()
               .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.StartDate)))
               .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.DeadlineDate)))
               .ForMember(dest => dest.Themes, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<ThemeDto>(r => new ThemeDto() { Id = r })))
               .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<TagDto>(r => new TagDto() { Id = r })));

            CreateMap<CommentDto, CommentOutputModel>();
            CreateMap<CommentInputModel, CommentDto>()
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => new UserDto() { Id = src.AuthorId }));

            CreateMap<AttachmentDto, AttachmentOutputModel>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyAttachmentTypeName(src.AttachmentType)));
            CreateMap<HomeworkAttemptInputModel, HomeworkAttemptDto>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => (HomeworkAttemptStatus)src.HomeworkAttemptStatusId))
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src =>new UserDto() { Id = src.AuthorId }));

            CreateMap<HomeworkAttemptWithCountDto, HomeworkAttemptWithCountOutputModel>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyHomeworkAttemptStatusName(src.HomeworkAttemptStatus)));

        }
    }
}
