using hzerpdemo.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace hzerpdemo
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
            var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
            services.AddSingleton(new InnovatorFactory(config));
            services.AddControllersWithViews();
            services.AddControllers()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.MaxDepth = 200;
                  // 允许JSON属性名称和C#属性名称不匹配，自动进行大小写转换
                  options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                  // 忽略在JSON中存在但在C#对象中不存在的属性
                  options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;

                  // 忽略无法序列化的属性
                  options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                  // 允许读取注释
                  options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;

                  // 允许写入小数点后多余的0
                  options.JsonSerializerOptions.WriteIndented = true;

              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            hsLog.ILogger.Instance().Register();
        }
    }
}
