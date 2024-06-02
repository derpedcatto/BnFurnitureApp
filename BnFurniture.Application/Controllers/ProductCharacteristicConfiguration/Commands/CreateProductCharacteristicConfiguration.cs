using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductCharacteristicConfiguration.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands
{
    public sealed record CreateProductCharacteristicConfigurationCommand(CreateProductCharacteristicConfigurationDTO Dto);

    public sealed class CreateProductCharacteristicConfigurationHandler : CommandHandler<CreateProductCharacteristicConfigurationCommand>
    {
        public CreateProductCharacteristicConfigurationHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(CreateProductCharacteristicConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = new BnFurniture.Domain.Entities.ProductCharacteristicConfiguration
            {
                Id = Guid.NewGuid(),
                ArticleId = request.Dto.ArticleId,
                CharacteristicId = request.Dto.CharacteristicId,
                CharacteristicValueId = request.Dto.CharacteristicValueId
            };

            await HandlerContext.DbContext.ProductCharacteristicConfiguration.AddAsync(configuration, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 201) { Message = "ProductCharacteristicConfiguration created successfully." };
        }
    }
}
