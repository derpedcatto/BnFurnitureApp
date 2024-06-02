using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands
{
    public sealed record CreateCharacteristicValueCommand(CreateCharacteristicValueDTO Dto);

    public sealed class CreateCharacteristicValueHandler : CommandHandler<CreateCharacteristicValueCommand>
    {
        public CreateCharacteristicValueHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(CreateCharacteristicValueCommand request, CancellationToken cancellationToken)
        {
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
}