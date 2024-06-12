using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.Commands
{
    public sealed record DeleteOrderStatusCommand(int Id);

    public sealed class DeleteOrderStatusHandler : CommandHandler<DeleteOrderStatusCommand>
    {
        public DeleteOrderStatusHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var dbContext = HandlerContext.DbContext;
            var orderStatus = await HandlerContext.DbContext.OrderStatus.FindAsync(new object[] { command.Id }, cancellationToken);

            if (orderStatus == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "OrderStatus item not found."
                };
            }

            HandlerContext.DbContext.OrderStatus.Remove(orderStatus);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 200) { Message = "Order item deleted successfully." };
        }
    }
}
