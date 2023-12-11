using DevRhythm.Infrastructure.Data.Configs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevRhythm.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }
    }
}
