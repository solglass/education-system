using AutoMapper;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API
{
    public class AutomapperConfig : Profile
    {
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _dateFormatWithTime = "dd.MM.yyyy H:mm:ss";
        public AutomapperConfig()
        {
            CreateMap<UserInputModel, UserDto>()
               .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => DateTime.ParseExact(src.BirthDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
               .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles.ConvertAll<Enum>(c=>(Role)c)));
            CreateMap<UpdateUserInputModel, UserDto>()
               .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => DateTime.ParseExact(src.BirthDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)));             
            CreateMap<UserDto, AuthorOutputModel>();
            CreateMap<UserDto, UserOutputModel>()
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.BirthDate.ToString(_dateFormat)))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles.Select(r => (int)r)));
            CreateMap<UserDto, UserOutputExtendedModel>()
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.BirthDate.ToString(_dateFormat)));

            CreateMap<PaymentInputModel, PaymentDto>()
                .ForMember(dest => dest.Period, opts => opts.MapFrom(src => Converters.StrToDateTimePeriod(src.Period)))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => Converters.StrToDateTime(src.Date)));
            CreateMap<PaymentDto, PaymentOutputModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString(_dateFormat)))
                .ForMember(dest => dest.Period, opts => opts.MapFrom(src => Converters.StrToStrOutputPeriod(src.Period)))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new AuthorOutputModel { Id = src.Student.Id, FirstName = src.Student.FirstName, LastName = src.Student.LastName, UserPic = src.Student.UserPic }));

            CreateMap<AttendanceInputModel, AttendanceDto>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserDto() { Id = src.UserId }));

            CreateMap<HomeworkDto, HomeworkOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupsIds, opts => opts.MapFrom(src => src.Groups.ConvertAll<int>(r => r.Id)));
            CreateMap<HomeworkDto, HomeworkSearchOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => src.DeadlineDate.ToString(_dateFormat)))
                .ForMember(dest=> dest.CourseId, opts => opts.MapFrom(src => src.Course.Id));

            CreateMap<GroupInputModel, GroupDto>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.StartDate)))
                .ForMember(dest => dest.Course, opts => opts.MapFrom(src => new CourseDto() { Id = src.CourseId }))
                .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src => (GroupStatus)src.GroupStatusId));
            CreateMap<StudentGroupInputModel, StudentGroupDto>();
            CreateMap<StudentGroupDto, StudentGroupOutputModel>();

            CreateMap<GroupDto, GroupOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src=>FriendlyNames.GetFriendlyGroupStatusName(src.GroupStatus)))
                .ForMember(dest => dest.GroupStatusId, opts => opts.MapFrom(src => (int)src.GroupStatus))
                .ForMember(dest => dest.EndDate, opts => opts.MapFrom(src => src.EndDate.ToString(_dateFormat)));
            CreateMap<GroupDto, GroupWithUsersOutputModel>()
               .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
               .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyGroupStatusName(src.GroupStatus)))
               .ForMember(dest => dest.GroupStatusId, opts => opts.MapFrom(src => (int)src.GroupStatus));

            CreateMap<TagInputModel, TagDto>();
            CreateMap<TagDto, TagOutputModel>();

            CreateMap<ThemeInputModel, ThemeDto>()
                .ForMember(dest=>dest.Tags, opts=>opts.MapFrom(src=>src.TagIds.ConvertAll<TagDto>(t=> new TagDto { Id = t })));
            CreateMap<ThemeDto, ThemeOutputModel>();
            CreateMap<ThemeDto, ThemeExtendedOutputModel>();

            CreateMap<HomeworkAttemptDto, HomeworkAttemptOutputModel>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyHomeworkAttemptStatusName(src.HomeworkAttemptStatus)));

            CreateMap<AttachmentInputModel, AttachmentDto>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => (AttachmentType)src.AttachmentTypeId));

            CreateMap<LessonInputModel, LessonDto>()
                .ForMember(dest => dest.Group, opts => opts.MapFrom(src => new GroupDto() { Id = src.GroupId }))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => Converters.StrToDateTime(src.LessonDate)))
                .ForMember(dest => dest.Themes, opts => opts.MapFrom(src => src.ThemesId.ConvertAll<ThemeDto>(r => new ThemeDto() { Id = r })));
            CreateMap<LessonDto, LessonOutputModel>()
                .ForMember(dest => dest.LessonDate, opts => opts.MapFrom(src => src.Date.ToString(_dateFormat)))
                .ForMember(dest => dest.Group, opts => opts.MapFrom(src => new GroupDto() { Id = src.Group.Id}));

            CreateMap<FeedbackInputModel, FeedbackDto>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserDto() { Id = src.UserId }))
                .ForMember(dest => dest.UnderstandingLevel, opts => opts.MapFrom(src => (UnderstandingLevel)src.UnderstandingLevelId));
            CreateMap<FeedbackDto, FeedbackOutputModel>()
                .ForMember(dest => dest.UnderstandingLevel, opts=>opts.MapFrom(src=>FriendlyNames.GetFriendlyUnderstandingLevelName(src.UnderstandingLevel)));

            CreateMap<AttendanceDto, AttendanceOutputModel>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserDto()
                {
                    Id = src.User.Id,
                    FirstName = src.User.FirstName,
                    LastName = src.User.LastName,
                    UserPic = src.User.UserPic
                }));

            CreateMap<CourseDto, CourseExtendedOutputModel>();
            CreateMap<CourseDto, CourseOutputModel>();
            CreateMap<CourseInputModel, CourseDto>()
                .ForMember(dest=>dest.Themes, opts=>opts.MapFrom(src=>src.ThemeIds.ConvertAll<ThemeDto>(t=>new ThemeDto { Id = t })))
                .ForMember(dest=>dest.Materials, opts=>opts.MapFrom(src=>src.MaterialIds.ConvertAll<MaterialDto>(t=>new MaterialDto { Id = t })));
            CreateMap<AttendanceReportDto, AttendanceReportOutputModel>();

            CreateMap<GroupReportDto, GroupReportOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.EndDate, opts => opts.MapFrom(src => src.EndDate.ToString(_dateFormat)));

            CreateMap<NumberOfLessonsForGroupToCompleteTheThemeDto, NumberOfLessonsForGroupToCompleteTheThemeOutputModel>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => src.StartDate.ToString(_dateFormat)))
                .ForMember(dest => dest.GroupStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyGroupStatusName(src.GroupStatus)));

            CreateMap<AttendanceReportDto, AttendanceReportOutputModel>();

            CreateMap<HomeworkInputModel, HomeworkDto>()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.StartDate)))
                .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.DeadlineDate)))
                .ForMember(dest => dest.Course, opts => opts.MapFrom(src => new CourseDto() { Id = src.CourseId }))
                .ForMember(dest => dest.Groups, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<GroupDto>(r => new GroupDto() { Id = r })))
                .ForMember(dest => dest.Themes, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<ThemeDto>(r => new ThemeDto() { Id = r })))
                .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.TagIds.ConvertAll<TagDto>(r => new TagDto() { Id = r })));

            CreateMap<HomeworkUpdateInputModel, HomeworkDto>()
               .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.StartDate)))
               .ForMember(dest => dest.DeadlineDate, opts => opts.MapFrom(src => Converters.StrToDateTime(src.DeadlineDate)))
               .ForMember(dest => dest.Groups, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<GroupDto>(r => new GroupDto() { Id = r })))
               .ForMember(dest => dest.Themes, opts => opts.MapFrom(src => src.ThemeIds.ConvertAll<ThemeDto>(r => new ThemeDto() { Id = r })))
               .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.TagIds.ConvertAll<TagDto>(r => new TagDto() { Id = r })));

            CreateMap<CommentDto, CommentOutputModel>();
            CreateMap<CommentInputModel, CommentDto>()
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => new UserDto() { Id = src.AuthorId }));

            CreateMap<CommentUpdateInputModel, CommentDto>();

            CreateMap<AttachmentDto, AttachmentOutputModel>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyAttachmentTypeName(src.AttachmentType)));
            CreateMap<HomeworkAttemptInputModel, HomeworkAttemptDto>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => (HomeworkAttemptStatus)src.HomeworkAttemptStatusId))
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src =>new UserDto() { Id = src.AuthorId }));

            CreateMap<HomeworkAttemptWithCountDto, HomeworkAttemptWithCountOutputModel>()
                .ForMember(dest => dest.HomeworkAttemptStatus, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyHomeworkAttemptStatusName(src.HomeworkAttemptStatus)));
            CreateMap<AttachmentInputModel, AttachmentDto>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => (AttachmentType)src.AttachmentTypeId));
            CreateMap<AttachmentDto, AttachmentOutputModel>()
                .ForMember(dest => dest.AttachmentType, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyAttachmentTypeName(src.AttachmentType)));


            CreateMap<MaterialInputModel, MaterialDto>();
            CreateMap<MaterialDto, MaterialOutputModel>();

            CreateMap<NotificationInputModel, NotificationDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => (src.Date == null) ? DateTime.Now : Converters.StrToDateTimeWithTime(src.Date)));
            CreateMap<NotificationDto, NotificationOutputModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString(_dateFormatWithTime)));
        }
    }
}
