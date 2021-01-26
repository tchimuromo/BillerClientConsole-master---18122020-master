using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillerClientConsole.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BillerClientConsole
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
            services.AddSession();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); // <= Add this for pagination
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();






            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();


            //ironpdf
            IronPdf.License.LicenseKey = "IRONPDF-17941296BC-181444-E7D0E8-C74E6C9B01-A2555F9F-UExAAF2885A4F327D8-DELPLOYMENT.TRIAL.EXPIRES.18.AUG.2019";

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzAyMjc1QDMxMzgyZTMxMmUzMG51ZitKTHRHc2Z4aFY0U3NGelJGRk5jYWxnZzN0QXRJYjZaclZ0dktmdFE9");



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<QueryDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("dbConn1")));

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("dbConn1")));

            services.AddDefaultIdentity<IdentityUser>()
               .AddDefaultUI(UIFramework.Bootstrap4)
               .AddEntityFrameworkStores<ApplicationDbContext>();




            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            opt =>
            {
                //configure your other properties
                opt.LoginPath = "/Auth/Login";
                opt.AccessDeniedPath = "/Auth/Login";
            });

            services.Configure<PasswordHasherOptions>(options =>
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
            );


            services.AddMvc(opt => opt.EnableEndpointRouting = false)
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();//this line must appear first
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("CompanyReg");
            });
        }
    }
}
