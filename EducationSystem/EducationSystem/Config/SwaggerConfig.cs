using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationSystem.API.Config
{
    public static class SwaggerConfig
    {
        public static void SwaggerExtention(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "EducationSystem" });
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
