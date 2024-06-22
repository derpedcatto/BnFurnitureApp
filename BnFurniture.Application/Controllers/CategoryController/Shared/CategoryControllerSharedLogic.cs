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
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductType
            .Where(pt => pt.CategoryId == categoryId)
            .Select(pt => new ProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority,
                CardImageUri = string.Empty
            });

        query = query.OrderBy(pt => pt.Name);

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            var skip = (pageNumber.Value - 1) * pageSize.Value;
            query = query.Skip(skip).Take(pageSize.Value);
        }

        var productTypes = await query.ToListAsync(cancellationToken);

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

    public async Task<List<ProductTypeDTO>?> GetProductTypesForCategory(
        string categorySlug,
        bool includeImages,
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductType
            .Include(pt => pt.ProductCategory)
            .Where(pt => pt.ProductCategory.Slug == categorySlug)
            .Select(pt => new ProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority,
                CardImageUri = string.Empty
            });

        query = query.OrderBy(pt => pt.Name);

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            var skip = (pageNumber.Value - 1) * pageSize.Value;
            query = query.Skip(skip).Take(pageSize.Value);
        }

        var productTypes = await query.ToListAsync(cancellationToken);

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
