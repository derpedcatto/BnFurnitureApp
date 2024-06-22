using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO.Request;
using BnFurniture.Application.Controllers.ProductController.DTO.Response;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace BnFurniture.Application.Controllers.ProductController.Commands;

public sealed record CreateProductWithArticlesCommand(
    string DtoJson,
    IFormFile ThumbnailImage);

public sealed class CreateProductWithArticlesHandler : CommandHandler<CreateProductWithArticlesCommand>
{
    private readonly CreateProductWithArticlesDTOValidator _validator;
    private readonly IAppImageService _appImageService;

    public CreateProductWithArticlesHandler(
        CreateProductWithArticlesDTOValidator validator,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
        _appImageService = appImageService;
    }

    public override async Task<ApiCommandResponse> Handle(
        CreateProductWithArticlesCommand request, CancellationToken cancellationToken)
    {
        var dto = JsonSerializer.Deserialize<CreateProductWithArticlesDTO>(request.DtoJson);

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var characteristics = await HandlerContext.DbContext.Characteristic
            .Include(c => c.CharacteristicValues)
            .Where(c => dto.Characteristics
                .Select(pc => pc.Slug).Contains(c.Slug))
            .ToListAsync(cancellationToken);

        var product = new Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            AuthorId = dto.AuthorId,
            ProductTypeId = dto.ProductTypeId,
            Name = dto.Name,
            Slug = dto.Slug,
            Summary = dto.Summary,
            Description = dto.Description,
            ProductDetails = dto.ProductDetails,
            CreatedAt = DateTime.UtcNow,
            Active = dto.Active,
            ProductArticles = new List<ProductArticle>()
        };

        foreach (var combination in GenerateCombinations(characteristics, dto.Characteristics))
        {
            var articleName = $"{dto.Name} {string.Join(" ", combination.Select(cv => cv.Value))}";
            product.ProductArticles.Add(new ProductArticle
            {
                Article = Guid.NewGuid(),
                ProductId = dto.AuthorId,
                AuthorId = product.AuthorId,
                Name = articleName,
                Price = Random.Shared.Next(200, 10000),
                Discount = dto.Discount,
                CreatedAt = DateTime.UtcNow,
                Active = true,
                ProductCharacteristicConfigurations = combination.Select(cv =>
                new Domain.Entities.ProductCharacteristicConfiguration
                {
                    CharacteristicId = cv.CharacteristicId,
                    CharacteristicValueId = cv.Id
                }).ToList()
            });
        }

        HandlerContext.DbContext.Add(product);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        await _appImageService.AddImageAsync(
            AppEntityType.Product,
            product.Id,
            AppEntityImageType.Thumbnail,
            request.ThumbnailImage,
            cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Product with articles creation success"
        };
    }

    private IEnumerable<IEnumerable<CharacteristicValue>> GenerateCombinations(
        List<Characteristic> characteristics,
        List<CharacteristicInputDTO> inputs)
    {
        var valueGroups = inputs.Select(input =>
            characteristics.First(c => c.Slug == input.Slug).CharacteristicValues
                .Where(cv => input.ValueSlugs.Contains(cv.Slug)).ToList()).ToList();

        return CartesianProduct(valueGroups);
    }

    public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(List<List<T>> sequences)
    {
        IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
        return sequences.Aggregate(
            emptyProduct,
            (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Concat(new[] { item }));
    }
}