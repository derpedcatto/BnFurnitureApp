using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands
{
    public sealed record DeleteCharacteristicCommand(Guid Id);

    public sealed class DeleteCharacteristicHandler : CommandHandler<DeleteCharacteristicCommand>
    {
        public DeleteCharacteristicHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristic = await HandlerContext.DbContext.Characteristic
                .Include(c => c.CharacteristicValues)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (characteristic == null)
            {
                return new ApiCommandResponse(false, 404) { Message = "Characteristic not found." };
            }

            // Check if the characteristic or its values are used in ProductCharacteristicConfiguration
            bool isCharacteristicUsed = await HandlerContext.DbContext.ProductCharacteristicConfiguration
                .AnyAsync(pcc => pcc.CharacteristicId == request.Id, cancellationToken);

            bool isCharacteristicValueUsed = await HandlerContext.DbContext.ProductCharacteristicConfiguration
                .AnyAsync(pcc => characteristic.CharacteristicValues.Select(cv => cv.Id).Contains(pcc.CharacteristicValueId), cancellationToken);

            if (isCharacteristicUsed || isCharacteristicValueUsed)
            {
                return new ApiCommandResponse(false, 400)
                {
                    Message = "Cannot delete characteristic or its values because they are used in ProductCharacteristicConfiguration."
                };
            }

            HandlerContext.DbContext.Characteristic.Remove(characteristic);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 200) { Message = "Characteristic and its values deleted successfully." };
        }
    }

}
