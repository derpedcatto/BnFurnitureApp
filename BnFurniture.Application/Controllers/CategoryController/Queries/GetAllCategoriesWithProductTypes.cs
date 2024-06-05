using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesWithProductTypesQuery();

public sealed class GetAllCategoriesWithProductTypesResponse 
{
    public List<ResponseProductCategoryWithProductTypesDTO> Categories { get; set; }

    public GetAllCategoriesWithProductTypesResponse(List<ResponseProductCategoryWithProductTypesDTO> categories)
    {
        Categories = categories;
    }
}

public sealed class GetAllCategoriesWithProductTypesHandler : QueryHandler<GetAllCategoriesWithProductTypesQuery, GetAllCategoriesWithProductTypesResponse>
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;

    public GetAllCategoriesWithProductTypesHandler(GetAllCategoriesHandler getAllCategoriesHandler,
        IHandlerContext context) : base(context)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
    }

    public override async Task<ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>> Handle(GetAllCategoriesWithProductTypesQuery request, CancellationToken cancellationToken)
    {
        var categoriesResponse = await _getAllCategoriesHandler.Handle(new(), cancellationToken);
        if (!categoriesResponse.IsSuccess)
        {
            return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>(false, categoriesResponse.StatusCode)
            {
                Message = categoriesResponse.Message,
                Errors = categoriesResponse.Errors,
                Data = null
            };
        }

        var categories = categoriesResponse.Data!.Categories;

        var categoriesWithProductTypes = await PopulateCategoriesWithProductTypes(categories, cancellationToken);

        return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>(true, 200)
        {
            Data = new(categoriesWithProductTypes)
        };
    }

    private async Task<List<ResponseProductCategoryWithProductTypesDTO>> PopulateCategoriesWithProductTypes(List<ResponseProductCategoryDTO> categories, CancellationToken cancellationToken)
    {
        var populatedCategories = new List<ResponseProductCategoryWithProductTypesDTO>();

        foreach (var category in categories)
        {
            var populatedCategory = new ResponseProductCategoryWithProductTypesDTO
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Priority = category.Priority,
                ProductTypes = await GetProductTypesForCategory(category.Id, cancellationToken)
            };

            if (category.SubCategories != null && category.SubCategories.Any())
            {
                populatedCategory.SubCategories = await PopulateCategoriesWithProductTypes(category.SubCategories, cancellationToken);
            }

            populatedCategories.Add(populatedCategory);
        }

        return populatedCategories;
    }

    private async Task<List<ResponseProductTypeDTO>> GetProductTypesForCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var productTypes = await HandlerContext.DbContext.ProductType
            .Where(pt => pt.CategoryId == categoryId)
            .Select(pt => new ResponseProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority
            })
            .OrderBy(pt => pt.Name)
            .ToListAsync(cancellationToken);

        return productTypes;
    }
}