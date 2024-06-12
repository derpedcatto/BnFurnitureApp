using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderStatusController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.Commands
{
    public sealed record UpdateOrderStatusCommand(UpdateOrderStatusDTO Dto);

    public sealed class UpdateOrderStatusHandler : CommandHandler<UpdateOrderStatusCommand>
    {
        private readonly UpdateOrderStatusDTOValidator _validator;
        public UpdateOrderStatusHandler(UpdateOrderStatusDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var dto = command.Dto;
            var validationResult = await _validator.ValidateAsync(dto, ct);

            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var orderStatus = await HandlerContext.DbContext.OrderStatus
                .FirstOrDefaultAsync(os => os.Id == dto.Id, ct);

            if (orderStatus == null) // Здесь проверяем, что запись не найдена
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "OrderStatus not found."
                };
            }

            orderStatus.Name = dto.Name;

            await HandlerContext.DbContext.SaveChangesAsync(ct);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Order status updated successfully."
            };
        }
    }
}
