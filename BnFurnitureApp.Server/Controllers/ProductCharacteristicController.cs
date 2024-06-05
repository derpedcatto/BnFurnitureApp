using BnFurniture.Application.Controllers.CharacteristicController.Queries;
using BnFurniture.Application.Controllers.ProductController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCharacteristicController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCharacteristics([FromServices] GetAllCharacteristicsHandler handler)
    {
        var query = new GetAllCharacteristicsQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetCharacteristic([FromServices] GetCharacteristicHandler handler,
        string slug)
    {
        var query = new GetCharacteristicQuery(slug);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("product/{slug}")]
    public async Task<IActionResult> GetProductWithCharacteristics([FromServices] GetProductWithCharacteristicsHandler handler,
        string slug)
    {
        var query = new GetProductWithCharacteristicsQuery(slug);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
