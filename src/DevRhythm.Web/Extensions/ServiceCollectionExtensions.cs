using DevRhythm.App.Interfaces;
using DevRhythm.App.Services;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.Interfaces;
using DevRhythm.Shared.Providers;
using DevRhythm.Web.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DevRhythm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string _defaultCulture = "en";

        private static readonly List<CultureInfo> _supportedCultures =
            [
                new CultureInfo(_defaultCulture),
                new CultureInfo("uk")
            ];

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

        public static void ConfigureRequestLocalizationOptions(this IServiceCollection services)
        {

            services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture(_defaultCulture);
                options.SupportedCultures = _supportedCultures;
                options.SupportedUICultures = _supportedCultures;
            });
        }
    }
}
