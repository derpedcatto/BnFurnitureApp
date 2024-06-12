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
    public class UpdateProductReviewDTO
    {
        [JsonPropertyName("id")]
        [Required]
        public Guid Id { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("text")]
        [Required]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateProductReviewDTOValidator : AbstractValidator<UpdateProductReviewDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateProductReviewDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(ReviewExists).WithMessage("Review with given ID does not exist.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .Length(1, 1000).WithMessage("Text length must be between 1 and 1000 characters.");
        }

        private async Task<bool> ReviewExists(Guid reviewId, CancellationToken ct)
        {
            return await _dbContext.ProductReview.AnyAsync(r => r.Id == reviewId, ct);
        }
    }
}
