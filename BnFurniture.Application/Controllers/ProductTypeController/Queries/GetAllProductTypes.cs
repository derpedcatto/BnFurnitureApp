using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Queries;

public sealed record GetAllProductTypesQuery(
    bool IncludeImages,
    bool RandomOrder,
    int? PageSize,
    int? PageNumber);

public sealed class GetAllProductTypesResponse
{
    public List<ProductTypeDTO> ProductTypes { get; set; }

    public GetAllProductTypesResponse(List<ProductTypeDTO> productTypes)
    {
        ProductTypes = productTypes;
    }
}

public sealed class GetAllProductTypesHandler : QueryHandler<GetAllProductTypesQuery, GetAllProductTypesResponse>
{
    private readonly IAppImageService _appImageService;

    public GetAllProductTypesHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public async override Task<ApiQueryResponse<GetAllProductTypesResponse>> Handle(
        GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ProductTypeDTO> productTypesQuery = 
            HandlerContext.DbContext.ProductType
                .Select(pt => new ProductTypeDTO
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Name = pt.Name,
                    Slug = pt.Slug,
                    Priority = pt.Priority,
                    CategorySlug = pt.ProductCategory.Slug
                });

        if (request.RandomOrder)
        {
            productTypesQuery = productTypesQuery.OrderBy(pt => Guid.NewGuid());
        }
        else
        {
            productTypesQuery = productTypesQuery.OrderBy(pt => pt.Name);
        }

        if (request.PageSize.HasValue && request.PageNumber.HasValue)
        {
            var skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
            productTypesQuery = productTypesQuery.Skip(skip).Take(request.PageSize.Value);
        }

        var productTypes = await productTypesQuery.ToListAsync(cancellationToken);

        if (request.IncludeImages)
        {
            foreach (var item in productTypes)
            {
                var cardImageResult = await _appImageService.GetImagesAsync(
                    AppEntityType.ProductType,
                    item.Id,
                    AppEntityImageType.PromoCardThumbnail,
                    cancellationToken);

                var thumbImageResult = await _appImageService.GetImagesAsync(
                    AppEntityType.ProductType,
                    item.Id,
                    AppEntityImageType.Thumbnail,
                    cancellationToken);

                item.CardImageUri = cardImageResult.Data?.FirstOrDefault() ?? string.Empty;
                item.ThumbnailImageUri = thumbImageResult?.Data?.FirstOrDefault() ?? string.Empty;
            }
        }

        return new ApiQueryResponse<GetAllProductTypesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = new(productTypes)
        };
    }
}