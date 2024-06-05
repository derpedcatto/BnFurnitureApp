using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.Commands;

public sealed record DeleteCharacteristicValueCommand(Guid Id);

public sealed class DeleteCharacteristicValueHandler : CommandHandler<DeleteCharacteristicValueCommand>
{
    public DeleteCharacteristicValueHandler(IHandlerContext context)
        : base(context)
    {

    }

    public override async Task<ApiCommandResponse> Handle(DeleteCharacteristicValueCommand request, CancellationToken cancellationToken)
    {
        var characteristicValue = await HandlerContext.DbContext.CharacteristicValue
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (characteristicValue == null)
        {
            return new ApiCommandResponse(false, 404)
            {
                Message = "Characteristic value not found."
            };
        }

        var isUsedInProductConfiguration = await HandlerContext.DbContext.ProductCharacteristicConfiguration
            .AnyAsync(pcc => pcc.CharacteristicValueId == request.Id, cancellationToken);

        if (isUsedInProductConfiguration)
        {
            return new ApiCommandResponse(false, 400)
            {
                Message = "Cannot delete characteristic value as it is used in product characteristic configurations."
            };
        }

        HandlerContext.DbContext.CharacteristicValue.Remove(characteristicValue);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, 200)
        {
            Message = "Characteristic value deleted successfully."
        };
    }
}
