using DevRhythm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDevRhythmContext(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DevRhythmDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DevRhythmDbConnection"),
                opt => opt.MigrationsAssembly(typeof(DevRhythmDbContext).Assembly.GetName().Name))
            );
        }
    }
}
