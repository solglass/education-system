using EducationSystem.API.Config;
using EducationSystem.API.Middleware;
using EducationSystem.Core.Config;
using EducationSystem.Core.Config.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace EducationSystem
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {           
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            if (!env.IsProduction())
            {
                builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json");
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AuthentificationConfigExtention();
            services.AddControllers();
            services.SwaggerExtention();
            services.RegistrateServicesConfig();
            services.AddAutoMapper(typeof(Startup));
            services.Configure<AppSettingsConfig>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "EducationSystem");
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
