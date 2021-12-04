using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyDemo3_Dapper;

namespace StudyDemo3_Dapper1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnOptions.ConnectionString = "Data Source=localhost;database=LTE;uid=SA;pwd=123456;";
            // 注册服务器端缓存服务
            services.AddResponseCaching();
            // 跨域
            services.AddCors(ac => ac.AddPolicy("any", ap => ap.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())); 
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // 使用服务器端缓存
            app.UseResponseCaching();
            app.UseAuthorization();
            // 使用跨域
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
