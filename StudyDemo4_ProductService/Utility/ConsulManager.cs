using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace StudyDemo4_ProductService
{
    public static class ConsulManager
    {
        /// <summary>
        /// 扩展中间件
        /// </summary>
        public static void UseConsul(this IApplicationBuilder app, IConfiguration configuration, IConsulClient consul)
        {
            RegServer(configuration, consul);
        }

        private static void RegServer(IConfiguration configuration, IConsulClient consul)
        {
            string consulGroup = configuration["ConsulGroup"];

            var ip = configuration["ip"];
            var port = Convert.ToInt32(configuration["port"].ToString());
            var serviceId = $"{consulGroup}_{ip}_{port}";

            // 健康检查
            var check = new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(6),
                HTTP = $"http://{ip}:{port}/heart",
                Timeout = TimeSpan.FromSeconds(2),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(2)
            };

            var regist = new AgentServiceRegistration
            {
                Check = check,
                Address = ip,
                Port = port,
                Name = consulGroup,
                ID = serviceId
            };
            consul.Agent.ServiceRegister(regist);
        }
    }
}
