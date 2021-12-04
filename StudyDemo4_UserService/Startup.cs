using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudyDemo4_UserService.Utility;
using System;

namespace StudyDemo4_UserService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConsulClient>(r => new ConsulClient(rr =>
            {
                rr.Address = new Uri("http://localhost:8500");
            }));
            services.AddSwaggerGen(r => r.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "user api",
                Version = "v1"
            }));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConsulClient consul)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();


            app.UseConsul(Configuration, consul);
            app.UseSwagger();
            app.UseSwaggerUI(r => { r.SwaggerEndpoint("/swagger/v1/swagger.json", "user api"); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
