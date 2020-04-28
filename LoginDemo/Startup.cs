using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptchaService.Extensions;
using DataBase;
using DataBase.Dao;
using DataBase.Interface;
using LoginDemo.Help;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using UEditor.Core;

namespace LoginDemo
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
            //"server=134.175.147.106;userid=jingshen;pwd=HG8XpGtThkRk3EeE;port=3306;database=jingshen;sslmode=none;"
            services.AddDbContext<DataBase.Models.jingshenContext>(x => x.UseMySql(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddScoped<IHmAdPosition, HmAdPositionDao>();
            services.AddScoped<IHmAd, HmAdDao>();
            services.AddScoped<IHmAdmin, HmAdminDao>();
            services.AddScoped<IHmRole, HmRoleDao>();
            services.AddSingleton(new LocalStr(Configuration[HostDefaults.ContentRootKey]));//core 2.2 ·����bug,��Ҫ����ע��,��Ȼ��ָ��c�̵�iis
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddCaptchaService();//�����֤�����������ע��
            services.AddScoped<Log>();//��ӹ���Ա������־���������ע��
            services.AddScoped<UserLog>();//�����ͨ�û���־���������ע��
            services.AddUEditorService();//��Ӹ��ı��༭��������ע��
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.AccessDeniedPath=new PathString("/Error");
                x.LoginPath = new PathString("/Manager/BLogin");
                x.LogoutPath = new PathString("/Manager/BLogin");
            });
            services.AddMvc();
            services.AddMvc().AddRazorPagesOptions(x => {
                x.Conventions.AddPageRoute("/Home/home", "");
                x.Conventions.AuthorizeFolder("/Manager").AllowAnonymousToPage("/Manager/BLogin");
                
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //��Ӷ�image�ļ��Ķ�ȡ,��Ȼ���wwwroot����bug����
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
              System.IO.Path.Combine(Configuration[HostDefaults.ContentRootKey], "image")),
                RequestPath = "/image"
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x=> { x.MapRazorPages();x.MapControllers(); });
        }
    }
}
