using EducationSystem.Business;
using EducationSystem.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Config
{
    public static class ServicesConfig
    {
        public static void RegistrateServicesConfig(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IHomeworkService, HomeworkService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITagRepository, TagRepository>();           
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();           
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IHomeworkAttemptRepository, HomeworkAttemptRepository>();

            services.AddSingleton<EmailService>();
        }
    }
}
