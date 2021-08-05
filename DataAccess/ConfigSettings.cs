using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyProject.DataAccess
{
    public class ConfigSettings
    {
        public static string ConnectionString { get; }

        static ConfigSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            ConnectionString = configurationBuilder.Build().GetSection("ConnectionString").Value;
        }
    }
}
