using BnFurniture.Application.Controllers.ProductArticleController.Commands;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using BnFurniture.Application.Controllers.ProductController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureAdmin.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
// [AuthorizePermissions(Permissions.DashboardAccess)]
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

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct(
        [FromServices] GetProductHandler handler,
        Guid productId)
    {
        var query = new GetProductQuery(productId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{productSlug}-{characteristicValueSlugs}")]
    public async Task<IActionResult> GetProductArticleByCharacteristics(
        [FromServices] GetProductArticleByCharacteristicsHandler handler,
        string productSlug, 
        string characteristicValueSlugs)
    {
        var query = new GetProductArticleByCharacteristicsQuery($"{productSlug}-{characteristicValueSlugs}");

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }



    [HttpPost]
    public async Task<IActionResult> CreateProductArticle(
        [FromServices] CreateProductArticleHandler handler,
        [FromBody] CreateProductArticleDTO model)
    {
        var command = new CreateProductArticleCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductArticle(
        [FromServices] UpdateProductArticleHandler handler,
        [FromBody] UpdateProductArticleDTO model)
    {
        var command = new UpdateProductArticleCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    // Does not delete other images, only adds new ones
    [HttpPut("images")]
    public async Task<IActionResult> AddProductArticleImages(
        [FromServices] SetProductArticleImagesHandler handler,
        [FromForm] SetProductArticleImagesDTO model)
    {
        var command = new SetProductArticleImagesCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{articleId:guid}")]
    public async Task<IActionResult> DeleteProductArticle(
        [FromServices] DeleteProductArticleHandler handler,
        Guid articleId)
    {
        var command = new DeleteProductArticleCommand(articleId);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
