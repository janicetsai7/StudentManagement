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
            //�ϥ�sqlserver
            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
                );
            
            
            //��^xml�榡
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc(options => options.EnableEndpointRouting=false);
            //services.AddMvcCore(options => options.EnableEndpointRouting = false);

            //�t�m�j�wIStudentRepository, MockStudentRepository
            //services.AddSingleton<IStudentRepository, MockStudentRepository>();
            //�p�G�O�ƾڮwDataStudentRepository
            //services.AddSingleton<IStudentRepository, DataStudentRepository>();
            //services.AddScoped<IStudentRepository, MockStudentRepository>();
            //AddTransient�P�@��http�ШD�ξ��h�Ӥ��Phttp�ШD�A�����u�s��ҡv�]�N�O�|��s
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
            //�D�}�o���ҳB�z�����`
            else
            {
               //�H�U��500 
                app.UseExceptionHandler("/Error");
                //�H�U��404���~���� �������s�b
                  //app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");                
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            //else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            //{
            //    app.UseDeveloperExceptionPage("/Error");
            //}
            //�}�o����(development) ��������(integration) ��������(testing) QA����  ��������(staging) �Ͳ�����(production)
            //UAT �Τ����Ҵ�������

            //app.Use(async (context,next) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    logger.LogInformation("M1:�ǤJ�ШD");
            //    await next();
            //    logger.LogInformation("M1:�ǥX�ШD");
            //});

            //app.Use(async (context, next) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    logger.LogInformation("M2:�ǤJ�ШD");
            //    await next();
            //    logger.LogInformation("M2:�ǥX�ШD");
            //});            

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("one.html");

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("one.html");

            //app.UseFileServer();
            //app.UseDirectoryBrowser();


            ////�K�[�q�{��󤤶��� index.html index.htm  �q�{default.html default.htm
            //app.UseDefaultFiles(fileServerOptions);

            ////�R�A��󤤶���            

            app.UseStaticFiles();
            //�@�w�n�[�bUseStaticFiles()�᭱
            
            //app.UseMvcWithDefaultRoute();
            //�N��l���ѵ������A��ѤU�C���w
            
            //���ѼҪO
            app.UseMvc(routes =>
            {
                //�ϥΪ̥����w���ѡA�H�H�U�w�]�����|���D
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                /* 
                 * ���ѼҪO�p�]�Fxxx/{con....},�ϥ�TagHelper(<a asp-controller="home" asp-action="details" 
                 * asp-route-id=@student.Id")�h��ץ����~�A�ɦV���`�����|�C�Ѩ�index.cxhtml ��37��
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
            //        //logger.LogInformation("M3:�B�z�ШD�A�ͦ��T��");
            //    });
            //});

            //�H�U���w�]�A����Ѥ����T�A�w�U�ɯ�ܤU�C���p
            //app.Run(async (context) =>
            //{
            //    //context.Response.ContentType = "text/plain;charset=uft-8";
            //    //await context.Response.WriteAsync("Run Hosting Environment : " + env.EnvironmentName);
            //    //await context.Response.WriteAsync("Hello RUN WORLD");

            //    //throw new Exception("�A���ШD�b�޹D���X�{���`�A���ˬd");
            //});


        }

    }
}
