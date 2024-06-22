using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.UserController.Commands;

public sealed record UserLoginCommand(UserLoginDTO Dto);

public sealed class UserLoginHandler : CommandHandler<UserLoginCommand>
{
    private readonly UserLoginDTOValidator _validator;

    public UserLoginHandler(UserLoginDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(
        UserLoginCommand request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        if (!string.IsNullOrEmpty(HandlerContext.HttpContext.Session.GetString("AuthUserId")))
        {
            return new ApiCommandResponse
                (true, (int)HttpStatusCode.Conflict)
            {
                Message = "Користувач вже авторизований."
            };
        }

        var user = await HandlerContext.DbContext.User.FirstOrDefaultAsync(
            u => u.Email == request.Dto.EmailOrPhone || u.PhoneNumber == request.Dto.EmailOrPhone,
            cancellationToken);

        HandlerContext.HttpContext.Session.SetString("AuthUserId", user.Id.ToString());

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Користувач успішно авторизований."
        };
    }
}