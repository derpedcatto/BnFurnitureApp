using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductCategoryController.DTO
{
    public class ProductCategoryDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("parentId")]
        public Guid? ParentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }
    }

    public class ProductCategoryDTOValidator : AbstractValidator<ProductCategoryDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .MustAsync(IsNameUnique).WithMessage("Category name must be unique.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug cannot be empty.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be in a valid format.")
                .MustAsync(IsSlugUnique).WithMessage("Slug must be unique.");

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
        }

        private async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ProductCategory.AnyAsync(c => c.Name == name, cancellationToken);
            return !result;
        }

        private async Task<bool> IsSlugUnique(string slug, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ProductCategory.AnyAsync(c => c.Slug == slug, cancellationToken);
            return !result;
        }
    }
}
