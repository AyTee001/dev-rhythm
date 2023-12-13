using DevRhythm.Core.Entities;

namespace DevRhythm.Infrastructure.Data.SeedDefaults
{
    internal class SeedConstants
    {
        internal static readonly List<Tag> SeedTags = [
            new Tag("javascript") { Id = 1 },
            new Tag("python") { Id = 2 },
            new Tag("java") { Id = 3 },
            new Tag("csharp") { Id = 4 },
            new Tag("php") { Id = 5 },
            new Tag("ruby") { Id = 6 },
            new Tag("typescript") { Id = 7 },
            new Tag("go") { Id = 8 },
            new Tag("swift") { Id = 9 },
            new Tag("kotlin") { Id = 10 },
            new Tag("rust") { Id = 11 },
            new Tag("dart") { Id = 12 },
            new Tag("html") { Id = 13 },
            new Tag("css") { Id = 14 },
            new Tag("frontend") { Id = 15 },
            new Tag("backend") { Id = 16 },
            new Tag("fullstack") { Id = 17 },
            new Tag("react") { Id = 18 },
            new Tag("angular") { Id = 19 },
            new Tag("vuejs") { Id = 20 }
        ];

        internal static readonly string DefaultImagePath = "/images/account-icon-user-icon-vector-graphics_292645-552.jpg";
    }
}
