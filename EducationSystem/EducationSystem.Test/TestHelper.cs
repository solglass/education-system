using EducationSystem.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Testing.json")
                .Build();
        }

        public static AppSettingsConfig GetApplicationConfiguration(string outputPath)
        {
            var configuration = new AppSettingsConfig();
            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .Bind(configuration);

            return configuration;
        }

        public static IOptions<AppSettingsConfig> GetIOptionsFromAppSettings(string outputPath)
        {
            var settings = GetApplicationConfiguration(outputPath);
            IOptions<AppSettingsConfig> appSettingsOptions = Options.Create(settings);

            return appSettingsOptions;
        }
    }
}
