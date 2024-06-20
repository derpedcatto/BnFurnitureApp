using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;

public class SetProductArticleImagesDTO
{
    [JsonPropertyName("article")]
    public Guid Article { get; set; }

    [JsonPropertyName("galleryImages")]
    public List<IFormFile> GalleryImages { get; set; } = [];
}

public class SetProductArticleImagesDTOValidator : AbstractValidator<SetProductArticleImagesDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public SetProductArticleImagesDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Article)
            .NotEmpty().WithMessage("Article is empty.")
            .MustAsync(IsProductArticleValid).WithMessage("Article not found.");

        RuleFor(x => x.GalleryImages)
            .NotEmpty().WithMessage("Images are empty")
            .Must(x => x.Count != 0).WithMessage("Images list must not be empty");
    }

    private async Task<bool> IsProductArticleValid(Guid article, CancellationToken ct)
    {
        return await _dbContext.ProductArticle.AnyAsync(p => p.Article == article, ct);
    }
}