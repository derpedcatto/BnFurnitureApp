using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Response;
using BnFurniture.Application.Controllers.CategoryController.Shared;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesWithProductTypesQuery(
    bool IncludeImages = true,
    bool RandomOrder = false);

public sealed class GetAllCategoriesWithProductTypesResponse 
{
    public List<ProductCategoryWithProductTypesDTO> Categories { get; set; }

    public GetAllCategoriesWithProductTypesResponse(
        List<ProductCategoryWithProductTypesDTO> categories)
    {
        Categories = categories;
    }
}

public sealed class GetAllCategoriesWithProductTypesHandler : QueryHandler<GetAllCategoriesWithProductTypesQuery, GetAllCategoriesWithProductTypesResponse>
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
    private readonly CategoryControllerSharedLogic _sharedLogic;

    public GetAllCategoriesWithProductTypesHandler(
        GetAllCategoriesHandler getAllCategoriesHandler,
        CategoryControllerSharedLogic sharedLogic,
        IHandlerContext context) : base(context)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
        _sharedLogic = sharedLogic;
    }

    public override async Task<ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>> Handle(
        GetAllCategoriesWithProductTypesQuery request,
        CancellationToken cancellationToken)
    {
        var categoriesQuery = new GetAllCategoriesQuery(
                IncludeImages: request.IncludeImages,
                RandomOrder: request.RandomOrder,
                FlatList: false);

        var categoriesResponse = await _getAllCategoriesHandler.Handle(
            categoriesQuery, cancellationToken);

        if (!categoriesResponse.IsSuccess)
        {
            return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>
                (false, categoriesResponse.StatusCode)
            {
                Message = categoriesResponse.Message,
                Errors = categoriesResponse.Errors,
                Data = null
            };
        }

        var categories = categoriesResponse.Data!.Categories;

        var categoriesWithProductTypes = await PopulateCategoriesWithProductTypes(
            categories,
            request.IncludeImages,
            request.RandomOrder,
            cancellationToken);

        return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Data = new(categoriesWithProductTypes)
        };
    }

    private async Task<List<ProductCategoryWithProductTypesDTO>> PopulateCategoriesWithProductTypes(
        List<ProductCategoryDTO> categories, 
        bool includeImages,
        bool randomOrder,
        CancellationToken cancellationToken)
    {
        var populatedCategories = new List<ProductCategoryWithProductTypesDTO>();

        foreach (var category in categories)
        {
            var populatedCategory = new ProductCategoryWithProductTypesDTO
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Priority = category.Priority,
                CardImageUri = category.CardImageUri,
                ProductTypes = await _sharedLogic.GetProductTypesForCategory(
                    category.Id, includeImages, cancellationToken),
            };

            if (category.SubCategories != null && category.SubCategories.Count != 0)
            {
                populatedCategory.SubCategories = await PopulateCategoriesWithProductTypes(
                    category.SubCategories, includeImages, randomOrder, cancellationToken);
            }

            populatedCategories.Add(populatedCategory);
        }

        if (randomOrder)
        {
            populatedCategories = populatedCategories.OrderBy(_ => Guid.NewGuid()).ToList();
        }

        return populatedCategories;
    }
}