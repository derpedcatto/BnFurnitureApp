using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using BnFurniture.Application.Controllers.ProductController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct([FromServices] GetProductHandler handler,
        Guid productId)
    {
        var query = new GetProductQuery(productId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{productSlug}-{characteristicValueSlugs}")]
    public async Task<IActionResult> GetProductArticleByCharacteristics([FromServices] GetProductArticleByCharacteristicsHandler handler, string productSlug, string characteristicValueSlugs)
    {
        var query = new GetProductArticleByCharacteristicsQuery($"{productSlug}-{characteristicValueSlugs}");

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
