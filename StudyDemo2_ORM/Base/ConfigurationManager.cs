using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 读取固定根目录下的appsettings.json
    /// </summary>
    public class ConfigurationManager
    {
        //private static string _sqlConnectionStringCustom = null;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            //_sqlConnectionStringCustom = configuration["ConnectionStrings:Customers"];
            SqlConnectionStringWrite = configuration["ConnectionStrings:Write"];
            SqlConnectionStringRead = configuration.GetSection("ConnectionStrings").GetSection("Read").GetChildren().Select(r => r.Value).ToArray();
        }

        //public static string SqlConnectionStringCustom
        //{
        //    get {return _sqlConnectionStringCustom;}
        //}

        public static string SqlConnectionStringWrite { get; set; }
        public static string[] SqlConnectionStringRead { get; set; }
    }
}
