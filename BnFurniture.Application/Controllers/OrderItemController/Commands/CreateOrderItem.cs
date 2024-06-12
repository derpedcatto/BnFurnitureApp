using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderItem.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BnFurniture.Application.Controllers.OrderItem.Commands
{
    public sealed record CreateOrderItemCommand(CreateOrderItemDTO dto);

    public sealed class CreateOrderItemHandler : CommandHandler<CreateOrderItemCommand>
    {
        private readonly CreateOrderItemDTOValidator _validator;

        public CreateOrderItemHandler(CreateOrderItemDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateOrderItemCommand command, CancellationToken cancellationToken)
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

            var neworderItem = new Domain.Entities.OrderItem()
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                ArticleId = dto.ArticleId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                Discount = dto.Discount
            };

            await HandlerContext.DbContext.OrderItem.AddAsync(neworderItem, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);


            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "OrderItem created successfully."
            };
        }
    }
}
