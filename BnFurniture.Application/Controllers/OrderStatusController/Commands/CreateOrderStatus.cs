using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderStatusController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.Commands
{
    public sealed record CreateOrderStatusCommand(CreateOrderStatusDTO Dto);

    public sealed class CreateOrderStatusHandler : CommandHandler<CreateOrderStatusCommand>
    {
        private readonly CreateOrderStatusDTOValidator _validator;

        public CreateOrderStatusHandler(IHandlerContext context, CreateOrderStatusDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Dto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Валідація не пройшла перевірку",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var newOrderStatus = new OrderStatus
            {
                Id = dto.Id,
                Name = dto.Name
            };

            await HandlerContext.DbContext.OrderStatus.AddAsync(newOrderStatus, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
            {
                Message = "OrderStatus created successfully."
            };
        }
    }
}
