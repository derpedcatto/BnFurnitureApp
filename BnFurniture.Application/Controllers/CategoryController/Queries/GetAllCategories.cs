using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesQuery();

public sealed class GetAllCategoriesResponse
{
    public List<ProductCategoryDTO> Categories { get; private set; }

    public GetAllCategoriesResponse(List<ProductCategoryDTO> categories)
    {
        Categories = categories;
    }
}

public sealed class GetAllCategoriesHandler : QueryHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    public GetAllCategoriesHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;

        var categories = await dbContext.ProductCategory
            .Include(c => c.ParentCategory)
            .ToListAsync();

        var categoryDTOList = MapCategoriesToDTOs(categories);

        var responseData = new GetAllCategoriesResponse(categoryDTOList);
        return new ApiQueryResponse<GetAllCategoriesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = responseData
        };
    }

    private List<ProductCategoryDTO> MapCategoriesToDTOs(List<ProductCategory> categories)
    {
        var categoryDictionary = categories.ToDictionary(c => c.Id);
        var dtoDictionary = new Dictionary<Guid, ProductCategoryDTO>();

        foreach (var category in categories)
        {
            if (!dtoDictionary.ContainsKey(category.Id))
            {
                dtoDictionary[category.Id] = MapCategoryToDTO(category);
            }

            if (category.ParentId.HasValue)
            {
                if (!dtoDictionary.ContainsKey(category.ParentId.Value))
                {
                    dtoDictionary[category.ParentId.Value] = MapCategoryToDTO(categoryDictionary[category.ParentId.Value]);
                }

                if (dtoDictionary[category.ParentId.Value].SubCategories == null)
                {
                    dtoDictionary[category.ParentId.Value].SubCategories = new List<ProductCategoryDTO>();
                }

                dtoDictionary[category.ParentId.Value].SubCategories.Add(dtoDictionary[category.Id]);
            }
        }

        return dtoDictionary.Values.Where(dto => dto.SubCategories != null).ToList();
    }

    private ProductCategoryDTO MapCategoryToDTO(ProductCategory category)
    {
        return new ProductCategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Priority = category.Priority,
            SubCategories = null
        };
    }
}