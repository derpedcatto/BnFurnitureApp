using BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Controllers.CategoryController.Shared;

public class CategoryControllerSharedLogic
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CategoryControllerSharedLogic> _logger;
    private readonly IAppImageService _appImageService;

    public CategoryControllerSharedLogic(
        ApplicationDbContext dbContext,
        ILogger<CategoryControllerSharedLogic> logger,
        IAppImageService appImageService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _appImageService = appImageService;
    }

    public async Task<List<ProductTypeDTO>?> GetProductTypesForCategory(
        Guid categoryId,
        bool includeImages,
        CancellationToken cancellationToken)
    {
        var productTypes = await _dbContext.ProductType
            .Where(pt => pt.CategoryId == categoryId)
            .Select(pt => new ProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority,
                CardImageUri = string.Empty
            })
            .OrderBy(pt => pt.Name)
            .ToListAsync(cancellationToken);

        if (productTypes.Count == 0)
        {
            return null;
        }

        if (includeImages)
        {
            foreach (var productType in productTypes)
            {
                var imageResponse = await _appImageService.GetImagesAsync(
                    AppEntityType.ProductType,
                    productType.Id,
                    AppEntityImageType.PromoCardThumbnail,
                    cancellationToken);

                productType.CardImageUri = imageResponse.Data?.LastOrDefault() ?? string.Empty;
            }
        }

        return productTypes;
    }
}
