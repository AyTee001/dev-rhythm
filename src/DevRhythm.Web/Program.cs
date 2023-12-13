using DevRhythm.App.Interfaces;
using DevRhythm.App.MappingProfiles;
using DevRhythm.App.Services;
using DevRhythm.Web.Extensions;
using System.Reflection;

namespace DevRhythm.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDevRhythmContext(builder.Configuration);
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(PostProfile)));
            builder.Services.AddCustomServices();

            var app = builder.Build();

            app.UseLatestDevRhythmDbContext();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Post/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
