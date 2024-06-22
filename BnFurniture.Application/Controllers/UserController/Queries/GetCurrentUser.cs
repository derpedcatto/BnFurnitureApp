using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.UserController.Queries;

public sealed record GetCurrentUserQuery();

public sealed class GetCurrentUserResponse
{
    public UserDTO User { get; set; }

    public GetCurrentUserResponse(UserDTO user)
    {
        User = user;
    }
}

public sealed class GetCurrentUserHandler : QueryHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    public GetCurrentUserHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetCurrentUserResponse>> Handle(
        GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userIdClaim = HandlerContext.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid);

        if (userIdClaim == null)
        {
            return new ApiQueryResponse<GetCurrentUserResponse>
                (false, (int)HttpStatusCode.Unauthorized)
            {
                Message = "User is not authenticated",
                Data = null
            };
        }

        var userId = Guid.Parse(userIdClaim.Value);

        var user = await HandlerContext.DbContext.User
            .Where(u => u.Id == userId)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                RegisteredAt = u.RegisteredAt,
                LastLoginAt = u.LastLoginAt,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return new ApiQueryResponse<GetCurrentUserResponse>
                (false, (int)HttpStatusCode.NotFound)
            {
                Message = "User not found",
                Data = null
            };
        }

        var responseData = new GetCurrentUserResponse(user);
        return new ApiQueryResponse<GetCurrentUserResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Message = "User fetch success.",
            Data = responseData
        };
    }
}