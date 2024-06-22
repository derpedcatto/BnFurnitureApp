using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;

public sealed record CreateCharacteristicCommand(CreateCharacteristicDTO Dto);

public sealed class CreateCharacteristicHandler : CommandHandler<CreateCharacteristicCommand>
{
    private readonly CreateCharacteristicDTOValidator _validator;

    public CreateCharacteristicHandler(
        CreateCharacteristicDTOValidator validator,
        IHandlerContext context) : base(context) 
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(
        CreateCharacteristicCommand request, CancellationToken cancellationToken)
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

        var characteristic = new Characteristic
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            Slug = request.Dto.Slug,
            Priority = request.Dto.Priority
        };

        await HandlerContext.DbContext.Characteristic.AddAsync(characteristic, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
        {
            Message = "Characteristic created successfully." }
        ;
    }
}
