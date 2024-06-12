using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderItem.Commands
{
    public sealed record DeleteOrderItemCommand(Guid Id);

    public sealed class DeleteOrderItemHandler : CommandHandler<DeleteOrderItemCommand>
    {
        public DeleteOrderItemHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken)
        {
            var dbContext = HandlerContext.DbContext;
            var orderItem = await HandlerContext.DbContext.OrderItem.FindAsync(new object[] { command.Id }, cancellationToken);

            if (orderItem == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Order item not found."
                };
            }

            HandlerContext.DbContext.OrderItem.Remove(orderItem);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 200) { Message = "Order item deleted successfully." };
        }
    }
}
