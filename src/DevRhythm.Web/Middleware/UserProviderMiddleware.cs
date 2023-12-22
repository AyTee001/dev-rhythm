using DevRhythm.Shared.Interfaces;
using System.Security.Claims;

namespace DevRhythm.Web.Middleware
{
    public class UserProviderMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IUserInfoProvider userUidProvider)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is not null && long.TryParse(userId, out long longUserId))
            {
                userUidProvider.SetUserUid(longUserId);
            }

            await _next.Invoke(context);
        }
    }
}
