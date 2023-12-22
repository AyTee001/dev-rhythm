using DevRhythm.App.MappingProfiles;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;
using System.Reflection;
using System.Text.Json.Serialization;
using DevRhythm.App.Services;
using DevRhythm.Shared.Interfaces;
using DevRhythm.Web.Middleware;
using DevRhythm.Shared.Providers;

namespace DevRhythm.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddScoped<IUserInfoProvider, UserInfoProvider>();

            builder.Services.AddDevRhythmContext(builder.Configuration);
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(PostProfile)));
            builder.Services.AddCustomServices();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext!);

            builder.Services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<DevRhythmDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<DevRhythmUserManager>()
                .AddSignInManager<SignInManager<User>>();

            builder.Services.AddScoped(_ => new Mock<IEmailSender>().Object);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Post/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAnyOrigin");

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();


            app.UseAuthentication();
            app.UseMiddleware<UserProviderMiddleware>();

            app.UseAuthorization();

            app.UseLatestDevRhythmDbContext();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
