using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BnFurniture.Domain.Responses;

namespace BnFurnitureApp.Server.Attributes;

public class AuthorizePermissionsAttribute : TypeFilterAttribute
{
    public AuthorizePermissionsAttribute(params string[] permissions) : base(typeof(AuthorizePermissionFilter))
    {
        Arguments = new object[] { permissions };
    }
}

public class AuthorizePermissionFilter : IAuthorizationFilter
{
    private readonly string[] _permissions;

    public AuthorizePermissionFilter(string[] permissions)
    {
        _permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        var forbiddenResponse = new ApiBaseResponse(false, 403)
        {
            Message = $"User does not have permission for this action."
        };

        if ( ! user.Identity.IsAuthenticated)
        {
            forbiddenResponse.Message = $"User is not authenticated.";
            context.Result = new JsonResult(forbiddenResponse) { StatusCode = forbiddenResponse.StatusCode };
        }

        var userPermissions = user.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        foreach (var permission in _permissions)
        {
            if ( ! userPermissions.Contains(permission))
            {
                forbiddenResponse.Message = $"User does not have sufficient permissions for this action.";
                context.Result = new JsonResult(forbiddenResponse) { StatusCode = forbiddenResponse.StatusCode };
            }
        }
    }
}