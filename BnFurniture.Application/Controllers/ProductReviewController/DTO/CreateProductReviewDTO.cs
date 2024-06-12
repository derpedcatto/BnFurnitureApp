using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.DTO
{
    public class CreateProductReviewDTO
    {
        [JsonPropertyName("productId")]
        [Required]
        public Guid ProductId { get; set; }

        [JsonPropertyName("authorId")]
        [Required]
        public Guid AuthorId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("text")]
        [Required]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateProductReviewDTOValidator : AbstractValidator<CreateProductReviewDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateProductReviewDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(ProductExists).WithMessage("Product with given ID does not exist.");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("AuthorId is required.")
                .MustAsync(AuthorExists).WithMessage("Author with given ID does not exist.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .Length(1, 1000).WithMessage("Text length must be between 1 and 1000 characters.");
        }

        private async Task<bool> ProductExists(Guid productId, CancellationToken ct)
        {
            return await _dbContext.Product.AnyAsync(p => p.Id == productId, ct);
        }

        private async Task<bool> AuthorExists(Guid authorId, CancellationToken ct)
        {
            return await _dbContext.User.AnyAsync(a => a.Id == authorId, ct);
        }
    }
}

