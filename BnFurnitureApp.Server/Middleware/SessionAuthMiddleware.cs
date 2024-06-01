using BnFurniture.Infrastructure.Persistence;
using System.Security.Claims;

namespace BnFurnitureAdmin.Server.Middleware;

public class SessionAuthMiddleware
{
    private readonly RequestDelegate _next;

    public SessionAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        if (context.Session.Keys.Contains("AuthUserId"))
        {
            var userAuthId = context.Session.GetString("AuthUserId");
            if (userAuthId != null)
            {
                Guid userId;
                if (Guid.TryParse(userAuthId, out userId))
                {
                    var user = await dbContext.User.FindAsync(userId);
                    if (user != null)
                    {
                        Claim[] claims = 
                        [
                            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        ];
                        context.User =
                            new ClaimsPrincipal(
                                new ClaimsIdentity(
                                    claims, "SessionAuth"));
                    }
                    else
                    {
                        context.Session.Remove("AuthUserId");
                        context.Response.Cookies.Delete("AuthUserId");
                    }
                }
                else
                {
                    // Handle the case where the session value is not a valid Guid
                    // This could involve logging the error or redirecting the user

                    // return ApiResponse with Invalid code?
                }
            }
            else
            {
                // Handle the case where the session value is null
                // This could involve logging the error or redirecting the user
            }
        }

        await _next(context);
    }
}