using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductArticleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductArticles(
        [FromServices] GetAllProductArticlesHandler handler)
    {
        var query = new GetAllProductArticlesQuery();

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

    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetProductArticleById(
        [FromServices] GetProductArticleByIdHandler handler,
        Guid articleId)
    {
        var query = new GetProductArticleByIdQuery(articleId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
