using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController.Commands
{
    public sealed record UpdateCharacteristicCommand(UpdateCharacteristicDTO Dto);

    public sealed class UpdateCharacteristicHandler : CommandHandler<UpdateCharacteristicCommand>
    {
        private readonly UpdateCharacteristicDTOValidator _validator;

        public UpdateCharacteristicHandler(IHandlerContext context, ApplicationDbContext dbContext) : base(context)
        {
            _validator = new UpdateCharacteristicDTOValidator(dbContext);
        }

        public override async Task<ApiCommandResponse> Handle(UpdateCharacteristicCommand request, CancellationToken cancellationToken)
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
}
