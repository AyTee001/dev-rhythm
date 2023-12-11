using DevRhythm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Web.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseLatestDevRhythmDbContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<DevRhythmDbContext>();

            context?.Database.Migrate();
        }
    }
}
