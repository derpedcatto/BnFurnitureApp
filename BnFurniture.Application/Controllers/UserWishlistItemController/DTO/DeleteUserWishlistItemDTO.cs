using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistItemController.DTO
{
    public class DeleteUserWishlistItemDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }

    public class DeleteUserWishlistItemDTOValidator : AbstractValidator<DeleteUserWishlistItemDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteUserWishlistItemDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(UserWishlistItemExists).WithMessage("UserWishlistItem with given ID does not exist.");
        }

        private async Task<bool> UserWishlistItemExists(Guid id, CancellationToken ct)
        {
            return await _dbContext.UserWishlistItem.AnyAsync(w => w.Id == id, ct);
        }
    }
}
