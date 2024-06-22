using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductArticleController : ControllerBase
{
    [HttpGet("slug/{articleId:guid}")]
    public async Task<IActionResult> GetProductArticleSlugById(
        [FromServices] GetProductArticleSlugByIdHandler handler,
        Guid articleId)
    {
        var query = new GetProductArticleSlugByIdQuery(articleId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetProductArticleById(
        [FromServices] GetProductArticleByIdHandler handler,
        Guid articleId)
    {
        var query = new GetProductArticleByIdQuery(articleId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetAllProductArticles(
        [FromServices] GetAllProductArticlesHandler handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllProductArticlesQuery(
            PageNumber: pageNumber,
            PageSize: pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("category/{categorySlug}")]
    public async Task<IActionResult> GetProductArticleByCategory(
        [FromServices] GetProductArticlesByCategoryHandler handler,
        string categorySlug,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetProductArticlesByCategoryQuery(
            CategorySlug: categorySlug,
            PageNumber: pageNumber,
            PageSize: pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("category/{categorySlug}/{productTypeSlug}")]
    public async Task<IActionResult> GetProductArticleByProductType(
        [FromServices] GetProductArticlesByProductTypeHandler handler,
        string categorySlug,
        string productTypeSlug,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetProductArticlesByProductTypeQuery(
            categorySlug,
            productTypeSlug,
            pageNumber,
            pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost("articles")]
    public async Task<IActionResult> GetProductArticlesByIds(
        [FromServices] GetProductArticlesByIdsHandler handler,
        [FromBody] GetProductArticlesByIdDTO dto)
    {
        var query = new GetProductArticlesByIdsQuery(dto);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
