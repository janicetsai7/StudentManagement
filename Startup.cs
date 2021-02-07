using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //使用sqlserver
            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
                );
            
            
            //返回xml格式
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc(options => options.EnableEndpointRouting=false);
            //services.AddMvcCore(options => options.EnableEndpointRouting = false);

            //配置綁定IStudentRepository, MockStudentRepository
            //services.AddSingleton<IStudentRepository, MockStudentRepository>();
            //如果是數據庫DataStudentRepository
            //services.AddSingleton<IStudentRepository, DataStudentRepository>();
            //services.AddScoped<IStudentRepository, MockStudentRepository>();
            //AddTransient同一個http請求或橫跨多個不同http請求，都為「新實例」也就是會刷新
            //services.AddTransient<IStudentRepository, MockStudentRepository>();
            services.AddScoped<IStudentRepository, SQLStudentRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                //developerExceptionPageOptions.SourceCodeLineCount = 100;
                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
                
               
                app.UseDeveloperExceptionPage();
            }
            //非開發環境處理的異常
            else
            {
               //以下為500 
                app.UseExceptionHandler("/Error");
                //以下為404錯誤頁面 頁面不存在
                  //app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");                
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            //else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            //{
            //    app.UseDeveloperExceptionPage("/Error");
            //}
            //開發環境(development) 集成環境(integration) 測試環境(testing) QA驗證  模擬環境(staging) 生產環境(production)
            //UAT 用戶驗證測試環境

            //app.Use(async (context,next) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    logger.LogInformation("M1:傳入請求");
            //    await next();
            //    logger.LogInformation("M1:傳出請求");
            //});

            //app.Use(async (context, next) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    logger.LogInformation("M2:傳入請求");
            //    await next();
            //    logger.LogInformation("M2:傳出請求");
            //});            

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("one.html");

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("one.html");

            //app.UseFileServer();
            //app.UseDirectoryBrowser();


            ////添加默認文件中間件 index.html index.htm  默認default.html default.htm
            //app.UseDefaultFiles(fileServerOptions);

            ////靜態文件中間件            

            app.UseStaticFiles();
            //一定要加在UseStaticFiles()後面
            
            //app.UseMvcWithDefaultRoute();
            //將原始路由註釋掉，改由下列指定
            
            //路由模板
            app.UseMvc(routes =>
            {
                //使用者未指定路由，以以下預設的路徑為主
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                /* 
                 * 路由模板如設了xxx/{con....},使用TagHelper(<a asp-controller="home" asp-action="details" 
                 * asp-route-id=@student.Id")則能修正錯誤，導向正常的路徑。參見index.cxhtml 第37集
                routes.MapRoute("default", "ltm/{controller=Home}/{action=Index}/{id?}"); 
                */
            });
            //app.UseMvc();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        //var configVal= _configuration["MyKey"];
            //        await context.Response.WriteAsync("UseEndpoints Hotsting Environment:" + env.EnvironmentName);
            //        //logger.LogInformation("M3:處理請求，生成響應");
            //    });
            //});

            //以下為預設，當路由不正確，預下導航至下列情況
            //app.Run(async (context) =>
            //{
            //    //context.Response.ContentType = "text/plain;charset=uft-8";
            //    //await context.Response.WriteAsync("Run Hosting Environment : " + env.EnvironmentName);
            //    //await context.Response.WriteAsync("Hello RUN WORLD");

            //    //throw new Exception("你的請求在管道中出現異常，請檢查");
            //});


        }

    }
}
