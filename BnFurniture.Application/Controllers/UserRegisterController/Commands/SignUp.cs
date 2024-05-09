using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserRegisterController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using BnFurniture.Shared.Utilities.Hash;
using System.Net;

namespace BnFurniture.Application.Controllers.UserRegisterController.Commands;

public sealed record SignUpCommand(UserSignUpDTO entityForm);

public sealed class SignUpHandler : CommandHandler<SignUpCommand>
{
    private readonly UserSignUpDTOValidator _validator;
    private readonly IHashService _hashService;

    public SignUpHandler(UserSignUpDTOValidator validator, IHashService hashService,
        IHandlerContext context): base(context)
    {
        _validator = validator;
        _hashService = hashService;
    }

    public override async Task<ApiCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var dto = request.entityForm;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку.",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        await SaveUser(dto, cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Реєстрація успішна."
        };
    }


    private async Task SaveUser(UserSignUpDTO dto, CancellationToken cancellationToken)
    {
        await HandlerContext.DbContext.AddAsync(new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Phonenumber = dto.MobileNumber,
            Password = _hashService.HashString(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            Created = DateTime.Now
        }, cancellationToken);

        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);
    }
}