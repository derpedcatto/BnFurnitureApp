using System;
using System.Text.Json.Serialization;
using FluentValidation;
using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductTypeController.DTO
{
    public class ProductTypeDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("categoryId")]
        public Guid CategoryId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }
    }

    public class ProductTypeDTOValidator : AbstractValidator<ProductTypeDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypeDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product type name cannot be empty.")
                .MustAsync(IsNameUnique).WithMessage("Product type name must be unique.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug cannot be empty.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be in a valid format.")
                .MustAsync(IsSlugUnique).WithMessage("Slug must be unique.");

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
        }

        private async Task<bool> IsNameUnique(ProductTypeDTO dto, string name, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ProductType.AnyAsync(c => c.Name == name && c.Id != dto.Id, cancellationToken);
            return !result;
        }

        private async Task<bool> IsSlugUnique(ProductTypeDTO dto, string slug, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ProductType.AnyAsync(c => c.Slug == slug && c.Id != dto.Id, cancellationToken);
            return !result;
        }
    }
}
