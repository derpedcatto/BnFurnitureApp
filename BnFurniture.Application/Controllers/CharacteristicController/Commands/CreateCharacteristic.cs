using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands
{
    public sealed record CreateCharacteristicCommand(CreateCharacteristicDTO Dto);

    public sealed class CreateCharacteristicHandler : CommandHandler<CreateCharacteristicCommand>
    {
        public CreateCharacteristicHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristic = new Characteristic
            {
                Id = Guid.NewGuid(),
                Name = request.Dto.Name,
                Slug = request.Dto.Slug,
                Priority = request.Dto.Priority
            };

            await HandlerContext.DbContext.Characteristic.AddAsync(characteristic, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 201) { Message = "Characteristic created successfully." };
        }
    }
}
