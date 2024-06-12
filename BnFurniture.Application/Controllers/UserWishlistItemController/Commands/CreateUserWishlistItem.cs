using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserWishlistItemController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistItemController.Commands
{
    public sealed record CreateUserWishlistItemCommand(CreateUserWishlistItemDTO Dto);

    public sealed class CreateUserWishlistItemHandler : CommandHandler<CreateUserWishlistItemCommand>
    {
        private readonly CreateUserWishlistItemDTOValidator _validator;

        public CreateUserWishlistItemHandler(CreateUserWishlistItemDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateUserWishlistItemCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Dto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, 422)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var newWishlistItem = new UserWishlistItem
            {
                Id = Guid.NewGuid(),
                ProductArticleId = dto.ProductArticleId,
                UserWishlistId = dto.UserWishlistId,
                AddedAt = dto.AddedAt
            };

            await HandlerContext.DbContext.UserWishlistItem.AddAsync(newWishlistItem, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 201)
            {
                Message = "UserWishlistItem created successfully."
            };
        }
    }
}
