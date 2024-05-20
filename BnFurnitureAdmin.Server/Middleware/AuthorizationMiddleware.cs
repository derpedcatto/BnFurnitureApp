using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BnFurnitureAdmin.Server.Middleware;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.Sid);
            if (userIdClaim != null)
            {
                var userId = Guid.Parse(userIdClaim.Value);
                var userRoles = await dbContext.User_UserRole
                    .Where(uur => uur.UserId == userId)
                    .Include(uur => uur.UserRole)
                    .ThenInclude(ur => ur.UserRole_Permissions)
                    .ThenInclude(urp => urp.Permission)
                    .ToListAsync();

                var permissions = userRoles
                    .SelectMany(uur => uur.UserRole.UserRole_Permissions.Select(urp => urp.Permission.Name))
                    .Distinct()
                    .ToList();

                // Add permissions to user claims
                foreach (var permission in permissions)
                {
                    context.User.AddIdentity(new ClaimsIdentity(new[] { new Claim("Permission", permission) }));
                }
            }
        }

        await _next(context);
    }
}
