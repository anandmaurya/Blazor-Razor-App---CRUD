using Microsoft.Extensions.Configuration;

namespace EmployeeApp.AppSettings
{
    public static class ApplicationConfiguration
    {
        private static IConfigurationRoot _configuration;
        static ApplicationConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
        public static string GetSetting(string key)
        {
            return _configuration[key];
        }
    }
}
