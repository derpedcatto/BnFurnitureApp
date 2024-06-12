using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetAllProductArticlesQuery();

public sealed class GetAllProductArticlesResponse
{
    public List<ProductArticleResponseDTO> ProductArticles { get; private set; }

    public GetAllProductArticlesResponse(List<ProductArticleResponseDTO> productArticles)
    {
        ProductArticles = productArticles;
    }
}

public sealed class GetAllProductArticlesHandler : QueryHandler<GetAllProductArticlesQuery, GetAllProductArticlesResponse>
{
    public GetAllProductArticlesHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetAllProductArticlesResponse>> Handle(GetAllProductArticlesQuery request, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;

        var productArticles = await dbContext.ProductArticle
            .Include(pa => pa.Product)
            .Include(pa => pa.Author)
            .ToListAsync(cancellationToken);

        var productArticleDTOList = MapProductArticlesToDTOs(productArticles);

        var responseData = new GetAllProductArticlesResponse(productArticleDTOList);
        return new ApiQueryResponse<GetAllProductArticlesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = responseData
        };
    }

    private List<ProductArticleResponseDTO> MapProductArticlesToDTOs(List<ProductArticle> productArticles)
    {
        return productArticles.Select(pa => new ProductArticleResponseDTO
        {
            Article = pa.Article,
            ProductId = pa.ProductId,
            AuthorId = pa.AuthorId,
            Name = pa.Name,
            CreatedAt = pa.CreatedAt,
            UpdatedAt = pa.UpdatedAt,
            Price = pa.Price,
            Discount = pa.Discount,
            Active = pa.Active,
            
        }).ToList();
    }
}
