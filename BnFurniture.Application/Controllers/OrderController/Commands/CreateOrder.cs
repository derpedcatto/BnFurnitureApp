using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.Order.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderItem.Commands
{
    public sealed record CreateOrderCommand(CreateOrderDTO dto);

    public sealed class CreateOrderHandler : CommandHandler<CreateOrderCommand>
    {
        private readonly CreateOrderDTOValidator _validator;

        public CreateOrderHandler(CreateOrderDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var dto = command.dto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var newOrder = new Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                StatusId = dto.StatusId,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt
            };

            await HandlerContext.DbContext.Order.AddAsync(newOrder, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Order created successfully."
            };
        }
    }
}
