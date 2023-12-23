using DevRhythm.App.Interfaces;
using DevRhythm.App.Services;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.Interfaces;
using DevRhythm.Shared.Providers;
using DevRhythm.Web.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string[] _configureCultureOptions = ["en", "ua"];
        private static readonly string _defaultCulture = "en";

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

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<DevRhythmDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<DevRhythmUserManager>()
                .AddSignInManager<SignInManager<User>>();
        }


        public static void ConfigureRequestLocalization(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(_defaultCulture);
                options.AddSupportedCultures(_configureCultureOptions);
                options.AddSupportedUICultures(_configureCultureOptions);
            });
        }
    }
}
