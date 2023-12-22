using DevRhythm.App.Interfaces;
using DevRhythm.App.Services;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.Interfaces;
using DevRhythm.Shared.Providers;
using DevRhythm.Web.Options;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDevRhythmContext(this IServiceCollection services, DbConnectionOptions dbOptions) 
        {
            services.AddDbContext<DevRhythmDbContext>(options =>
            options.UseSqlServer(
                dbOptions.DevRhythmDbConnection,
                opt => opt.MigrationsAssembly(typeof(DevRhythmDbContext).Assembly.GetName().Name))
            );
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
        }
    }
}
