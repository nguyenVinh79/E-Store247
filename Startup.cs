using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ECommerce.Project.Models;
using ECommerce.Project.Service;
using ECommerce.Project.Settings;
using System;

namespace ECommerce.Project
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
            services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);  //Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 8.1.0

            //add Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation -Version 3.1.9
            services.AddRazorPages().AddRazorRuntimeCompilation();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataShopContext>(
                options => options.UseSqlServer(connectionString)
            );

            #region Session configuration

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            #endregion Session configuration

            #region Configure Identity

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DataShopContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                // Lockout -  user lock
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            #endregion Configure Identity

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddTransient<IMailService, MailService>();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Configure session

            app.UseSession();

            #endregion Configure session

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "search",
                  template: "product-search",
                  defaults: new { controller = "Product", action = "Search" }
              );
                routes.MapRoute(
                  name: "product-detail",
                  template: "{title}-{id}",
                  defaults: new { controller = "Product", action = "Detail" }
              );
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}