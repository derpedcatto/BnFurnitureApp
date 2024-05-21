using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.ProductController.DTO
{
    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("productTypeId")]
        public Guid ProductTypeId { get; set; }

        [JsonPropertyName("authorId")]
        public Guid AuthorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("productDetails")]
        public string? ProductDetails { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("created")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

    }

    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name cannot be empty.");

            RuleFor(x => x.ProductTypeId)
                .MustAsync(ExistInDatabase).WithMessage("Producttype_Id must exist.");

            RuleFor(x => x.AuthorId)
                .MustAsync(ExistInDatabase).WithMessage("Author_Id must exist.");

            RuleFor(x => x.Slug)
               .NotEmpty().WithMessage("Slug cannot be empty.")
               .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be in a valid format.")
               .MustAsync(IsSlugUnique).WithMessage("Slug must be unique.");

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
        }

        private async Task<bool> ExistInDatabase(Guid id, CancellationToken cancellationToken)
        {
            var productTypeExists = await _dbContext.ProductType.AnyAsync(pt => pt.Id == id, cancellationToken);
            var authorExists = await _dbContext.User.AnyAsync(a => a.Id == id, cancellationToken);
            return productTypeExists || authorExists;
        }
        private async Task<bool> IsSlugUnique(string slug, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Product.AnyAsync(p => p.Slug == slug, cancellationToken);
            return !result;
        }
    }
}
