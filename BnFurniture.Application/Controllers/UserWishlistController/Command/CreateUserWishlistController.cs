using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserWishlistController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistController.Command
{
    public sealed record CreateUserWishlistCommand(CreateUserWishlistDTO Dto);

    public sealed class CreateUserWishlistHandler : CommandHandler<CreateUserWishlistCommand>
    {
        private readonly CreateUserWishlistDTOValidator _validator;

        public CreateUserWishlistHandler(CreateUserWishlistDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateUserWishlistCommand command, CancellationToken cancellationToken)
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

            var newWishlist = new UserWishlist
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId
            };

            await HandlerContext.DbContext.UserWishlist.AddAsync(newWishlist, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, 201)
            {
                Message = "UserWishlist created successfully."
            };
        }
    }
}
