using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserWishlistItemController.DTO;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistItemController.Commands
{
    public sealed record DeleteUserWishlistItemCommand(Guid Id);

    public sealed class DeleteUserWishlistItemHandler : CommandHandler<DeleteUserWishlistItemCommand>
    {
        private readonly DeleteUserWishlistItemDTOValidator _validator;

        public DeleteUserWishlistItemHandler(DeleteUserWishlistItemDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(DeleteUserWishlistItemCommand command, CancellationToken cancellationToken)
        {
            var userWishlistItem = await HandlerContext.DbContext.UserWishlistItem.FindAsync(new object[] { command.Id }, cancellationToken);

            if (userWishlistItem == null)
            {
                return new ApiCommandResponse(false, 404)
                {
                    Message = "UserWishlistItem not found."
                };
            }

            HandlerContext.DbContext.UserWishlistItem.Remove(userWishlistItem);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 200)
            {
                Message = "UserWishlistItem deleted successfully."
            };
        }
    }
}
