using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.App.UserRegisterController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.App.UserRegisterController.Commands;

public sealed record LoginCommand(UserLoginDTO entityForm);

public sealed class LoginHandler : CommandHandler<LoginCommand>
{
    private readonly UserLoginDTOValidator _validator;

    public LoginHandler(UserLoginDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var dto = request.entityForm;
        var httpContext = HandlerContext.HttpContextAccessor.HttpContext;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        if (!string.IsNullOrEmpty(httpContext.Session.GetString("AuthUserId")))
        {
            return new ApiCommandResponse(true, (int)HttpStatusCode.Conflict)
            {
                Message = "Користувач вже авторизований."
            };
        }

        var user = await HandlerContext.DbContext.User.FirstOrDefaultAsync(
            u => u.Email == dto.EmailOrPhone || u.PhoneNumber == dto.EmailOrPhone,
            cancellationToken);

        httpContext.Session.SetString("AuthUserId", user.Id.ToString());

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Користувач успішно авторизований."
        };
    }
}