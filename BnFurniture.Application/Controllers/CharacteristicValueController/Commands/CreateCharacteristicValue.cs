using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;

public sealed record CreateCharacteristicValueCommand(CreateCharacteristicValueDTO Dto);

public sealed class CreateCharacteristicValueHandler : CommandHandler<CreateCharacteristicValueCommand>
{
    private readonly CreateCharacteristicValueDTOValidator _validator;

    public CreateCharacteristicValueHandler(CreateCharacteristicValueDTOValidator validator,
        IHandlerContext context) : base(context) 
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateCharacteristicValueCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var characteristicValue = new CharacteristicValue
        {
            Id = Guid.NewGuid(),
            CharacteristicId = request.Dto.CharacteristicId,
            Value = request.Dto.Value,
            Slug = request.Dto.Slug,
            Priority = request.Dto.Priority
        };

        await HandlerContext.DbContext.CharacteristicValue.AddAsync(characteristicValue, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, 201) { Message = "Characteristic value created successfully." };
    }
}