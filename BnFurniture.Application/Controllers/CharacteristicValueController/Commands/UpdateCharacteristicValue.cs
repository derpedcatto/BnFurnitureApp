using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.Commands
{
    public sealed record UpdateCharacteristicValueCommand(UpdateCharacteristicValueDTO Dto);

    public sealed class UpdateCharacteristicValueHandler : CommandHandler<UpdateCharacteristicValueCommand>
    {
        private readonly UpdateCharacteristicValueDTOValidator _validator;

        public UpdateCharacteristicValueHandler(IHandlerContext context) : base(context)
        {
            _validator = new UpdateCharacteristicValueDTOValidator(context.DbContext);
        }

        public override async Task<ApiCommandResponse> Handle(UpdateCharacteristicValueCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                     .GroupBy(e => e.PropertyName)
                     .ToDictionary(
                         g => g.Key,
                         g => g.Select(e => e.ErrorMessage).ToList()
                     );
                return new ApiCommandResponse(false, 400) { Message = "Validation failed.", Errors = errors };
            }

            var characteristicValue = await HandlerContext.DbContext.CharacteristicValue
                .FirstOrDefaultAsync(cv => cv.Id == request.Dto.Id, cancellationToken);

            if (characteristicValue == null)
            {
                return new ApiCommandResponse(false, 404) { Message = "Characteristic value not found." };
            }

            characteristicValue.CharacteristicId = request.Dto.CharacteristicId;
            characteristicValue.Value = request.Dto.Value;
            characteristicValue.Slug = request.Dto.Slug;
            characteristicValue.Priority = request.Dto.Priority;

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 200) { Message = "Characteristic value updated successfully." };
        }
    }
}
