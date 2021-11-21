using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 读取固定根目录下的appsettings.json
    /// </summary>
    public class ConfigurationManager
    {
        private static string _sqlConnectionStringCustom = null;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            _sqlConnectionStringCustom = configuration["ConnectionStrings:Customers"];
        }

        public static string SqlConnectionStringCustom
        {
            get
            {
                return _sqlConnectionStringCustom;
            }
        }
    }
}
