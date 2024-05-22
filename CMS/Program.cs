using CMS.Controllers;
using CMS.DbEntity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>

            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;    ///   


                // User settings.

                options.User.AllowedUserNameCharacters =

                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });








            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               // Cookie settings
               options.Cookie.HttpOnly = true; //you cannot modify or override it.
               options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

               options.LoginPath = "/Account/Login";
               options.AccessDeniedPath = "/Account/AccessDenied";
               options.SlidingExpiration = true;

           });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowBranchManager", policy => policy.RequireClaim("Designation", "Branch Manager"));
            });






            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IEmailServices, EmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
