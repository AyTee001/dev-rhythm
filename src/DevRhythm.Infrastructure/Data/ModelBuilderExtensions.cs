using Bogus;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data.Configs;
using DevRhythm.Infrastructure.Data.SeedDefaults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }

        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Tag>().HasData(SeedConstants.SeedTags);

            var users = GetUsers();
            var posts = GetPosts(users);
            var postTags = GetPostTags(posts);
            var comments = GetComments(posts, users);
            var replies = GetReplies(comments, users);
            var identityUsers = GetIdentityUsers(users);

            builder.Entity<DevRhythmUser>().HasData(users);

            builder.Entity<Post>().HasData(posts);

            builder.Entity<PostTag>().HasData(postTags);

            builder.Entity<Comment>().HasData(comments);

            builder.Entity<Reply>().HasData(replies);

            builder.Entity<DevRhythmIdentityUser>().HasData(identityUsers);

        }

        private static List<DevRhythmUser> GetUsers(int count = SeedSettings.UserCount)
        {
            Faker.GlobalUniqueIndex = 0;

            return new Faker<DevRhythmUser>()
                .CustomInstantiator(f => new DevRhythmUser(f.Person.FirstName, f.Person.LastName, f.Lorem.Sentence(), SeedConstants.DefaultImagePath))
                .UseSeed(SeedSettings.UserSeed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.BirthDate, f => f.Person.DateOfBirth)
                .RuleFor(e => e.PostCount, f => f.Random.Number(0, 20))
                .RuleFor(e => e.Reputation, f => f.Random.Number(0, 10000000))
                .RuleFor(e => e.ThreadCount, f => f.Random.Number(1, 20))
                .RuleFor(e => e.PostCount, f => f.Random.Number(1, 20))
                .RuleFor(e => e.RegisteredAt, f => f.Date.Between(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 12, 31, 0, 0, 0, DateTimeKind.Utc)))
                .Generate(count);
        }

        private static List<Post> GetPosts(List<DevRhythmUser> users, int count = SeedSettings.PostCount)
        {
            Faker.GlobalUniqueIndex = 0;

            return new Faker<Post>()
                .CustomInstantiator(f => new Post(f.Hacker.Phrase(), f.Lorem.Text()))
                .UseSeed(SeedSettings.PostSeed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.VoteResult, f => f.Random.Int(-10000, 10000))
                .RuleFor(e => e.CreatedBy, f => f.PickRandom(users).Id)
                .RuleFor(p => p.CreatedAt, (f, e) =>
                {
                    var author = users.Find(a => a.Id == e.CreatedBy)!;
                    return f.Date.Between(author.RegisteredAt, author.RegisteredAt.AddMonths(6));
                })
                .Generate(count);
        }

        private static List<PostTag> GetPostTags(List<Post> posts)
        {
            int count = posts.Count * 3;

            var postTags = new Faker<PostTag>()
                .RuleFor(e => e.PostId, f => f.PickRandom(posts).Id)
                .RuleFor(e => e.TagId, f => f.PickRandom(SeedConstants.SeedTags).Id)
                .Generate(count);

            return postTags.DistinctBy(c => new { c.PostId, c.TagId }).ToList();
        }

        private static List<Comment> GetComments(List<Post> posts, List<DevRhythmUser> users)
        {
            int count = posts.Count * 2;
            Faker.GlobalUniqueIndex = 0;

            return new Faker<Comment>()
                .UseSeed(SeedSettings.CommentSeed)
                .CustomInstantiator(f => new Comment(f.Lorem.Sentence()))
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.PostId, f => f.PickRandom(posts).Id)
                .RuleFor(e => e.CreatedBy, f => f.PickRandom(users).Id)
                .RuleFor(p => p.CreatedAt, (f, e) =>
                {
                    var post = posts.Find(a => a.Id == e.PostId)!;
                    return f.Date.Between(post.CreatedAt, DateTime.Today);
                })
                .Generate(count);

        }

        private static List<Reply> GetReplies(List<Comment> comments, List<DevRhythmUser> users)
        {
            int count = comments.Count * 2;
            Faker.GlobalUniqueIndex = 0;

            return new Faker<Reply>()
                .UseSeed(SeedSettings.ReplySeed)
                .CustomInstantiator(f => new Reply(f.Lorem.Sentence()))
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.CommentId, f => f.PickRandom(comments).Id)
                .RuleFor(e => e.CreatedBy, f => f.PickRandom(users).Id)
                .RuleFor(p => p.CreatedAt, (f, e) =>
                {
                    var post = comments.Find(a => a.Id == e.CommentId)!;
                    return f.Date.Between(post.CreatedAt, DateTime.Today);
                })
                .Generate(count);
        }

        private static List<DevRhythmIdentityUser> GetIdentityUsers(List<DevRhythmUser> users)
        {
            Faker.GlobalUniqueIndex = -1;

            var identityUsers = new Faker<DevRhythmIdentityUser>()
                .RuleFor(e => e.UserName, f => f.Internet.UserName())
                .RuleFor(e => e.PasswordHash, (f, e) => new PasswordHasher<IdentityUser>().HashPassword(e, "P@ssw0rd"))
                .RuleFor(e => e.Email, f => f.Internet.Email())
                .RuleFor(e => e.EmailConfirmed, f => true)
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.PhoneNumberConfirmed, f => true)
                .RuleFor(e => e.DevRhythmUserId, (f, e) =>
                {
                    var user = users[f.IndexGlobal];
                    return user.Id;
                })
                .Generate(users.Count);

            return identityUsers;
        }
    }
}
