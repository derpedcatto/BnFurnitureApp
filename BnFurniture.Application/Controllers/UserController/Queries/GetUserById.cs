using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.UserController.Queries;

public sealed record GetUserByIdQuery(Guid UserId);

public sealed class GetUserByIdResponse
{
    public UserDTO User { get; set; }

    public GetUserByIdResponse(UserDTO user)
    {
        User = user;
    }
}

public sealed class GetUserByIdHandler : QueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public GetUserByIdHandler(
        IHandlerContext context) : base(context)
    {
        
    }

    public override async Task<ApiQueryResponse<GetUserByIdResponse>> Handle(
        GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await HandlerContext.DbContext.User
            .Where(u => u.Id == request.UserId)
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
            return new ApiQueryResponse<GetUserByIdResponse>
                (false, (int)HttpStatusCode.NotFound)
            {
                Message = "User ID not found in database",
                Errors = new() { ["userId"] = ["User ID not found in database"] },
                Data = null
            };
        }

        var responseData = new GetUserByIdResponse(user);
        return new ApiQueryResponse<GetUserByIdResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Message = $"User fetch success.",
            Data = responseData
        };
    }
}