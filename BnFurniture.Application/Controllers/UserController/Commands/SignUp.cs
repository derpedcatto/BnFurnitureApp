﻿using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using BnFurniture.Shared.Utilities.Hash;
using System.Net;

namespace BnFurniture.Application.Controllers.UserController.Commands;

public sealed record SignUpCommand(UserSignUpDTO Dto);

public sealed class SignUpHandler : CommandHandler<SignUpCommand>
{
    private readonly UserSignUpDTOValidator _validator;
    private readonly IHashService _hashService;

    public SignUpHandler(
        UserSignUpDTOValidator validator,
        IHashService hashService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _hashService = hashService;
    }

    public override async Task<ApiCommandResponse> Handle(
        SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку.",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        await SaveUser(request.Dto, cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
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
            PhoneNumber = dto.MobileNumber,
            Password = _hashService.HashString(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            RegisteredAt = DateTime.Now
        }, cancellationToken);

        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);
    }
}