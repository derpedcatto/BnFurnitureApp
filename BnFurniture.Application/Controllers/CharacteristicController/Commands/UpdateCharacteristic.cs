using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;

public sealed record UpdateCharacteristicCommand(UpdateCharacteristicDTO Dto);

public sealed class UpdateCharacteristicHandler : CommandHandler<UpdateCharacteristicCommand>
{
    private readonly UpdateCharacteristicDTOValidator _validator;

    public UpdateCharacteristicHandler(UpdateCharacteristicDTOValidator validator, 
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(UpdateCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            { 
                Message = "Валідація не пройшла перевірку.",
                Errors = validationResult.ToApiResponseErrors() 
            };
        }

        var characteristic = await HandlerContext.DbContext.Characteristic
            .FirstOrDefaultAsync(c => c.Id == request.Dto.Id, cancellationToken);

        if (characteristic == null)
        {
            return new ApiCommandResponse(false, 404) { Message = "Characteristic not found." };
        }

        characteristic.Name = request.Dto.Name;
        characteristic.Slug = request.Dto.Slug;
        characteristic.Priority = request.Dto.Priority;

        HandlerContext.DbContext.Characteristic.Update(characteristic);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, 200) { Message = "Characteristic updated successfully." };
    }
}
