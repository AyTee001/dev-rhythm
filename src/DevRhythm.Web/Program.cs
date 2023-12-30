using DevRhythm.App.MappingProfiles;
using DevRhythm.Web.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;
using System.Reflection;
using System.Text.Json.Serialization;
using DevRhythm.Web.Middleware;
using DevRhythm.Web.Options;
using Microsoft.Extensions.Options;
using DevRhythm.Infrastructure.Hubs;

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
                })
                .AddDataAnnotationsLocalization(options => {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                    })
                .AddViewLocalization();

            builder.Services.AddDevRhythmContext(builder.Configuration.GetSection(DbConnectionOptions.Connections).Get<DbConnectionOptions>()!);

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(PostProfile)));

            builder.Services.AddSignalR()
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            builder.Services.AddCustomServices();

            builder.Services.AddIdentity();

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

            builder.Services.AddRazorPages();
            builder.Services.ConfigureRequestLocalizationOptions();

            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

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
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            app.UseAuthentication();
            app.UseMiddleware<UserProviderMiddleware>();

            app.UseAuthorization();

            app.UseLatestDevRhythmDbContext();

            app.MapHub<NotificationHub>("/notificationHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
