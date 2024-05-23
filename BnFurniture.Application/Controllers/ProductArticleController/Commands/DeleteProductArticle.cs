﻿using System;
using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductArticleController.Commands
{
    public sealed record DeleteProductArticleCommand(Guid ArticleId);

    public sealed class DeleteProductArticleHandler : CommandHandler<DeleteProductArticleCommand>
    {
        public DeleteProductArticleHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteProductArticleCommand command, CancellationToken cancellationToken)
        {
            var dbContext = HandlerContext.DbContext;
            var productArticle = await dbContext.ProductArticle.FindAsync(new object[] { command.ArticleId }, cancellationToken);

            if (productArticle == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Product article not found."
                };
            }

            dbContext.ProductArticle.Remove(productArticle);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Product article deleted successfully."
            };
        }
    }
}
