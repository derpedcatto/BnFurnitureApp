using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using BnFurniture.Shared.Utilities.Email;
using BnFurniture.Shared.Utilities.Hash;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace BnFurniture.Application.Controllers.UserController.Commands;

public sealed record PassForgotCommand(UserPassForgotDTO Dto);

public sealed class PassForgotHandler : CommandHandler<PassForgotCommand>
{
    private readonly PassForgotDTOValidator _validator;
    private readonly IHashService _hashService;
    private readonly IEmailService _emailService;

    public PassForgotHandler(
        PassForgotDTOValidator validator,
        IHashService hashService,
        IEmailService emailService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _hashService = hashService;
        _emailService = emailService;
    }


    public override async Task<ApiCommandResponse> Handle(
        PassForgotCommand request, CancellationToken cancellationToken = default)
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

        var newPass = GeneratePassword();
        var user = await HandlerContext.DbContext.User.FirstOrDefaultAsync(
            u => u.Email == request.Dto.EmailOrPhone || u.PhoneNumber == request.Dto.EmailOrPhone,
            cancellationToken);

        var result = await _emailService.SendNewPasswordEmailAsync(
            user.Email,
            newPass,
            $"{user.FirstName} {user.LastName}",
            cancellationToken);

        if (!result.IsSuccess)
        {
            return new ApiCommandResponse
                (false, result.StatusCode)
            {
                Message = result.Message,
            };
        }

        user.Password = _hashService.HashString(newPass);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Новий пароль успішно сгенеровано, і лист з новим паролем відправлен на пошту користувача."
        };
    }


    private string GeneratePassword(int length = 7)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+{}[]|\\:;\"',./<>?`~";
        var sb = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int index = Random.Shared.Next(validChars.Length);
            sb.Append(validChars[index]);
        }

        return sb.ToString();
    }
}