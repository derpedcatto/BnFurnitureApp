using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.UserController.Queries;

public sealed record GetUserByIdQuery(Guid userId);

public sealed class GetUserByIdResponse
{
    public ResponseUserDTO User { get; set; }

    public GetUserByIdResponse(ResponseUserDTO user)
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

    public override async Task<ApiQueryResponse<GetUserByIdResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await HandlerContext.DbContext.User
            .Where(u => u.Id == query.userId)
            .Select(u => new ResponseUserDTO
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
            return new ApiQueryResponse<GetUserByIdResponse>(false, (int)HttpStatusCode.NotFound)
            {
                Message = "User ID not found in database",
                Errors = new() { ["userId"] = ["User ID not found in database"] },
                Data = null
            };
        }

        var responseData = new GetUserByIdResponse(user);
        return new ApiQueryResponse<GetUserByIdResponse>(true, (int)HttpStatusCode.OK)
        {
            Message = $"User fetch success.",
            Data = responseData
        };
    }
}