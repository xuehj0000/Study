
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using NLog;

namespace z_AdminLTE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("��ʼ�� main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: �������ô���
                logger.Error(exception, "�����쳣��ֹͣ����");
                throw;
            }
            finally
            {
                // ȷ����Ӧ�ó����˳�֮ǰˢ�²�ֹͣ�ڲ���ʱ��/�̣߳�����Linux�ϳ��ֶַδ���
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();
    }
}
