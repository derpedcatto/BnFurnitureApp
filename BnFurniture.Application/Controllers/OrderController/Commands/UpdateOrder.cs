using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.Order.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderController.Commands
{
    public sealed record UpdateOrderCommand(UpdateOrderDTO Dto);

    public sealed class UpdateOrderHandler : CommandHandler<UpdateOrderCommand>
    {
        private readonly UpdateOrderDTOValidator _validator;

        public UpdateOrderHandler(UpdateOrderDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Dto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var order = await HandlerContext.DbContext.Order
                .FirstOrDefaultAsync(o => o.Id == dto.Id, cancellationToken);

            if (order == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Order not found."
                };
            }

            order.UserId = dto.UserId;
            order.StatusId = dto.StatusId;
            order.CreatedAt = dto.CreatedAt;
            order.ModifiedAt = dto.ModifiedAt;

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Order updated successfully."
            };
        }
    }
}
