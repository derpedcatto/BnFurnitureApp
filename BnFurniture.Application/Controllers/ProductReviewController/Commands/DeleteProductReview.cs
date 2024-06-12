using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.Commands
{
    public sealed record DeleteProductReviewCommand(Guid Id);

    public sealed class DeleteProductReviewHandler : CommandHandler<DeleteProductReviewCommand>
    {
        public DeleteProductReviewHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteProductReviewCommand command, CancellationToken cancellationToken)
        {
            var dbContext = HandlerContext.DbContext;
            var productReview = await dbContext.ProductReview.FindAsync(new object[] { command.Id }, cancellationToken);

            if (productReview == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Product review not found."
                };
            }

            dbContext.ProductReview.Remove(productReview);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Product review deleted successfully."
            };
        }
    }
}
