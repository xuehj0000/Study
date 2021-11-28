using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace z_AdminLTE
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // �˷���������ʱ���á�ʹ�ô˷�������������ӷ���
        public void ConfigureServices(IServiceCollection services)
        {
            // ����SqlServer
            services.AddDapper("SqlDb", r =>
            {
                r.ConnectionString = Configuration.GetConnectionString("Default");
                r.DbType = DbType.SqlServer;
            });

            // �������ӿ���������ͼ��Razorҳ����ʽ�ı���Զ�����
            services.AddControllersWithViews().AddRazorRuntimeCompilation();  

            // Ȩ����֤����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Login/Index"; });
        }

        // �˷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            // ��¼��֤
            app.UseAuthentication();
            // ��Ȩ��֤
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
