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
    public class CreateUserWishlistItemDTO
    {
        [JsonPropertyName("productArticleId")]
        public Guid ProductArticleId { get; set; }

        [JsonPropertyName("userWishlistId")]
        public Guid UserWishlistId { get; set; }

        [JsonPropertyName("addedAt")]
        public DateTime AddedAt { get; set; }
    }

    public class CreateUserWishlistItemDTOValidator : AbstractValidator<CreateUserWishlistItemDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateUserWishlistItemDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.ProductArticleId)
                .NotEmpty().WithMessage("ProductArticleId is required.")
                .MustAsync(ProductArticleExists).WithMessage("ProductArticle with given ID does not exist.");

            RuleFor(x => x.UserWishlistId)
                .NotEmpty().WithMessage("UserWishlistId is required.")
                .MustAsync(UserWishlistExists).WithMessage("UserWishlist with given ID does not exist.");

            RuleFor(x => x.AddedAt)
                .NotEmpty().WithMessage("AddedAt is required.");
        }

        private async Task<bool> ProductArticleExists(Guid productArticleId, CancellationToken ct)
        {
            return await _dbContext.ProductArticle.AnyAsync(pa => pa.Article == productArticleId, ct);
        }

        private async Task<bool> UserWishlistExists(Guid userWishlistId, CancellationToken ct)
        {
            return await _dbContext.UserWishlist.AnyAsync(uw => uw.Id == userWishlistId, ct);
        }
    }
}

